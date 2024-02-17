const { data } = require("jquery");

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    
    dataTable = $('#tblData').DataTable({
        ajax: url: '/Admin/Product/GetAll',
        error: function (response) {
            alert(response + "Nega")

        },
        success: function (response) {
            alert(response)
        },
        dataType: 'json',       
        columns: [
            { data: 'title',"width":"25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'price', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'Id',
                render: function (data) {
                    return `<div class="w-75 btn-group">
                            <a href='/admin/product/upsert?id=${data}' class="btn btn-info mx-2">
                                <i class="bi bi-pencil-square"></i>Edit
                            </a>
                            <a href='/admin/product/delete?id=${data}' class="btn btn-danger mx-2">
                                <i class="bi bi-trash"></i>Delete
                            </a>
                        </div>`
                },
                "width": "25%"
            }
        ]
    });
}