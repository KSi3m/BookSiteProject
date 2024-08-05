$(document).ready(function () {



    LoadBookOffers();

    $("#createBookOffer form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),

            success: function (data) {
                toastr["success"]("Created new book offer");
                LoadBookOffers();
                $("#createBookOffer").modal('hide');
                $("#createBookOffer form")[0].reset();
            },
            error: function () {
                toastr["error"]("Something went wrong");
            }
        });
    });

    $(document).on('click', '.edit-book-offer', function () {
        const offerId = $(this).data('offer-id');
        $.ajax({
            url: `/BookOffer/${offerId}`, 
            type: 'GET',
            success: function (data) {
                $("#editBookOffer form input[name='Id']").val(data.id);
                $("#editBookOffer form select[name='Type']").val(data.type);
                $("#editBookOffer form input[name='Price']").val(data.price);
                $("#editBookOffer form select[name='Status']").val(data.status);
                $("#editBookOfferModal").modal('show');
            },
            error: function () {
                
                toastr["error"]("Unable to fetch offer details");
            }
        });
    });


    $("#editBookOffer form").submit(function (event) {
        event.preventDefault();

        var offerId = $("#bookOfferId").val();

        $.ajax({
            url: `/BookOffer/${offerId}`,
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
    });

    
});