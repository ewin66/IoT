<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingVacant.aspx.cs" Inherits="EasyJoin.ParkingVacant" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="https://img.hcharts.cn/jquery/jquery-1.8.3.min.js"></script>
    <script src="https://img.hcharts.cn/highcharts/highcharts.js"></script>
    <script src="https://img.hcharts.cn/highcharts/modules/exporting.js"></script>
    <script src="https://img.hcharts.cn/highcharts-plugins/highcharts-zh_CN.js"></script>
    <script type="text/javascript">  
        $(function () {
            var data = [], detailChart;;
            $(document).ready(function () {
                // create the master chart
                function createMaster() {
                    $('#master-container').highcharts({
                        chart: {
                            reflow: false,
                            borderWidth: 0,
                            backgroundColor: null,
                            marginLeft: 50,
                            marginRight: 20,
                            zoomType: 'x',
                            events: {
                                // listen to the selection event on the master chart to update the
                                // extremes of the detail chart
                                selection: function (event) {
                                    var extremesObject = event.xAxis[0],
                                        min = extremesObject.min,
                                        max = extremesObject.max,
                                        detailData = [],
                                        xAxis = this.xAxis[0];
                                    // reverse engineer the last part of the data
                                    $.each(this.series[0].data, function () {
                                        if (this.x > min && this.x < max) {
                                            detailData.push([this.x, this.y]);
                                        }
                                    });
                                    // move the plot bands to reflect the new detail span
                                    xAxis.removePlotBand('mask-before');
                                    xAxis.addPlotBand({
                                        id: 'mask-before',
                                        from: Date.UTC(2017, 3, 1),
                                        to: min,
                                        color: 'rgba(0, 0, 0, 0.2)'
                                    });
                                    xAxis.removePlotBand('mask-after');
                                    xAxis.addPlotBand({
                                        id: 'mask-after',
                                        from: Date.UTC(2017, 3, 1),
                                        to: Date.UTC(2017, 3, 31),
                                        color: 'rgba(0, 0, 0, 0.2)'
                                    });
                                    detailChart.series[0].setData(detailData);
                                    return false;
                                }
                            }
                        },
                        title: {
                            text: null
                        },
                        xAxis: {
                            type: 'datetime',
                            showLastTickLabel: true,
                            maxZoom: 7 * 24 * 60, // 7 days
                            plotBands: [{
                                id: 'mask-before',
                                from: Date.UTC(2017, 3, 1),
                                to: Date.UTC(2017, 3, 31),
                                color: 'rgba(0, 0, 0, 0.2)'
                            }],
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
                            max:1,
                            showFirstLabel: false
                        },
                        tooltip: {
                            formatter: function () {
                                return false;
                            }
                        },
                        legend: {
                            enabled: false
                        },
                        credits: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                fillColor: {
                                    linearGradient: [0, 0, 0, 70],
                                    stops: [
                                        [0, Highcharts.getOptions().colors[0]],
                                        [2, 'rgba(255,255,255,0)']
                                    ]
                                },
                                lineWidth: 1,
                                marker: {
                                    enabled: false
                                },
                                shadow: false,
                                states: {
                                    hover: {
                                        lineWidth: 1
                                    }
                                },
                                enableMouseTracking: false
                            }
                        },
                        series: [{
                            type: 'spline',
                            name: '车位使用率',
                            pointInterval: 24 * 60 * 30,
                            pointStart: Date.UTC(2017, 3, 1),
                            data: data
                        }],
                        exporting: {
                            enabled: false
                        }
                    }, function (masterChart) {
                        createDetail(masterChart);
                    })
                .highcharts(); // return chart instance
                }

                function createDetail(masterChart) {
                    // prepare the detail chart
                    var detailData = [],
                        detailStart = Date.UTC(2017, 3, 1);
                    $.each(masterChart.series[0].data, function () {
                        if (this.x >= detailStart) {
                            detailData.push(this.y);
                        }
                    });
                    // create a detail chart referenced by a global variable
                    detailChart = $('#detail-container').highcharts({
                        chart: {
                            marginBottom: 120,
                            reflow: false,
                            marginLeft: 50,
                            marginRight: 20,
                            style: {
                                position: 'absolute'
                            }
                        },
                        credits: {
                            enabled: false
                        },
                        title: {
                            text: '车位使用率分析图'
                        },
                        subtitle: {
                            text: '通过拖动下方图表选择区域'
                        },
                        xAxis: {
                            type: 'datetime'
                        },
                        yAxis: {
                            title: {
                                text: null
                            },
                            maxZoom: 0.1
                        },
                        tooltip: {
                            formatter: function () {
                                var point = this.points[0];
                                return '<b>' + point.series.name + '</b><br/>' +
                                    Highcharts.dateFormat('%Y-%m-%d', this.x) + ':<br/>' +
                                    Highcharts.numberFormat(point.y, 2);
                            },
                            shared: true
                        },
                        legend: {
                            enabled: false
                        },
                        plotOptions: {
                            series: {
                                marker: {
                                    enabled: false,
                                    states: {
                                        hover: {
                                            enabled: true,
                                            radius: 3
                                        }
                                    }
                                }
                            }
                        },
                        series: [{
                            name: '车位使用率',
                            pointStart: detailStart,
                            pointInterval: 24 * 60,
                            data: detailData
                        }],
                        exporting: {
                            enabled: false
                        }
                    }).highcharts(); // return chart
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
                            data.push(rate);
                        }
                        createMaster();
                    });
                }
                // make the container smaller and add a second container for the master chart
                var $container = $('#container')
                .css('position', 'relative');
                $('<div id="detail-container">')
                    .appendTo($container);
                $('<div id="master-container">')
                    .css({ position: 'absolute', top: 300, height: 100, width: '100%' })
                    .appendTo($container);
                // create master and in its callback, create the detail chart
                
                CreateChart();

                function CreateChart() {
                    GetParkingOccupy();
                }
            });
        });
    </script>
</head>
<body>
    <div id="container" style="width:100%;"></div>
</body>
</html>

