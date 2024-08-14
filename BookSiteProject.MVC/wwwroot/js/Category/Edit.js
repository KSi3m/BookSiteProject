$(document).ready(function () {
    var oldName = "";
    $(document).on('click', '.edit-category', function () {
        oldName = $(this).data('old-name');
        $.ajax({
            url: `/api/categories/${oldName}`,
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
        var oldName = $(this).find('[name="OldName"]').val();

        $.ajax({
            url: `/api/categories/${oldName}`,
            type: "PUT",
            data: formData,
            success: function (data) {

                toastr["success"]("Category edited");
                LoadCategories();
                $("#editCategory").modal('hide')
                $("#editCategory form")[0].reset();

            },
            error: function () {

                toastr["error"]("Unable to fetch offer details");
            }
        });
    });


});