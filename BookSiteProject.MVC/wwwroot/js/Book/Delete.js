

$(document).on('click', '.delete-book-offer', function (event) {
        event.preventDefault();

        var button = $(this);
        var offerId = button.data('offer-id'); 
  
   
        if (confirm('Are you sure you want to delete this book offer?')) {
            $.ajax({
                url: `/api/bookoffers/${offerId}`,
                type: 'DELETE',
                success: function (data) {
                    toastr["success"]("Book offer deleted");
                    LoadBookOffers();
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                }
            });
        }
});


