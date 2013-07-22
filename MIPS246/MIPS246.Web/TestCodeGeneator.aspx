<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TestCodeGeneator.aspx.cs" Inherits="TestCodeGeneator" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">Mips246 测试代码生成器</h1>
        </div>
        <div class="row-fluid">
            <div class="span3">
                <table class="table table-hover table-bordered">
                    <tr>
                        <td>代码数量</td>
                    </tr>
                    <tr>
                        <td>代码数量</td>
                    </tr>
                    <tr>
                        <td>代码数量</td>
                    </tr>
                    <tr>
                        <td>代码数量</td>
                    </tr>
                </table>
            </div>
            <div class="span9">
                <textarea class="span12" rows="25" id="test_code_area" placeholder="测试代码序列..." readonly="readonly"></textarea>
            </div>
            
        </div>
    </div>
</asp:Content>

