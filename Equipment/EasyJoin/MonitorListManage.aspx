<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MonitorListManage.aspx.cs" Inherits="EasyJoin.MonitorListManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .textOver{white-space:nowrap; overflow:hidden; text-overflow:ellipsis;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <div>
        <asp:Button ID="btnAdd" runat="server" OnClick="btnAdd_Click" Text="添加" />
    </div>
    <div>
    <asp:GridView ID="gridViewMonitorList" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow"
                        BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"
                        Width="100%" DataKeyNames="ID" OnRowCommand="gv_RowCommand" OnRowDeleting="gv_RowDeleting" OnRowDataBound="gv_RowDataBound">
                        <FooterStyle BackColor="Tan" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="编号">
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EQUIPMENT_TYPE_NAME" HeaderText="类型" >
                                <ItemStyle HorizontalAlign="Left" Width="50px"/>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>                            
                            <asp:TemplateField HeaderText="型号">   
                                <ItemTemplate>                            
                                    <div class="textOver" style="width:100px;" 
                                        title="<%#Eval("EQUIPMENT_MODEL_NAME")%>"> <%#Eval("EQUIPMENT_MODEL_NAME")%> </div>  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="名称">   
                                <ItemTemplate>                            
                                    <div class="textOver" style="width:100px;" 
                                        title="<%#Eval("Name")%>"> <%#Eval("Name")%> </div>  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="经纬度">
                                <ItemTemplate>                            
                                    <div class="textOver" style="width:100px;" title="<%#Eval("LatLong")%>"> <%#Eval("LatLong")%> </div>  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>           
                            <asp:TemplateField HeaderText="安装地址">   
                                <ItemTemplate>                            
                                    <div class="textOver" style="width:150px;" title="<%#Eval("POSITION")%>"> <%#Eval("POSITION")%> </div>  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                            
                            <asp:TemplateField HeaderText="接入时间">
                                <ItemTemplate>                            
                                    <div class="textOver" style="width:100px;" title="<%#Eval("JOIN_TIME")%>"> <%#Eval("JOIN_TIME")%> </div>  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>                             
                            <asp:BoundField DataField="JOINER" HeaderText="接入" >
                                <ItemStyle HorizontalAlign="Left" Width="50px"/>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="STATE" HeaderText="状态" >
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>                            
                            <asp:ButtonField CommandName="Update" HeaderText="修改" Text="修改">
                                <ItemStyle HorizontalAlign="Left" Width="50px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:ButtonField>
                            <asp:CommandField HeaderText="删除" ShowDeleteButton="True">
                                <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:CommandField>
                        </Columns>
                        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <HeaderStyle BackColor="Tan" Font-Bold="True" />
                        <AlternatingRowStyle BackColor="PaleGoldenrod" />
                    </asp:GridView>
    </div>
    <div>

    </div>
</div>
    </form>
</body>
</html>
