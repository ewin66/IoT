<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="EasyJoin.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>帕克智控</title>
    <script src="js/jquery-1.7.2.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Label ID="lbInfo" runat="server" Font-Bold="True" Font-Names="微软雅黑" ForeColor="#000099" Text="受监测车位总数：占用：空闲"></asp:Label>
    <div runat="server" id="divMain" style="height:401px;width:715px;background-image:url('Image/B.jpg');position:relative;">
    </div>
    </form>
    <script type="text/javascript">
        ShowParkingState();
        setInterval(ShowParkingState, 10000);
        function ShowParkingState() {
            var mydate = new Date();
            var t=mydate.toLocaleString();
            $.getJSON("GetParkingState.ashx", {}, function (data) {
                var listHtml = "";
                var total=0;
                var occupy=0;
                var vacant = 0;
                var toolTip = "";
                for (var i = 0, l = data.length; i < l; i++) {
                    if (data[i]["STATE"] == "1") {
                        $("#" + data[i]["WPSD_ID"]).css("background", "#006400");
                        vacant += 1;
                    }
                    if (data[i]["STATE"] == "2") {
                        $("#" + data[i]["WPSD_ID"]).css("background", "#FF0000");
                        occupy+=1;
                    }
                    toolTip = data[i]["ToolTip"];
                    $("#" + data[i]["WPSD_ID"]).attr("title", toolTip);
                    total += 1;
                }
                $("#lbInfo").text("车位总数：" + total + " 占用：" + occupy + " 空闲：" + vacant + "      更新时间：" + t);
            });
            }
    </script>
</body>
</html>
