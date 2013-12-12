<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="UploadHomework.aspx.cs" Inherits="UploadHomework" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
        <form id="form1" runat="server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">上传作业</h1>
        </div>
        <div>
            <table>
                <tr>
                     <td><asp:FileUpload ID="FileUploader" runat="server"/></td>
                   
                    
                </tr>
                 <tr><td><asp:Button ID="UploadButton" runat="server" Text="上传" class="btn btn-large btn-warning" type="submit" OnClick="UploadButton_Click"/></td>
                        
                    </tr>
            </table>
        </div>
    </div>
        </form>
</asp:Content>

