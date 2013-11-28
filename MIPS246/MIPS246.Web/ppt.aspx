<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ppt.aspx.cs" Inherits="ppt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle"><h1 class="mips246cnfont">PPT</h1></div>
        <div>
            <%=pptTable %>
        </div>
    </div>    
</asp:Content>

