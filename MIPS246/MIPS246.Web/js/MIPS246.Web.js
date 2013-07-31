function SaveAsmFile() {
    var text = document.getElementById("source_code_area").value;
    var data = "data:x-application/text," + encodeURIComponent(text);
    window.open(data);
}

function ClearSourceBox() {
    $(source_code_area).val("");
}

function Assemble() {
    var sourcecode = $(source_code_area).val();
    var displayFormat = $("input[name=displayformat]:radio:checked").val();
    var hasAddress = $("input[name=hasAddress]:checkbox:checked").attr("checked");
    var args = "{'sourceCode':'" + sourcecode + "', 'displayFormat': '" + displayFormat + "' , 'hasAddress': '" + hasAddress + "'}";
    $.ajax(
        {
            type: "Post",
            url: "AssemblerRequest.aspx/Assemble",
            contentType: "application/json; charset=utf-8",
            data: args,
            dataType: "json",
            success: 
                function (data) {
                    UpdateTargetCode(data.d);
                }
        })
}

function UpdateTargetCode($data)
{
    $(target_code_area).val($data);
}

function SaveTargetCode() {
    var sourcecode = $(source_code_area).val();
    var displayFormat = $("input[name=displayformat]:radio:checked").val();
    var outputFormat = $("input[name=OutputFormat]:radio:checked").val();
    var args = "{'sourceCode':'" + sourcecode + "', 'displayFormat': '" + displayFormat + "' , 'outputFormat':'" + outputFormat + "'}";
    $.ajax(
        {
            type: "Post",
            url: "AssemblerRequest.aspx/Assemble",
            contentType: "application/json; charset=utf-8",
            data: args,
            dataType: "json",
            success:
                function (data) {
                    UpdateTargetCode(data.d);
                }
        })
    $.ajax(
        {
            type: "Post",
            url: "AssemblerRequest.aspx/SaveTargetCode",
            contentType: "application/json; charset=utf-8",
            data: args,
            dataType: "json",
            success:
                function (data) {
                    window.open("./AssemblerOutput/" + data.d);
                    $('#save_option_popup_window').modal('toggle');
                }
        })
}

function CloseSaveTargetPopUp() {
    $('#save_option_popup_window').modal('toggle');
}

function DisplayFormatChange() {
    if ($(target_code_area).val() != "") {
        Assemble();
    }
}

function SaveSourceCode() {
    var sourcecode = $(source_code_area).val();
    var args = "{'sourceCode':'" + sourcecode + "'}";
    $.ajax(
        {
            type: "Post",
            url: "AssemblerRequest.aspx/SaveSourceCode",
            contentType: "application/json; charset=utf-8",
            data: args,
            dataType: "json",
            success:
                function (data) {
                    window.open("./AssemblerOutput/" + data.d);
                }
        })
}