$(document).ready(function () {
    var oldName = "";
    $(document).on('click', '.edit-category', function () {
        oldName = $(this).data('old-name');
        $.ajax({
            url: `/api/Category/${oldName}`,
            type: 'GET',
            success: function (data) {
                $("#editCategory form input[name='NewName']").val(data.name);
                $("#editCategory form input[name='OldName']").val(oldName);
            
                $("#editCategory").modal('show');
            },
            error: function () {

                toastr["error"]("Unable to fetch offer details");
            }
        });
    });


    $("#editCategory form").submit(function (event) {
        event.preventDefault();
        var formData = $(this).serialize();

        $.ajax({
            url: `/api/Category/Edit`,
            type: "POST",
            data: formData,
            success: function (data) {

                toastr["success"]("Category edited");
                LoadCategories();
                $("#editCategory").modal('hide')
                $("#editCategory form")[0].reset();

            },
            error: function (xhr) {
                if (xhr.responseJSON) {
                    var errors = xhr.responseJSON;

                    for (var property in errors) {
                        if (errors.hasOwnProperty(property)) {
                            var messages = errors[property];
                            messages.forEach(function (message) {
                                toastr["error"](message);
                            });
                        }
                    }
                } else {
                    toastr["error"]("Something went wrong");
                }
            }
        });
    });


});