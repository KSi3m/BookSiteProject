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
});