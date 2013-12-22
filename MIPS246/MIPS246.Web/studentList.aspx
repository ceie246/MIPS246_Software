<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="studentList.aspx.cs" Inherits="studentList" %>

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
                    <th>NO.</th>
                    <th>学号</th>
                    <th>姓名</th>
                    <th>性别</th>
                    <th>专业</th>
                    <th>开发板编号</th>
                    <th>操作</th>
                </tr>
            </thead>
            <%=studentInfo %>
            <tr>
                <td colspan="6"><a class="btn btn-primary" href="AddUser.aspx">添加用户</a></td>
            </tr>
            
        </table>
       </div>
</asp:Content>

