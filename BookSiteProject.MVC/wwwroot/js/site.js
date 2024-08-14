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

    $.ajax({
        url: `/api/books/${bookEncodedName}/bookoffers`,
        type: 'GET',
        success: function (data) {

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

const LoadCategories = () => {
    const container = $("#categories");
   
    $.ajax({
        url: `/api/categories`,
        type: 'GET',
        success: function (data) {
       

            if (!data.length) {
                container.html("No categories");
            } else {
                RenderCategories(data, container);
            }
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
};

const RenderCategories = (categories, container) => {
    container.empty();

   

    for (const item of categories) {


        var styling;
        var listField;
        var status;

        if (item.active == true) {
            styling = "bg-success text-white";
            listField = "Unlist";
            status = false;
        }
        else {
            styling = "bg-secondary text-white";
            listField = "ListBack";
            status = true;
        }

        

        if (typeof isAdmin === 'undefined') {
            isAdmin = false;
        }

        const adminButtons = isAdmin ? `<td class="text-end align-middle">
                        <form id="editForm" style="display:inline;">
                            <input type="hidden" name="CategoryName" value="${item.name}" />
                            <button type="button" class="btn btn-danger btn-sm edit-category" data-old-name="${item.name}">Edit</button>
                        </form>


                        <form id="deleteForm" style="display:inline;">
                            <input type="hidden" name="CategoryName" value="${item.name}" />
                            <button type="button" class="btn btn-danger btn-sm" onclick="confirmDeleteModal('${item.name}');">Delete</button>
                        </form>
                        

                          <form id="changeStatusForm" style="display:inline;">
                            <input type="hidden"  name="CategoryName" value="${item.name}" />
                            <input type="hidden"  name="Status" value=${status.toString().toLowerCase()} />
                            <button type="button"  class="btn btn-warning btn-sm" onclick="confirmChangeStatusModal('${item.name}','${listField}','${status.toString().toLowerCase()}');">${listField}</button>
                          </form>
                          
                    
                </td>
    ` : '';

        container.append(`
               
             <div class="card mb-3">
               <div class="card-body ${styling}">
                <table class="table">
                    <tr>
                        <td>
                              ${item.name}
                        </td>
                            ${adminButtons}
                    </tr>
                </table>
              </div >
             </div >
      
            `);
    }
};