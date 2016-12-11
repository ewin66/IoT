<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EasyJoin.aspx.cs" Inherits="EasyJoin.EasyJoin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:Label ID="Label1" runat="server" Text="设备编号"></asp:Label>
            <asp:TextBox ID="txtID" runat="server" ReadOnly="True"></asp:TextBox>
        </div>
        <div>
            <asp:Label ID="Label2" runat="server" Text="设备名称"></asp:Label>
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </div>
        <div>    
            模组编号<asp:TextBox ID="txtCOMMUNICATION_NO" runat="server" Width="200px">ZHKBL0416050059</asp:TextBox>    
            设备地址号<asp:TextBox ID="txtADDRESS_NO" runat="server" Width="50px"></asp:TextBox>    
        </div>
        <div>    
        设备类型<asp:DropDownList ID="ddListEQIPMENT_TYPE" runat="server" Height="25px" Width="200px" OnSelectedIndexChanged="ddListEQIPMENT_TYPE_SelectedIndexChanged" AutoPostBack="True">
                
            </asp:DropDownList>
    </div>
        <div>    
        设备型号<asp:DropDownList ID="ddListEQIPMENT_MODEL" runat="server" Height="25px" Width="200px">
                
            </asp:DropDownList>
    </div>
        <div>    
        安装地址<asp:TextBox ID="txtPOSITION" runat="server" Width="335px"></asp:TextBox>    
    </div>
        <div>经度<asp:TextBox ID="txtLONGITUDE" runat="server" Width="100px"></asp:TextBox>    
            纬度<asp:TextBox ID="txtLATITUDE" runat="server" Width="100px"></asp:TextBox>    
        </div>
        <div>接入者<asp:TextBox ID="txtJOINER" runat="server" Width="200px"></asp:TextBox>    
        </div>
        <div>状态<asp:RadioButton ID="rdBtnState1" runat="server" Checked="True" Text="启用" GroupName="State" />
            <asp:RadioButton ID="rdBtnState2" runat="server" Text="停用" GroupName="State" />
            <asp:Label ID="lbJoinTime" runat="server"></asp:Label>
        </div>
        <div>
            <asp:Label ID="lbMessage" runat="server" ForeColor="Red"></asp:Label>
        </div>
        <div></div>
        <div>    
            <asp:Button ID="btnSave" runat="server" Height="25px" Text="保存" Width="100px" OnClick="btnSave_Click" />
    </div>
    </div>
    </form>
</body>
</html>
