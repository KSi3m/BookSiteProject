$(document).ready(function () {

    var encodedName = "";
    $(document).on('click', '#addBookOfferButton', function () {
        encodedName = $(this).data("encoded-name");
    });

    $("#createBookOffer form").submit(function (event) {
        event.preventDefault();
        $(this).find('input[name="BookEncodedName"]').val(encodedName);

        if ($('#createBookOffer form')[0].checkValidity()) {
            $.ajax({
                url: `/api/books/bookoffers`,
                type: 'POST',
                data: $(this).serialize(),

                success: function (data) {
                    toastr["success"]("Created new book offer");
                    LoadBookOffers();
                    $("#createBookOffer").modal('hide');
                    $("#createBookOffer form")[0].reset();
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
})