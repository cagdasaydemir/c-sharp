$(document).ready(function () {
    $(document).on('click', '.deleteButtonComment', function (e) {
        e.preventDefault();
        var deleteUrl = $(this).data('url');
        var currentUrl = $(this).data('current-url');
        DeleteComment(deleteUrl, currentUrl);
    });
});

function DeleteComment(url, currentUrl) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    window.location.href = currentUrl;
                }
            })
        }
    })
}