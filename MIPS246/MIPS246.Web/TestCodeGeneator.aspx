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
                <table class="table table-hover table-striped table-condensed">
                    <tr>
                        <td>代码数量</td>
                        <td><input type="text" class="input-mini" id="codenum" placeholder="50" onclick="UpdateDefaultValue()"/></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><button class="btn btn-primary btn-large" type="button" onclick="GeneatorCode()">生成</button></td>
                    </tr>
                </table>
            </div>
            <div class="span9">
                <textarea class="span12" rows="25" id="test_code_area" placeholder="测试代码序列..."></textarea>
                <div class="span3">
                    <label class="radio inline">
                        <input type="radio" name="displayformat" id="mnemonic_format" value="MNE" checked="checked" onchange="GeneatorCodeDisplayFormatChange()" />Mnemonic
                    </label>
                    <label class="radio inline">
                        <input type="radio" name="displayformat" id="hex_format" value="HEX" onchange="GeneatorCodeDisplayFormatChange()"/>HEX
                    </label>
                    <label class="radio inline">
                        <input type="radio" name="displayformat" id="bin_format" value="BIN" onchange="GeneatorCodeDisplayFormatChange()"/>BIN
                    </label>
                </div>
            </div>            
        </div>
    </div>
</asp:Content>

