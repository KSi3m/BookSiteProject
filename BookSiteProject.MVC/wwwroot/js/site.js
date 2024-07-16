const RenderBookOffers = (offers, container) => {
    container.empty();

    const OfferType = {
        0: 'Sale',
        1: 'Rental'
    };

    const OfferStatus = {
        0: 'Available',
        1: 'Unavailable'
    };

    for (const offer of offers) {
        container.append(`
                <div class="card border-secondary mb-3">
                    <div class="card-header">${OfferType[offer.type]}</div>
                    <div class="card-body">
                        <h5 class="card-title">${offer.dateOfCreation}</h5> 
                        <h5 class="card-title">${offer.price}</h5> 
                        <h5 class="card-title">${OfferStatus[offer.status]}</h5> 
                    </div>
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
        type: 'get',
        success: function (data) {
            console.log("Data received:", data);

            if (!data.length) {
                container.html("No book offers as of yet");
                console.log("DUPP");
            } else {
                RenderBookOffers(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
};
