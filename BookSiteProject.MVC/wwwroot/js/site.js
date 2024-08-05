const RenderBookOffers = (offers, container) => {
    container.empty();

    const OfferType = {
        0: 'Sale',
        1: 'Rental'
    };

    const OfferStatus = {
        0: 'Unavailable',
        1: 'Available'
    };
  
    for (const offer of offers) {
        const date = new Date(offer.dateOfCreation);
        const formattedDate = `${date.toLocaleDateString('en-US', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        })} at ${date.toLocaleTimeString('en-US', {
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit'
        })}`;

        if (typeof isAdmin === 'undefined') {
            isAdmin = false;
        }

        const adminButtons = isAdmin ? `
        <a class="btn btn-danger delete-book-offer" data-offer-id="${offer.id}">Delete</a>
        <a class="btn btn-info edit-book-offer" data-offer-id="${offer.id}" data-bs-toggle="modal" data-bs-target="#editBookOffer">Edit</a>
    ` : '';

        container.append(`
                <div class="card border-secondary mb-3">
                    <div class="card-header">Type of offer: ${OfferType[offer.type]}</div>
                    <div class="card-body">
                        <h5 class="card-title">Submitted at: ${formattedDate}</h5> 
                        <h5 class="card-title">Price: ${offer.price}</h5> 
                        <h5 class="card-title">Offer Status: ${OfferStatus[offer.status]}</h5> 
                    </div>
                    ${adminButtons}
                 
                </div>
            `);
    }
};

const LoadBookOffers = () => {
    const container = $("#offers");
    const bookEncodedName = container.data("encodedName");

    console.log("Book Encoded Name:", bookEncodedName);

    $.ajax({
        url: `/Book/${bookEncodedName}/BookOffer`,
        type: 'GET',
        success: function (data) {
            console.log("Data received:", data);

            if (!data.length) {
                container.html("No book offers as of yet");
                } else {
                RenderBookOffers(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
};
