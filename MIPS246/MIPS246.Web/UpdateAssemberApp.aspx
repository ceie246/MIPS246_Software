<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UpdateAssemberApp.aspx.cs" Inherits="UpdateAssemberApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
    <div id="subtitle">
            <h1 class="mips246font">更新汇编器程序
            </h1>
        </div>
        <div>
            <table>
                
                <tr>
                     <td><asp:FileUpload ID="FileUploader" runat="server"/></td>
                   
                    
                </tr>
                 
                <tr><asp:FileUpload ID="FileUpload2" runat="server"/></tr>
                <tr><td><asp:Button ID="UploadButton" runat="server" Text="上传" class="btn btn-large btn-warning" type="submit" /></td>
                        
                    </tr>
            </table>
        </div>
</asp:Content>

