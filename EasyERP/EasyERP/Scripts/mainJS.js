/// <reference path="jquery-2.1.1.min.js" />
/// <reference path="jquery-2.1.0-vsdoc.js" />
$(document).ready(function () {
    var doughnutData1 = [
			{
			    value: 75,
			    color: "#e3e3e3",
			    highlight: "#e3e3e3"
			},
			{
			    value: 15,
			    color: "#8db859",
			    highlight: "#5AD3D1"
			}
    ];

    var doughnutData2 = [
        {
            value: 15,
            color: "#e3e3e3",
            highlight: "#e3e3e3"
        },
        {
            value: 75,
            color: "#fb4d61",
            highlight: "#5AD3D1"
        }
    ];

    var ctx1 = document.getElementById("chart-area-1").getContext("2d");
    window.myDoughnut1 = new Chart(ctx1).Doughnut(doughnutData1, { responsive: false });
    var ctx2 = document.getElementById("chart-area-2").getContext("2d");
    window.myDoughnut2 = new Chart(ctx2).Doughnut(doughnutData2, { responsive: false });

    var rozwin = document.getElementById('rozwin');
    var menu = document.getElementById('menu');
    var content = document.getElementById('content');

    rozwin.onclick = function () {
        if (this.style.marginRight == '30px') {
            this.style.marginRight = '20px';
            menu.style.width = '72px';
            menu.style.color = '#343f51';
            content.style.paddingLeft = '72px';
        } else {
            this.style.marginRight = '30px';
            menu.style.width = '192px';
            menu.style.color = '#fff';
            content.style.paddingLeft = '192px';
        }
    };

    al = 0;
    kalendarz = document.getElementById('data');

    printCalendar();

    $("#addProduct-modalPopUp").dialog({
        autoOpen: false,
        show: {
            effect: "blind",
            duration: 1000
        },
        hide: {
            effect: "explode",
            duration: 1000
        }
    });

    $("#open-dialog").click(function () {
        $("#addProduct-modalPopUp").dialog("open");
    });
})
