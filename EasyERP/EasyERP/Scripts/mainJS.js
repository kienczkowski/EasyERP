/// <reference path="jquery-2.1.1.min.js" />
/// <reference path="jquery-2.1.0-vsdoc.js" />
/// <reference path="jquery-ui-1.11.1.js" />
/// <reference path="js/Chart.js" />

//GLOBAL VARIABLE
var timer;

$(document).ready(function () {

    var controllerName = $("#controllerName").val();
    if (controllerName == "Dashboard") {

        timer = setInterval(TaskPercent, 5000);

        al = 0;
        kalendarz = document.getElementById('data');
        printCalendar();

        $('#lista tr').click(function () {
            var select = $(this).find("a");
            var url = select.attr("href");
            $.ajax({
                url: url,
                success: function (html) {
                    $('#allTask').html(html);
                }
            });
        });

        $("#searcher").keydown(function (e) {
            if (e.keyCode == 13) {
                var url = "/Dashboard/Searcher?phrase=" + $(this).val();
                window.location.href = url;
                return false;
            }
        });
    }

    if (controllerName == "Reports") {

        //Chart.defaults.global = {
        //    // Boolean - Whether to animate the chart
        //    animation: true,

        //    // Number - Number of animation steps
        //    animationSteps: 60,

        //    // String - Animation easing effect
        //    animationEasing: "easeOutQuart",

        //    // Boolean - If we should show the scale at all
        //    showScale: true,

        //    // Boolean - If we want to override with a hard coded scale
        //    scaleOverride: false,

        //    // ** Required if scaleOverride is true **
        //    // Number - The number of steps in a hard coded scale
        //    scaleSteps: null,
        //    // Number - The value jump in the hard coded scale
        //    scaleStepWidth: null,
        //    // Number - The scale starting value
        //    scaleStartValue: null,

        //    // String - Colour of the scale line
        //    scaleLineColor: "rgba(0,0,0,.1)",

        //    // Number - Pixel width of the scale line
        //    scaleLineWidth: 1,

        //    // Boolean - Whether to show labels on the scale
        //    scaleShowLabels: true,

        //    // Interpolated JS string - can access value
        //    scaleLabel: "<%=value%>",

        //    // Boolean - Whether the scale should stick to integers, not floats even if drawing space is there
        //    scaleIntegersOnly: true,

        //    // Boolean - Whether the scale should start at zero, or an order of magnitude down from the lowest value
        //    scaleBeginAtZero: false,

        //    // String - Scale label font declaration for the scale label
        //    scaleFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",

        //    // Number - Scale label font size in pixels
        //    scaleFontSize: 12,

        //    // String - Scale label font weight style
        //    scaleFontStyle: "normal",

        //    // String - Scale label font colour
        //    scaleFontColor: "#666",

        //    // Boolean - whether or not the chart should be responsive and resize when the browser does.
        //    responsive: false,

        //    // Boolean - whether to maintain the starting aspect ratio or not when responsive, if set to false, will take up entire container
        //    maintainAspectRatio: true,

        //    // Boolean - Determines whether to draw tooltips on the canvas or not
        //    showTooltips: true,

        //    // Array - Array of string names to attach tooltip events
        //    tooltipEvents: ["mousemove", "touchstart", "touchmove"],

        //    // String - Tooltip background colour
        //    tooltipFillColor: "rgba(0,0,0,0.8)",

        //    // String - Tooltip label font declaration for the scale label
        //    tooltipFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",

        //    // Number - Tooltip label font size in pixels
        //    tooltipFontSize: 14,

        //    // String - Tooltip font weight style
        //    tooltipFontStyle: "normal",

        //    // String - Tooltip label font colour
        //    tooltipFontColor: "#fff",

        //    // String - Tooltip title font declaration for the scale label
        //    tooltipTitleFontFamily: "'Helvetica Neue', 'Helvetica', 'Arial', sans-serif",

        //    // Number - Tooltip title font size in pixels
        //    tooltipTitleFontSize: 14,

        //    // String - Tooltip title font weight style
        //    tooltipTitleFontStyle: "bold",

        //    // String - Tooltip title font colour
        //    tooltipTitleFontColor: "#fff",

        //    // Number - pixel width of padding around tooltip text
        //    tooltipYPadding: 6,

        //    // Number - pixel width of padding around tooltip text
        //    tooltipXPadding: 6,

        //    // Number - Size of the caret on the tooltip
        //    tooltipCaretSize: 8,

        //    // Number - Pixel radius of the tooltip border
        //    tooltipCornerRadius: 6,

        //    // Number - Pixel offset from point x to tooltip edge
        //    tooltipXOffset: 10,

        //    // String - Template string for single tooltips
        //    tooltipTemplate: "<%if (label){%><%=label%>: <%}%><%= value %>",

        //    // String - Template string for single tooltips
        //    multiTooltipTemplate: "<%= value %>",

        //    // Function - Will fire on animation progression.
        //    onAnimationProgress: function () { },

        //    // Function - Will fire on animation completion.
        //    onAnimationComplete: function () { }
        //}


        var data = {
            labels: ["January", "February", "March", "April", "May", "June", "July"],
            datasets: [
                {
                    label: "My First dataset",
                    fillColor: "rgba(220,220,220,0.2)",
                    strokeColor: "rgba(220,220,220,1)",
                    pointColor: "rgba(220,220,220,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(220,220,220,1)",
                    data: [65, 59, 80, 81, 56, 55, 40]
                },
                {
                    label: "My Second dataset",
                    fillColor: "rgba(151,187,205,0.2)",
                    strokeColor: "rgba(151,187,205,1)",
                    pointColor: "rgba(151,187,205,1)",
                    pointStrokeColor: "#fff",
                    pointHighlightFill: "#fff",
                    pointHighlightStroke: "rgba(151,187,205,1)",
                    data: [28, 48, 40, 19, 86, 27, 90]
                }
            ]
        };


        var ctx = document.getElementById("myChart").getContext("2d");
        var myNewChart = new Chart(ctx).Line(data, { responsive: false });
    }

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

    $("#success").delay(3000).slideUp();
})


function TaskPercent() {
    //var root = location.protocol + '//' + location.host;
    var userId = $("#UserId").val();
    ajaxHelper("/api/Task/GetPercent/" + userId, "GET");
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
            clearTimeout(timer);
        }

    });
}

function AmountCheck(e)
{
    ///Orders/AddProductToBasket?ProductId=3&ClientId=0&Amount=1&Amount=10&Amount=10&Amount=10
    var href = "/Orders/AddProductToBasket?ProductId=3&ClientId=0";
    var name = '#Amount' + $(e).attr("ProductId");
    href = href + "&Amount=" + $(name).val();
    $(e).attr("href", href);
    return true;
}
