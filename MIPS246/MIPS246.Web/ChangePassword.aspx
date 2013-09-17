<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>修改密码 - MIPS246</title>
    <link href="./css/bootstrap.css" rel="stylesheet" />
    <link href="./css/bootstrap-responsive.css" rel="stylesheet" />
    <link href="./css/mips246.css" rel="stylesheet" /> 
    <link href='http://fonts.googleapis.com/css?family=Advent+Pro:500' rel='stylesheet' type='text/css' />
    <script src="./js/jquery-1.10.1.min.js"></script>   
    <script src="./js/bootstrap.js"></script>    
    <script src="./js/MIPS246.Web.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="form-signin">
    <div class="sign-container">
        <div>
            <h2 class="form-signin-heading">修改密码</h2>
            <asp:TextBox ID="currentPasswordBox" runat="server" placeholder="密码" TextMode="Password" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="currentPasswordBox" ErrorMessage="密码为空" Font-Names="微软雅黑" ForeColor="#CC0000"></asp:RequiredFieldValidator>
             <asp:TextBox ID="PasswordBox" runat="server" placeholder="密码" TextMode="Password" ></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="PasswordBox" ErrorMessage="RequiredFieldValidator" Font-Names="微软雅黑" ForeColor="#CC0000">密码为空</asp:RequiredFieldValidator>
             <asp:TextBox ID="RepasswordBox" runat="server" placeholder="重复密码" TextMode="Password"></asp:TextBox>
            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="RepasswordBox" ControlToValidate="PasswordBox" ErrorMessage="CompareValidator" Font-Names="微软雅黑" ForeColor="#CC0000">密码不一致</asp:CompareValidator>
            <asp:Button ID="ChangeButton" runat="server" Text="修改" class="btn btn-large btn-warning" type="submit" OnClick="ChangeButton_Click" />
            <asp:Label ID="warningLabel" runat="server" ForeColor="#CC0000" Text="用户名或密码错误!" Visible="False"></asp:Label>
        </div>        
    </div> 
    </form>
</body>
</html>
