<%@ Page Language="C#" AutoEventWireup="true" CodeFile="signin.aspx.cs" Inherits="signin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Sign in - MIPS246</title>
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
            <h2 class="form-signin-heading">请登陆</h2>
             <asp:TextBox ID="studentIDBox" runat="server" placeholder="学号" OnTextChanged="studentIDBox_TextChanged"></asp:TextBox>
            <br />
             <asp:TextBox ID="passwordBox" runat="server" placeholder="密码" TextMode="Password" OnTextChanged="passwordBox_TextChanged"></asp:TextBox><br />
            <asp:Button ID="signinButton" runat="server" Text="登陆" class="btn btn-large btn-primary" type="submit" OnClick="signinButton_Click" />
            <asp:Label ID="warningLabel" runat="server" ForeColor="#CC0000" Text="用户名或密码错误!" Visible="False"></asp:Label>
        </div>
        
    </div> 
    </form>
</body>
</html>
