
/*$(document).ready(function () {

    LoadCategories();

});*/

function confirmDeleteModal(data) {
    $('#confirmDeleteModal').modal('show');
    var deleteButton = document.querySelector('.delete-category');
    deleteButton.setAttribute('onclick', 'submitDeleteForm("'+data+'")');
}



function submitDeleteForm(name) {
    $.ajax({
        url: `api/categories/${name}`, 
        type: 'DELETE', 
 
        success: function (data) {
            toastr["success"]("Category deleted");
            LoadCategories();
            $("#confirmDeleteModal").modal('hide');
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
    
}