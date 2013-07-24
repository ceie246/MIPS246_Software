function SaveAsmFile() {
    var text = document.getElementById("source_code_area").value;
    var data = "data:x-application/text," + encodeURIComponent(text);
    window.open(data);
}

function ClearSourceBox() {
    $(source_code_area).val("");
}