<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MIPS246Book.aspx.cs" Inherits="MIPS246Book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle"><h1 class="mips246font">实验指导书</h1></div>
        <div>
            <%=bookTable %>
        </div>
    </div>    
</asp:Content>

