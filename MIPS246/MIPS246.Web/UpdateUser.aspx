<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateUser.aspx.cs" Inherits="UpdateUser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
     <form id="form1" runat="server">
      <div class="container">
    <div id="subtitle">
            <h1 class="mips246font">用户更新
            </h1>
        </div>
          <div>
            <table class="table table-striped">
                <tr>
                    <td class="userinfoLeft">学号</td>
                    <td class="userinfoRight">
                        <asp:TextBox ID="studentIDBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="userinfoLeft">姓名</td>
                    <td class="userinfoRight">
                        <asp:TextBox ID="nameBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="userinfoLeft">性别</td>
                    <td class="userinfoRight">
                        <asp:RadioButton ID="MaleRadioButton" runat="server" GroupName="Sex" Text="男" Checked="true" />                        
                        <asp:RadioButton ID="FemaleRadioButton" runat="server" GroupName="Sex" Text="女" />   
                    </td>
                </tr>
                <tr>
                    <td class="userinfoLeft">专业</td>
                    <td class="userinfoRight">
                        <asp:TextBox ID="majorBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="userinfoLeft">开发板编号</td>
                    <td class="userinfoRight">
                        <asp:TextBox ID="boardBox" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="userinfoLeft">密码</td>
                    <td class="userinfoRight">
                        <asp:TextBox ID="PasswordBox" runat="server" TextMode="Password" placeholder="留空不更改"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="UpdateButton" runat="server" Text="更新" class="btn btn-large btn-primary" type="submit" OnClick="UpdateButton_Click" /></td>
                    <td></td>
                </tr>
            </table>
        </div>
          </div>
      </form>
</asp:Content>

