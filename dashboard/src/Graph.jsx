import React from "react";
import { Line } from 'react-chartjs-2';


export default function Graph(graphData) {
	//console.log(["Graph", graphData]);
	return <div className={"Box Box-" + (graphData + 6)}><Line
		options={{

			responsive: true,
			maintainAspectRatio: false,
			scales:
			{
				x:
				{
					type: 'time',
					time: {
						displayFormats: {
							quarter: 'MMM YYYY'
						}
					},
					title: {
						display: true,
						text: 'Date'
					},

					stacked: true,
				},
				y: {
					title: {
						display: true,
						text: 'Count'
					},
				}
			},
			plugins: {
				legend: {
					position: 'top',
				},


				responsive: true,

			}
		}
		}
		/* data={{
				 labels: graphData.data.map(function (i) { return i.xAxis.toString() })   
				   datasets: [
					   {
						   labels: 'Dataset 1',
						   data: graphData.data.map(function (i) { return i.yAxis1 })  
						   borderColor: 'rgb(255, 99, 132)',
						   backgroundColor: 'rgba(255, 99, 132, 0.5)',
					   },
					   {
						   labels: 'Dataset 2',
						   data: graphData.data.map(function (i) { return i.yAxis2 }),
						   borderColor: 'rgb(53, 162, 235)',
						   backgroundColor: 'rgba(53, 162, 235, 0.5)',
					   },
				   ],
			   }} */


		data={{

			labels: graphData.data.xAxis.map(x => new Date(x)),
			//labels: graphData.data.xAxis,
			datasets: graphData.data.dataSets.map(function (k) {
				//console.log(["k", k]);
				return {
					label: graphData.data.title,
					data: k.yAxis,
					borderColor: 'rgb(' + k.borderColor + ')',
					backgroundColor: 'rgba(' + k.backgroundColor + ')',
				}
			})
			/*datasets:[  {
			  labels: graphData.data.dataSets.map(function (k) { return k.label.toString() }),
				data: graphData.data.dataSets.map(function (x) { return x.yAxis }),
			  //color: graphData.data.dataSets.map(function (k) { return k.colour }),
			  borderColor: 'rgb(53, 162, 235)',
			  backgroundColor: 'rgba(53, 162, 235, 0.5)',
		  },
		  ]*/
		}}
	/>
	</div>
}

