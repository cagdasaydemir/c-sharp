$(document).ready(function () {
    tinymce.init({
        selector: 'textarea',
        plugins: 'anchor autolink charmap codesample emoticons lists media table visualblocks wordcount',
        toolbar: 'undo redo | blocks fontfamily fontsize | bold italic underline strikethrough | link image media table mergetags | align lineheight | numlist bullist indent outdent',
    });

    $("#blogPostForm").submit(function (event) {
        var editorContent = tinymce.get('tinyEditor').getContent();
        var title = $("#Title").val();
        var titleErrorSpan = $("#validationMessageTitle");

        if (!editorContent.trim()) {
            if (!$('#editor-error-message').length) {
                $('<span id="editor-error-message" class="text-danger">The Editor is empty</span>').insertAfter($('#validationMessageContent'));
            }
            event.preventDefault();
        } else {
            $('#editor-error-message').remove();
        }

        if (!title.trim()) {
            titleErrorSpan.text("The Title field is required");
            event.preventDefault();
        } else {
            titleErrorSpan.text("");
        }
    });
});