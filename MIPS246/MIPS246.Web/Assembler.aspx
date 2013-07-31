<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Assembler.aspx.cs" Inherits="Assembler" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="PageBody" Runat="Server">
    <div class="container">
        <div id="subtitle">
            <h1 class="mips246font">Mips246 汇编器</h1>
        </div>
        <div class="row">
            <div class="span2 offset6">
                <label class="radio inline">
                    <input type="radio" name="displayformat" id="hex_format" value="HEX" checked="checked" onchange="DisplayFormatChange()"/>HEX
                </label>
                <label class="radio inline">
                    <input type="radio" name="displayformat" id="bin_format" value="BIN" onchange="DisplayFormatChange()"/>BIN
                </label>
            </div>
            <div class="span1 offset3">
                <label class="checkbox" onchange="DisplayFormatChange()">
                    <input type="checkbox" name="hasAddress" id="hasAddress" checked="checked"/>Address
                </label>                
            </div>            
        </div>
        <div class="row-fluid" id="assemble_codearea">
            <div class="span6">
                <textarea class="span12" rows="20" id="source_code_area" placeholder="源代码..."></textarea>
            </div>
            <div class="span6 error">
                <textarea class="span12 " rows="20" id="target_code_area" placeholder="机器码..."></textarea>
            </div> 
        </div>
        <div class="row-fluid" id="button _area">
            <button class="btn btn-info span1" type="button" onclick="SaveSourceCode()">保存</button>
            <button class="btn btn-info span1" type="button">载入</button>
            <button class="btn btn-danger span1" type="button" onclick="ClearSourceBox()">清除</button>
            <a href="#save_option_popup_window" class="btn btn-info span1 offset3" data-toggle="modal">保存</a>
            <button class="btn btn-primary span2 offset3 btn-large" type="button" onclick="Assemble()">编译</button>
        </div>
    </div>
    <div id="save_option_popup_window" class="modal hide fade" tabindex="-1" role="dialog" aria-labelledby="save_option_popup_window" aria-hidden="true">
          <div class="modal-header">
                  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                  <h3>保存选项</h3>
          </div>
          <div class="modal-body">
              <div class="span2">
                  <label class="radio inline">
                      <input type="radio" name="OutputFormat" id="OutputTXT" value="TXT" checked="checked"/>Txt文件
                  </label>
                  <label class="radio inline">
                      <input type="radio" name="OutputFormat" id="OutputTXTRadio2" value="COE" />Coe文件
                  </label>
              </div>
          </div>
          <div class="modal-footer">
                  <a href="#" class="btn" onclick="CloseSaveTargetPopUp()">关闭</a>
                  <a href="#" class="btn btn-info btn-large" onclick="SaveTargetCode()">保存</a>
          </div>
    </div>
</asp:Content>

