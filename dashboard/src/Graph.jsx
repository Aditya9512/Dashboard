import React from "react";
import { Line } from 'react-chartjs-2';
import './graph.css';

export default function Graph({ index, data }) {
	return (
		<div className={`graph Box-${index + 6}`}>
			<Line
				options={{
					responsive: true,
					maintainAspectRatio: false,
					scales: {
						x: {
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
					}
				}}
				data={{
					labels: data.xAxis.map(x => new Date(x)),
					datasets: data.dataSets.map(k => ({
						label: data.title,
						data: k.yAxis,
						borderColor: `rgb(${k.borderColor})`,
						backgroundColor: `rgba(${k.backgroundColor})`,
					}))
				}}
			/>
		</div>
	);
}
