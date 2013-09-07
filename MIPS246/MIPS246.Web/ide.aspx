<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ide.aspx.cs" Inherits="ide" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle"><h1 class="mips246cnfont">实验指导书</h1></div>
        <div>
            <%=ideTable %>
        </div>
    </div>    
</asp:Content>

