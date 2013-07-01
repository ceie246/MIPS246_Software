<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Assembler.aspx.cs" Inherits="Assembler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">Mips246 汇编器</h1>
        </div>
        <div class="row">
            <div class="span2 offset6" >
                <label class="radio inline">
                    <input type="radio" name="displayformat" id="hex_format" value="HEX" checked="checked" />HEX
                </label>
                <label class="radio inline">
                    <input type="radio" name="displayformat" id="bin_format" value="BIN" />BIN
                </label>
            </div>            
        </div>
        <div class="row-fluid" id="assemble_codearea">
            <div class="span6">
                <textarea class="span12" rows="25" id="source_code_area" placeholder="源代码..."></textarea>
            </div>
            <div class="span6">
                <textarea class="span12" rows="25" id="target_code_area" placeholder="机器码..." readonly="readonly"></textarea>
            </div> 
        </div>
        <div class="row-fluid" id="button _area">
            <button class="btn btn-info span1" type="button">保存</button>
            <button class="btn btn-info span1" type="button">载入</button>
            <button class="btn btn-danger span1" type="button">清除</button>
            <button class="btn btn-info span1 offset3" type="button">保存</button>
            <button class="btn btn-primary span2 offset3 btn-large" type="button">编译</button>
        </div>
    </div>
</asp:Content>

