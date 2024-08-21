$(document).ready(function () {

    function GetBookOffers(bookEncodedName) {
        return new Promise((resolve, reject) => {
            $.ajax({
                url: `/api/books/${bookEncodedName}/bookoffers`,
                type: 'GET',
                success: function (data) {
                    if (!data.length) {
                        resolve([]); 
                    } else {
                        resolve(data); 
                    }
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                    reject(new Error("Failed to fetch book offers"));
                }
            });
        });
    }
    function updateList(bookOffers, filterSelect, sortSelect) {
        let filteredOffers = bookOffers;


        const filterValue = filterSelect.value;
        if (filterValue === 'active') {
            filteredOffers = filteredOffers.filter(offer => offer.status === 1);
        } else if (filterValue === 'inactive') {
            filteredOffers = filteredOffers.filter(offer => offer.status === 0);
        }


        const sortValue = sortSelect.value;
        filteredOffers.sort((a, b) => {
            if (sortValue === 'date') {
                return new Date(a.dateOfCreation) - new Date(b.dateOfCreation);
            } else if (sortValue === 'price') {
                return a.price - b.price;
            }
            return 0;
        });

        RenderBookOffers(filteredOffers, $("#offers"));
    }

    const container = $("#offers");
    const bookEncodedName = container.data("encodedName");
    var bookOffers;
    const filterSelect = document.getElementById('filter');
    const sortSelect = document.getElementById('sort'); 

    function InitializeBookOffers() {
        GetBookOffers(bookEncodedName).then(data => {
            bookOffers = data;
            updateList(bookOffers, filterSelect, sortSelect)
        }).catch(error => {
            toastr["error"](error);
        });
    }
    InitializeBookOffers();
 
    filterSelect.addEventListener('change', () => updateList(bookOffers, filterSelect, sortSelect));
    sortSelect.addEventListener('change', () => updateList(bookOffers, filterSelect, sortSelect));


   

});
