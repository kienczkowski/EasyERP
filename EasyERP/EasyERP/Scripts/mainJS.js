/// <reference path="jquery-2.1.1.min.js" />
/// <reference path="jquery-2.1.0-vsdoc.js" />
$(document).ready(function () {

    setInterval(function () {
        //AKCJA DO WYKONYWANIA
        TaskPercent();
    }, 5000);
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


function TaskPercent() {
    //var root = location.protocol + '//' + location.host;
    ajaxHelper("/api/Task/GetPercent", "GET");
}

function TaskFinite(percent1, percent2) {
    var Finite = [
			{
			    value: percent2,
			    color: "#e3e3e3",
			    highlight: "#e3e3e3"
			},
			{
			    value: percent1,
			    color: "#8db859",
			    highlight: "#5AD3D1"
			}
    ];

    return Finite;
}

function TaskInFinite(percent1, percent2) {
    var InFinite = [
			{
			    value: percent2,
			    color: "#e3e3e3",
			    highlight: "#e3e3e3"
			},
			{
			    value: percent1,
			    color: "#fb4d61",
			    highlight: "#5AD3D1"
			}
    ];

    return InFinite;
}

function ajaxHelper(uri, method, data) {
    $.ajax({
        type: method,
        url: uri,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var FinitePercentStart = response[0].PercentStart;
            var FinitePercentEnd = response[0].PercentEnd;
            var InFinitePercentStart = response[1].PercentStart;
            var InFinitePercentEnd = response[1].PercentEnd;
            var ctx1 = document.getElementById("chart-area-1").getContext("2d");
            window.myDoughnut1 = new Chart(ctx1).Doughnut(TaskFinite(FinitePercentStart, FinitePercentEnd), { responsive: false });
            var ctx2 = document.getElementById("chart-area-2").getContext("2d");
            window.myDoughnut2 = new Chart(ctx2).Doughnut(TaskInFinite(InFinitePercentStart, InFinitePercentEnd), { responsive: false });
            $(".skonczone").text(FinitePercentStart + "%");
            $(".nie-skonczone").text(InFinitePercentStart + "%");
        },
        error: function (xhr, status, msg) {
            alert(xhr.responseText);
        }

    });
}
