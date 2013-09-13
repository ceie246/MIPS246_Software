<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AddQuestion.aspx.cs" Inherits="AddQuestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <form id="form1" runat="server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">提交问题</h1>
        </div>
        <asp:TextBox ID="QuestionBox" runat="server" placeholder="问题"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="年份">
        </asp:Label><asp:ListBox ID="YearBox" runat="server" Rows="1">
            <asp:ListItem>2013</asp:ListItem>
            <asp:ListItem>2014</asp:ListItem>
            <asp:ListItem>2015</asp:ListItem>
        </asp:ListBox><br />
        <asp:TextBox ID="AnswerBox" runat="server" placeholder="回答" TextMode="MultiLine"></asp:TextBox><br />
        <asp:Button ID="SubmitButton" runat="server" Text="提交" OnClick="SubmitButton_Click" />
    </div>
        
    </form>
</asp:Content>

