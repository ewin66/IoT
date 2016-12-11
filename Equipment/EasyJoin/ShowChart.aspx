<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowChart.aspx.cs" Inherits="EasyJoin.ShowChart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        #div_MonitorList {
            position:absolute;
            left:10px;
            top:60px;
            height:500px;
            vertical-align:top;
            width:200px;            
            border: 1px dashed #000000;  
            padding:8px;  
            color: #fafafa;
            letter-spacing: 0;
            font-family: arial,"Hiragino Sans GB","Microsoft Yahei",sans-serif;
            font-size:12px;        
        }
    </style>
    <script type="text/javascript" src="js/jquery-1.5.2.min.js"></script>
    <script type="text/javascript" src="js/highcharts.js"></script>
    <%--取消下面注释就可以在右上角看到效果--%>
    <%--<script type="text/javascript" src="download/exporting.js" charset="gb2312"></script>--%>
    <script type="text/javascript" src="js/theme/gray.js"></script>
    <script type="text/javascript">
        
        var chart;
        var d = [0.15, 0.16, 0.17, 0.18, 0.19, 0.10, 0.08, 0.02, 0.25, 0.22, 0.23, 0.20];
        $(document).ready(function() {
            chart = new Highcharts.Chart({
                chart: {
                    renderTo: 'container',          //放置图表的容器
                    plotBackgroundColor: null,
                    plotBorderWidth: null,
                    defaultSeriesType: 'line'
                },
                title: {
                    text: '水位曲线图'
                },
                subtitle: {
                    text: '监测点1'
                },
                events : {
                    load : st// 定时器
                },
                xAxis: {//X轴数据
                    categories: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                    labels: {
                        rotation: -45, //字体倾斜
                        align: 'right',
                        style: { font: 'normal 13px 宋体' }
                    }
                },
                yAxis: {//Y轴显示文字
                    title: {
                        text: '水位/米'
                    }
                },
                tooltip: {
                    enabled: true,
                    formatter: function() {
                        return '<b>' + this.x + '</b><br/>' + this.series.name + ': ' + Highcharts.numberFormat(this.y, 1);
                    }
                },
                plotOptions: {
                    line: {
                        dataLabels: {
                            enabled: true
                        },
                        enableMouseTracking: true//是否显示title
                    }
                },
                series: [                    
                {
                    name: '监测点1',
                    data: [0.15, 0.16, 0.17, 0.18, 0.19, 0.10, 0.08, 0.02, 0.25, 0.22, 0.23, 0.20]}]
                });
        });
        setInterval(ShowChartData, 3000);
        //10秒钟刷新一次数据
        function st() {
            //setInterval("getData()", 10000);
        }

        function getData() {

            var categories = [];
            $.ajax({
                type: "post",
                url: "${pageContext.request.contextPath}/demo/chart_demo.action",
                dataType: "json",
                success: function (data) {
                    var d = [];
                    $(data).each(function (n, item) {
                        d.push(item.data);
                        categories.push(item.categories);
                    })
                    chart.series[0].setData(d);
                    chart.xAxis[0].setCategories(categories);
                }
            });
        }

        function ShowChartData()
        {
            d.shift();//移除最前一个元素并返回该元素值，数组中元素自动前移
            d.push(d[0]);// 将一个或多个新元素添加到数组结尾，并返回数组新长度
            //var oSeries = {
            //    name: "第二条",
            //    data: [0.25, 0.12, 0.11, 0.12, 0.13, 0.14, 0.08, 0.02, 0.05, 0.20, 0.21, 0.30]
            //}
            chart.series[0].setData(d);
            //addSeries(oSeries);
        }
    </script>
</head>
<body>
<div id="div_MonitorList">
    <div style='color:#FF0000'><a href="#">监测点1-数贸大厦【0.09】</a></div>
    <div style='color:#000000'><a href="#">监测点2-凯茵豪园【0.05】</a></div>
    <div id="container">
    </div> 
</div>
</body>
</html>
