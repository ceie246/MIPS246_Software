<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateScore.aspx.cs" Inherits="UpdateScore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <form id="form1" runat="server">
        <div class="container">
    <div id="subtitle">
            <h1 class="mips246font">Mips246 用户列表</h1>
        </div>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>学号</th>
                    <th>姓名</th>
                    <th>作业1</th>
                    <th>作业2</th>
                    <th>作业3</th>
                    <th>作业4</th>
                    <th>作业5</th>
                    <th>作业6</th>
                    <th>作业7</th>
                    <th>作业8</th>
                    <th>作业9</th>
                    <th>作业10</th>
                </tr>
            </thead>
            <tr>
                <td><%=StudentID %></td>
                <td><%=StudentName %></td>
                <td>
                    <asp:TextBox ID="ScoreBox1" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ScoreBox2" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ScoreBox3" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ScoreBox4" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ScoreBox5" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="ScoreBox6" runat="server" Width="50px"></asp:TextBox>
                </td>
                <td><asp:TextBox ID="ScoreBox7" runat="server" Width="50px"></asp:TextBox></td>
                <td><asp:TextBox ID="ScoreBox8" runat="server" Width="50px"></asp:TextBox></td>
                <td><asp:TextBox ID="ScoreBox9" runat="server" Width="50px"></asp:TextBox></td>
                <td><asp:TextBox ID="ScoreBox10" runat="server" Width="50px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="12">
                    <asp:Button ID="UpdateButton" runat="server" Text="更新" class="btn btn-large btn-warning" type="submit" OnClick="UpdateButton_Click" />
                </td>
            </tr>
        </table>
       </div>
    </form>
</asp:Content>

