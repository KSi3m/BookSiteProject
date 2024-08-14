function confirmChangeStatusModal(name,text,status) {
    $('#confirmChangeStatusModal').modal('show');
    var changeButton = document.querySelector('.change-status-category');
    changeButton.setAttribute('onclick', 'submitChangeForm("' + name + '",'+status+')');
    changeButton.textContent = text;
}

function submitChangeForm(name,status) {

    var text = ""
  
    $.ajax({
        url: `api/categories/${name}`,
        type: 'PATCH',
        data: 
        {
            CategoryName : name,
            Status : status
        },
        success: function (data) {
            if (status === true) { text = "Category activated succesfully!"; }
            else { text = "Category deactivated succesfully!"; }
            toastr["success"](text);
            LoadCategories();
            $("#confirmChangeStatusModal").modal('hide');
        },
        error: function () {

            if (status === true) { text = "Failed to activate category!"; }
            else { text = "Failed to deactivate category!"; }
            toastr["error"](text);
        }
    });

}

