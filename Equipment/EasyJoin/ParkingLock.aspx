<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParkingLock.aspx.cs" Inherits="EasyJoin.ParkingLock" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            
                    <asp:Button ID="btnLock" runat="server" Font-Bold="True" Font-Names="黑体" Font-Size="XX-Large" Height="50px" OnClick="btnLock_Click" Width="100px" />
            <asp:Label ID="lbMessage" runat="server"></asp:Label>
                
        </div>
        
    </form>
</body>
</html>
