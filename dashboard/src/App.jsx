import React, { useState, useEffect } from "react";
import './App.css';
import Stat from './stat.jsx';
import Graph from './Graph.jsx';
import ringer from "./alert.mp3";
import 'chartjs-adapter-date-fns';
import Chilli from './Chilli.png';
import DonutChart from 'react-donut-chart';
import {
	Chart as ChartJS,
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	Title,
	Tooltip,
	Legend,
	TimeScale
} from 'chart.js';

ChartJS.register(
	CategoryScale,
	LinearScale,
	PointElement,
	LineElement,
	Title,
	Tooltip,
	Legend,
	TimeScale
);

function GetData(setClientdata, time, setTime) {
	setTime(new Date());
	fetch('http://localhost:5202/Clients?DT=' + time.toISOString())
		.then(response => {
			if (!response.ok) {
				throw new Error(`HTTP error! status: ${response.status}`);
			}
			return response.text();  // Get the raw text first
		})
		.then(text => {
			console.log('Raw server response:', text);  // Log the raw response
			return JSON.parse(text);  // Then parse it as JSON
		})
		.then(json => {
			const audio = new Audio(ringer);
			audio.loop = false;
			//audio.play();
			setClientdata(json);
			setTimeout(function () {
				let alertDiv = document.getElementById("alert");
				if (alertDiv) {
					alertDiv.style.opacity = "0";
					alertDiv.style.transition = "opacity 1s ease";
				}
				audio.pause();
			}, 50890);
		})
		.catch(error => {
			console.error('Error:', error);
			// Handle the error appropriately
		});
}

export default function Dashboard() {
	const [Clientdata, setClientdata] = useState(null);
	const [time, setTime] = useState(null);

	useEffect(() => {
		if (time === null) {
			var currentDateObj = new Date();
			var numberOfMlSeconds = currentDateObj.getTime();
			var subMlSeconds = 20000 * 15 * 60 * 1000;
			var newDateObj = new Date(numberOfMlSeconds - subMlSeconds);
			GetData(setClientdata, newDateObj, setTime);
		} else {
			setTimeout(() => {
				GetData(setClientdata, time, setTime);
			}, 50900);
		}
	}, [time]);

	console.log({ Clientdata, time });

	if (Clientdata === null) {
		return "Loading...";
	}

	return (
		<div className="App">
			<div className="twelve">
				<h1>DASHBOARD</h1>
			</div>
			<div className={"Boxrow "}>
				<div className={"Box Box-1"}><p>Clients This Year</p><br /><h2> {Clientdata.year}</h2></div>
				<div className={"Box Box-2"}><p>Clients This Month</p><br /><h2> {Clientdata.month}</h2> </div>
				<div className={"Box Box-3"}><p>Clients This Week</p><br /><h2> {Clientdata.week}</h2> </div>
				<div className={"Box Box-4"}><p>Total Clients</p><br /><h2>{Clientdata.total} </h2> </div>
			</div>
			<div className="d">
				{("newSignups" in Clientdata && Clientdata.newSignups.length > 0) ? Clientdata.newSignups.map((member, index) =>
					<div id="alert" key={index}>
						<img className="fit-picture" src={Chilli} alt="Chilli" />
						<strong>{member.member} </strong> Has Joined With a <strong> {member.package} </strong>
						<img className="fiit-picture" src={Chilli} alt="Chilli" />
					</div>)
					: null}
			</div> <br />
			<div className={"Boxrow "}>
				<div className={"gBox"}>
					{Clientdata.graphs.map((graphData, index) =>
						<Graph key={index} index={index} data={graphData} />)}
				</div>
				<div className={"gBox"}>
					<DonutChart
						data={Clientdata.packages.map(name => ({
							label: name.name,
							value: name.count,
						}))}
					/>
				</div>
			</div>
		</div>
	);
}
