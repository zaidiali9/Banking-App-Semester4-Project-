
window.onload = function () {
	var chart = new CanvasJS.Chart("chartContainer", {
		animationEnabled: true,
		data: [{
			type: "pie",
			startAngle: 0,
			yValueFormatString: "##0.00\"%\"",
			indexLabel: "{label} {y}",
			dataPoints: [
				{ y: loanamu, label: "Principal" },
				{ y: intamu, label: "Intrest" },
			]
		}]
	});
	chart.options.backgroundColor = "#F6F6F6"; // Replace #ff0000 with your desired color
	chart.render();
}