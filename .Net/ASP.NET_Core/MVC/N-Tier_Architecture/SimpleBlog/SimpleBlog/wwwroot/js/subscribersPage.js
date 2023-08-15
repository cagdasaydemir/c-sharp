var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "language": {
            "emptyTable": 'No data available in table. Add Subscriber from <a href="/subscriber/subscribe" class="btn btn-link p-0">click here</a>.'
        },
        "ajax": { url: '/subscriber/GetAll' },
        "columns": [
            { data: 'name', "width": "15%" },
            { data: 'email', "width": "15%" },
            {
                data: 'dateSubscribed',
                "width": "15%",
                render: function (data) {
                    return new Date(data).toISOString().split('T')[0];
                }
            },
            {
                data: 'dateUnSubscribed',
                "width": "15%",
                render: function (data) {
                    return data ? new Date(data).toISOString().split('T')[0] : "-";
                }
            },

            {
                data: 'isSubscribed',
                "width": "15%",
                render: function (data, type, row) {
                    return `<a href="#" class="btn ${data ? 'text-success' : 'text-danger'} btn-sm w-100 toggle-subscription"
                            data-id="${row.id}" data-subscribed="${data}">
                        ${data ? `<i style="font-size: 2rem;" class="bi bi-toggle-on text-success"></i>`
                            : `<i style="font-size: 2rem;" class="bi bi-toggle-off text-danger"></i>`}
                    </a>`;
                }
            },
            {
                data: null,
                render: function (data) {
                    return `<div class="w-100 d-flex justify-content-center" >
                       
                        <a class="btn btn-danger" onclick="deleteSubscriber(${data.id})">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    </div>`;
                },
                "width": "15%"
            }
        ],
        "columnDefs": [
            {
                "targets": [0, 1, 2, 3, 4, 5],
                "className": "align-middle"
            }
        ]
    });
}

function deleteSubscriber(id) {
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
                url: `/subscriber/delete/${id}`,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            });
        }
    });
}

$('#tblData').on('click', '.toggle-subscription', function (e) {
    e.preventDefault();
    var id = $(this).data('id');
    var isSubscribed = $(this).data('subscribed');

    $.ajax({
        url: `/subscriber/toggleSubscription?id=${id}`,
        type: 'POST',
        success: function (data) {
            if (data.success) {
                dataTable.ajax.reload();
                toastr.success(data.message);
            } else {
                toastr.error(data.message);
            }
        }
    });
});
