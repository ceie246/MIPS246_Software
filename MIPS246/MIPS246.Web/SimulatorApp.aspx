<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="SimulatorApp.aspx.cs" Inherits="Simulator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <form id="form1" runat="server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">Mips246 模拟器</h1>
        
            <p><b>当前版本：</b><%=Version %></p> 
            <p><b>更新日期：</b><%=UpdateTime %></p> 
            <p><b>软件手册：</b><a class="btn btn-primary" href="<%=ManualUrl %>">下载</a></p> 
            <p><b>安装包：</b><a class="btn btn-primary" href="<%=DownloadUrl %>">下载</a></p> 
            <p><%=update %></p>
        </div>
   </div>
        </form>

</asp:Content>

