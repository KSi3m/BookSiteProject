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
        container.append(`
                <div class="card border-secondary mb-3">
                    <div class="card-header">Type of offer: ${OfferType[offer.type]}</div>
                    <div class="card-body">
                        <h5 class="card-title">Submitted at: ${formattedDate}</h5> 
                        <h5 class="card-title">Price: ${offer.price}</h5> 
                        <h5 class="card-title">Offer Status: ${OfferStatus[offer.status]}</h5> 
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
                } else {
                RenderBookOffers(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
};
