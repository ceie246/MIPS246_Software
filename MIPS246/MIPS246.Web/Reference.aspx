﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reference.aspx.cs" Inherits="Reference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle"><h1 class="mips246cnfont">相关资料</h1></div>
        <div>
            <%=ReferenceTable %>
        </div>
    </div>    
</asp:Content>

