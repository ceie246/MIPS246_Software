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
                    <th>更新</th>
                </tr>
            </thead>
            <%=scoreInfo %>
            
        </table>
       </div>
</asp:Content>

