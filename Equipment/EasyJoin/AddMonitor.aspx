<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddMonitor.aspx.cs" Inherits="EasyJoin.AddMonitor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>    
            模组编号<asp:TextBox ID="TextBox1" runat="server" Width="200px">ZHKBL0416050059</asp:TextBox>    
        </div>
        <div>    
        设备类型<asp:DropDownList ID="DropDownList1" runat="server" Height="25px" Width="200px">
                <asp:ListItem>温度</asp:ListItem>
                <asp:ListItem>湿度</asp:ListItem>
                <asp:ListItem>水压</asp:ListItem>
                <asp:ListItem>水位</asp:ListItem>
                <asp:ListItem>水表</asp:ListItem>
                <asp:ListItem>电表</asp:ListItem>
                <asp:ListItem>噪声</asp:ListItem>
                <asp:ListItem>光感</asp:ListItem>
                <asp:ListItem>距离</asp:ListItem>
                <asp:ListItem>震动</asp:ListItem>
                <asp:ListItem>转速</asp:ListItem>
            </asp:DropDownList>
    </div>
        <div>    
        设备型号<asp:DropDownList ID="DropDownList2" runat="server" Height="25px" Width="200px">
                <asp:ListItem>GL-800液位变送器</asp:ListItem>
            </asp:DropDownList>
    </div>
        <div>    
        安装位置<asp:TextBox ID="TextBox2" runat="server" Width="200px"></asp:TextBox>    
    </div>
        <div>    
            <asp:Button ID="btnJoin" runat="server" Height="25px" Text="接入" Width="100px" />
    </div>
    </form>
</body>
</html>
