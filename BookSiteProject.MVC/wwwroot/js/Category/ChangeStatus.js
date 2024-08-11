function confirmChangeStatusModal(name,text,status) {
    $('#confirmChangeStatusModal').modal('show');
    var changeButton = document.querySelector('.change-status-category');
    changeButton.setAttribute('onclick', 'submitChangeForm("' + name + '",'+status+')');
    changeButton.textContent = text;
}

function submitChangeForm(name,status) {
    
  
    $.ajax({
        url: 'api/Category/ChangeStatus',
        type: 'POST',
        data: 
        {
            CategoryName : name,
            Status : status
        },
        success: function (data) {
            toastr["success"]("Changed status of category");
            LoadCategories();
            $("#confirmChangeStatusModal").modal('hide');
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });

}

