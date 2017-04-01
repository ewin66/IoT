<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingOccupy.aspx.cs" Inherits="EasyJoin.ParkingOccupy" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>车位使用情况</title>
    <script src="https://img.hcharts.cn/jquery/jquery-1.8.3.min.js"></script>
    <script src="https://img.hcharts.cn/highcharts/highcharts.js"></script>
    <script src="https://img.hcharts.cn/highcharts/modules/exporting.js"></script>
    <script src="https://img.hcharts.cn/highcharts-plugins/highcharts-zh_CN.js"></script>
    <script type="text/javascript">
        $(function () {
            var data = [], detailChart;;
            $(document).ready(function () {
        var datas = [];
        var chart;
        Highcharts.setOptions({
            global: {useUTC:false}
        });
        var $container = $('#container').css('position', 'relative');
        $('<div id="detail-container">').appendTo($container);
        $('<div id="master-container">').css({ position: 'absolute', top: 0, height: 300, width: '100%' }).appendTo($container);

        CreateChart();

        function CreateChart() {
            GetParkingOccupy();
        }

        function CreateMasterChart() {
            $('#master-container').highcharts({
                chart: {
                    reflow: false,
                    borderWidth: 1,
                    backgroundColor: null,
                    marginLeft: 50,
                    marginRight: 20,
                    zoomType: 'x'
                    //,events: {
                    //    // listen to the selection event on the master chart to update the
                    //    // extremes of the detail chart
                    //    selection: function (event) {
                    //        var extremesObject = event.xAxis[0],
                    //            min = extremesObject.min,
                    //            max = extremesObject.max,
                    //            detailData = [],
                    //            xAxis = this.xAxis[0];
                    //        // reverse engineer the last part of the data
                    //        $.each(this.series[0].data, function () {
                    //            if (this.x > min && this.x < max) {
                    //                detailData.push([this.x, this.y]);
                    //            }
                    //        });
                    //        // move the plot bands to reflect the new detail span
                    //        xAxis.removePlotBand('mask-before');
                    //        xAxis.addPlotBand({
                    //            id: 'mask-before',
                    //            from: Date.UTC(2006, 0, 1),
                    //            to: min,
                    //            color: 'rgba(0, 0, 0, 0.2)'
                    //        });
                    //        xAxis.removePlotBand('mask-after');
                    //        xAxis.addPlotBand({
                    //            id: 'mask-after',
                    //            from: max,
                    //            to: Date.UTC(2008, 11, 31),
                    //            color: 'rgba(0, 0, 0, 0.2)'
                    //        });
                    //        detailChart.series[0].setData(detailData);
                    //        return false;
                    //    }
                    //}
                },
                title: {
                    text: '车位使用率统计'
                },
                subtitle: {
                    text: '通过拖动下方图表选择区域'},
                xAxis: {
                    type: 'datetime',
                    showLastTickLabel: true,
                    maxZoom: 60,//14 * 24 * 3600000,                     
                    title: {
                        text: null
                    }
                },
                yAxis: {
                    gridLineWidth: 0,
                    labels: {
                        enabled: false
                    },
                    title: {
                        text: null
                    },
                    min: 0,
                    showFirstLabel: false
                },
                tooltip: {
                    formatter: function () {
                        var point = this.points[0];
                        return '<b>' + point.series.name + '</b><br/>' +
                            Highcharts.dateFormat('%Y-%m-%d %H:%M:%S', this.x) + '<br/>' +
                            Highcharts.numberFormat(point.y, 2);
                    },
                    shared: true
                },
                legend: {
                    enabled: false
                },
                credits: {
                    enabled: false
                },
                series: [{
                    type: 'spline',
                    name: '使用率',
                    pointInterval:60, //24 * 3600 * 1000,
                    //pointStart: Date.UTC(2017, 3, 30),
                    data: datas
                }],
                exporting: {enabled: false}
            });
        }

        function SetChartData() {
            if (chart == null) return;
            if (datas == null) return;
            GetParkingOccupy();
            for (var j = 0; j < datas.length; j++)
            {
                var newPoint1;
                newPoint1 = {
                    x: datas[j].x, // current time
                    y: datas[j].y
                };
                chart.series[0].addPoint(newPoint1, true, true);
            }
            //chart.series[0].setData(datas);
            //chart.addSeries(chartData, true, true);
            //chart.xAxis.push(xPoint);
            //chart.addSeries(chartData,true,false);
            //LoadMoreDataForChart(chart.series);
        }

        function LoadMoreDataForChart(seriesObj) {
                var series = seriesObj[0];
                $.getJSON("GetParkingState.ashx", {}, function (data) {
                    var total = 0;
                    var occupy = 0;
                    var vacant = 0;
                    var rate;
                    var time;
                    for (var i = 0, l = data.length; i < l; i++) {
                        time = data[i]["UpdateTime"];
                        occupy = data[i]["Occupy"];
                        vacant = data[i]["Vacant"];
                        total = occupy + vacant;
                        rate = occupy / total;
                    
                        var x = time;
                        var y = rate;
                        var newPoint;
                        datas.push({
                            x: time,
                            y: rate
                        });



                    }
                });
            }

        function GetMoreDataForChart() {
                $.getJSON("GetParkingState.ashx", {}, function (data) {
                    var total = 0;
                    var occupy = 0;
                    var vacant = 0;
                    var rate;
                    var time;
                    for (var i = 0, l = data.length; i < l; i++) {
                        time = data[i]["UpdateTime"];
                        occupy = data[i]["Occupy"];
                        vacant = data[i]["Vacant"];
                        total = occupy + vacant;
                        rate = occupy / total;
                        var x = time;
                        var y = rate;
                        var newPoint;
                        newPoint = {
                            x: x,
                            y: y
                        };
                        series.addPoint(newPoint, false, true);
                    }
                });
            }

        function GetParkingOccupy() {
            var time = (new Date()).getTime();
            $.getJSON("GetParkingOccupy.ashx", {}, function (d) {
                var total = 0;
                var occupy = 0;
                var vacant = 0;
                var rate;
                for (var i = 0, l = d.length; i < l; i++) {
                    time = d[i]["UpdateTime"];
                    occupy = d[i]["Occupy"];
                    vacant = d[i]["Vacant"];
                    total = occupy + vacant;
                    rate = occupy / total;
                    datas.push({
                        x: Date.parse(time),
                        y: rate
                    });
                }
                CreateMasterChart();
            });
        }
            });
        });
    </script>
</head>
<body>
    <div id="container" style="width:100%;"></div>
</body>
</html>
