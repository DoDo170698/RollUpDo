function showNoty(text, type, layout, timeout) {
    toastr.options = {
        "closeButton": true,
        "debug": false,
        "positionClass": "toast-bottom-right",
        "onclick": null,
        "showDuration": "1000",
        "hideDuration": "1000",
        "timeOut": timeout,
        "extendedTimeOut": "1000",
        "showEasing": "swing",
        "hideEasing": "linear",
        "showMethod": "fadeIn",
        "hideMethod": "fadeOut"
    }
    toastr[type](text, "Thông báo");
}


function reloadPage() {
    App.blockUI({
        target: '#ajaxContent',
        boxed: true,
        animate: true,
        message: "Vui lòng chờ"
    });
    $.ajax({
        url: tableRoute,
        data: {  },
        contentType: 'application/json',
        dataType: 'html',
        type: 'GET',
        cache: false,
        success: function (data) {
            $('#ajaxContent').html(data);
            initEvents(); 
            $('#sample_editable_1').DataTable();
        },
        error: function (errorThrown) {
            alert(messages.ErrorOccurred + errorThrown);
        },
        complete: function (result) {
            App.unblockUI();
        }
    });
}

function initEvents() {

    $('.delete').click(function () {
        if (!confirm("Bạn có chắc chắn muốn xóa")) {
            return;
        }
        var nRow = $(this).parents('tr')[0];
        var deleteId = $(nRow).attr('data-objectid');
        $.ajax({
            type: 'POST',
            url: deleteRoute,
            dataType: 'json',
            data: { id: deleteId },
            success: function (result) {
                if (result.IsSuccess) {
                    showNoty(result.Message, 'success', 'center', 5000);
                    var table = $('#sample_editable_1').DataTable();
                    table.row($(this).closest('tr')[0]).remove();
                } else {
                    showNoty(result.Message, 'error', 'center', 5000);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert(messages.ErrorOccurred + errorThrown);
            }
        });
    });
    
}