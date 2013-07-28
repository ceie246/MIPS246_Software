function SaveAsmFile() {
    var text = document.getElementById("source_code_area").value;
    var data = "data:x-application/text," + encodeURIComponent(text);
    window.open(data);
}

function ClearSourceBox() {
    $(source_code_area).val("");
}

function Assemble() {
    $sourcecode = $(source_code_area).val();
    $.post(
        "AssemblerRequest.aspx",
        {
            sourcecode: $sourcecode
        },
        UpdateTargetCode($data))
}

function UpdateTargetCode($data)
{
    $(target_code_area).val($data);
}