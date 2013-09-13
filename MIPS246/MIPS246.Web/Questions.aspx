<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Questions.aspx.cs" Inherits="question" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div id="subtitle">
       <h1 class="mips246font">常见问题</h1>
    </div>
    <div class="container">
        <%=questionList %>
    </div>
</asp:Content>

