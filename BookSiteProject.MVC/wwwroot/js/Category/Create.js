$(document).ready(function () {

    LoadCategories();

    $("#createCategory form").submit(function (event) {
        event.preventDefault();
      
        if ($('#createCategory form')[0].checkValidity()) {
           
            $.ajax({
                url: 'api/categories',
                type: 'POST',
                data: $(this).serialize(),

                success: function (data) {
                    toastr["success"]("Created new category");
                    LoadCategories();
                    $("#createCategory").modal('hide');
                    $("#createCategory form")[0].reset();
                },
                error: function (xhr) {
                    if (xhr.responseJSON) {
                        var errors = xhr.responseJSON;

                        for (var property in errors) {
                            if (errors.hasOwnProperty(property)) {
                                var messages = errors[property];
                                if (Array.isArray(messages)) {
                                    messages.forEach(function (message) {
                                        toastr["error"](message);
                                    });
                                } else {   
                                    toastr["error"](messages);
                                }
                            }
                        }
                    } else {
                        toastr["error"]("Something went wrong");
                    }
                }
            });
        }
        });

});