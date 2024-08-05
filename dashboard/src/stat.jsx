import React from "react";




export default function Stat(stat) {
	//console.log(stat);
	return <div className={"Box Box-" + (stat.index + 1)}><h2>{stat.data.textAbove}</h2> <h1>{stat.data.value}</h1></div>
}
