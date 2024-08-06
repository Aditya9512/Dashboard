import React, { useState, useEffect, useRef } from "react";
import './App.css';
import Graph from './Graph.jsx';
import State from "./stat.jsx";
import ringer from "./alert.mp3";
import 'chartjs-adapter-date-fns';
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

// Registering Chart.js components
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

function GetData(setClientdata, time, setTime, setAlerts, shownAlerts) {
	setTime(new Date());
	fetch('http://localhost:5202/Clients?DT=' + time.toISOString())
		.then(response => {
			if (!response.ok) {
				throw new Error(`HTTP error! status: ${response.status}`);
			}
			return response.text();
		})
		.then(text => {
			console.log('Raw server response:', text);
			return JSON.parse(text);
		})
		.then(json => {
			const audio = new Audio(ringer);
			audio.loop = false;
			setClientdata(json);

			if (json.newSignups && json.newSignups.length > 0) {
				const newAlerts = json.newSignups
					.filter(signup => !shownAlerts.has(signup.member))
					.map(signup => ({
						message: `🌶️ New member ${signup.member} has joined with a ${signup.package} package! 🌶️`,
						id: Date.now() + Math.random()
					}));

				if (newAlerts.length > 0) {
					setAlerts(prevAlerts => [...prevAlerts, ...newAlerts]);
					newAlerts.forEach(alert => shownAlerts.add(alert.id)); // Mark as shown
					// audio.play(); // Uncomment if you want the alert sound
				}
			}

			setTimeout(() => {
				setAlerts([]); // Clear all alerts after 10 seconds
				audio.pause();
			}, 10000);
		})
		.catch(error => {
			console.error('Error:', error);
		});
}

export default function Dashboard() {
	const [Clientdata, setClientdata] = useState(null);
	const [time, setTime] = useState(null);
	const [alerts, setAlerts] = useState([]);
	const shownAlerts = useRef(new Set()); // Track shown alerts

	useEffect(() => {
		if (time === null) {
			const currentDateObj = new Date();
			const numberOfMlSeconds = currentDateObj.getTime();
			const subMlSeconds = 20000 * 15 * 60 * 1000;
			const newDateObj = new Date(numberOfMlSeconds - subMlSeconds);
			GetData(setClientdata, newDateObj, setTime, setAlerts, shownAlerts.current);
		} else {
			const interval = setInterval(() => {
				GetData(setClientdata, time, setTime, setAlerts, shownAlerts.current);
			}, 50900); // Fetch data every 50.9 seconds
			return () => clearInterval(interval); // Cleanup interval on unmount
		}
	}, [time]);

	if (Clientdata === null) {
		return "Loading...";
	}

	return (
		<div className="App">
			<div className="alert-container">
				{alerts.map(alert => (
					<div key={alert.id} className="alert-popup">
						{alert.message}
					</div>
				))}
			</div>
			<div className="twelve">
				<h1>DASHBOARD</h1>
			</div>
			<div className={"Boxrow"}>
				<div className={"Box Box-1 box-blue"}><p>Clients This Year</p><br /><h2>{Clientdata.year}</h2></div>
				<div className={"Box Box-2 box-green"}><p>Clients This Month</p><br /><h2>{Clientdata.month}</h2></div>
				<div className={"Box Box-3 box-red"}><p>Clients This Week</p><br /><h2>{Clientdata.week}</h2></div>
				<div className={"Box Box-4 box-yellow"}><p>Total Clients</p><br /><h2>{Clientdata.total}</h2></div>
			</div>

			<br />
			<div className="graph-container">
				{Clientdata.graphs.map((graphData, index) =>
					<Graph key={index} index={index} data={graphData} />
				)}
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
