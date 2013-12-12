<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ScoreList.aspx.cs" Inherits="ScoreList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
    <div id="subtitle">
            <h1 class="mips246font">Mips246 用户列表</h1>
        </div>
        <table class="table table-hover">
            <thead>
                <tr>
                    <th>学号</th>
                    <th>姓名</th>
                    <th>成绩1</th>
                    <th>成绩2</th>
                    <th>成绩3</th>
                    <th>成绩4</th>
                    <th>成绩5</th>
                    <th>成绩6</th>
                    <th>成绩7</th>
                    <th>成绩8</th>
                    <th>成绩9</th>
                    <th>成绩10</th>
                </tr>
            </thead>
            <%=scoreInfo %>
            
        </table>
       </div>
</asp:Content>

