$(document).ready(function () {



    LoadBookOffers();

    $("#createBookOffer form").submit(function (event) {
        event.preventDefault();

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
        }
       
        
    });

    $(document).on('click', '.edit-book-offer', function () {
        const offerId = $(this).data('offer-id');
        
        $.ajax({
            url: `/api/bookoffers/${offerId}`, 
            type: 'GET',
            success: function (data) {
                $("#editBookOffer form input[name='Id']").val(data.id);
                $("#editBookOffer form select[name='Type']").val(data.type);
                $("#editBookOffer form input[name='Price']").val(data.price);
                $("#editBookOffer form select[name='Status']").val(data.status);
                $("#editBookOfferModal").modal('show');
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
                    toastr["error"]("Unable to fetch offer details");
                }
            }
        });
    });


    $("#editBookOffer form").submit(function (event) {
        event.preventDefault();

        var offerId = $("#bookOfferId").val();
        if ($('#editBookOffer form')[0].checkValidity()) {
            $.ajax({
                url: `/api/bookoffers/${offerId}`,
                type: "PUT",
                data: $(this).serialize(),
                success: function (data) {

                    toastr["success"]("Book offer edited");
                    LoadBookOffers();
                    $("#editBookOffer").modal('hide')
                    $("#editBookOffer form")[0].reset();

                },
                error: function () {
                    toastr["error"]("Something went wrong");
                }
            });
        }
    });

    
});