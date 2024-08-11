$(document).ready(function () {

    LoadCategories();

    $("#createCategory form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: 'api/Category/Create',
            type: 'POST',
            data: $(this).serialize(),

            success: function (data) {
                toastr["success"]("Created new category");
                LoadCategories();
                $("#createCategory").modal('hide');
                $("#createCategory form")[0].reset();
            },
            error: function () {
                toastr["error"]("Something went wrong");
            }
        });
    });
});