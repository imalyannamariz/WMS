$(document).ready(function () {
    $(".alert").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);
    });

    $('#tblCategory').DataTable({
        destroy: true,
        searching: true,
        order: [4, 'desc'],
        pageLength: 20,
        dom: 'ftp',
        ajax: {
            url: GetRecords,
            type: 'GET',
            dataSrc: '',
            complete: function (response, request) {

            }
        },
        fnRowCallback: function(nRow, aData, iDisplayIndex) {
            $('#btnEdit', nRow).off().on('click', function (e) {
                var table = $('#tblCategory').DataTable();
                row = table.row($(this).closest('tr')).data();
                $.ajax({
                    url: GetRowDetails,
                    type: 'GET',
                    data: {
                        CategoryID: row.CategoryID
                    },
                    success: function (data) {
                        $('#CategoryName').val(data.CategoryName);
                        $('#CategoryDescription').val(data.CategoryDescription);
                        $('#CategoryID').val(data.CategoryID);
                        $('#mode').val(1);
                        $('#mdlCreateUpdate').modal('show');
                        $('#mdlCreateUpdate').on('hidden.bs.modal', function () {
                            $('#mdlCreateUpdate form')[0].reset();
                        });
                    }
                });
            });

            $('#btnDelete', nRow).off().on('click', function (e) {
                var table = $('#tblCategory').DataTable();
                row = table.row($(this).closest('tr')).data();
                $.ajax({
                    url: GetDelete,
                    type: 'GET',
                    data: {
                        CategoryID: row.CategoryID
                    },
                    success: function (data) {
                        console.log(data);
                        $('#CatID').val(data.CategoryID);
                        $('#mdlDelete').modal('show');
                        $('#mdlDelete').on('hidden.bs.modal', function () {
                            $('#mdlDelete form')[0].reset();
                        });
                    }
                });
            });
        },
        columns: [
            {
                'title': 'Category ID',
                'data': 'CategoryID',
                'visible': false
            },
            {
                'title': 'Category Name',
                'data': 'CategoryName'
            },
            {
                "title": "Category Description",
                "data": "CategoryDescription",
                "visible": true
            },
            {
                "title": "Created By",
                "data": "CreatedBy",
                "visible": true
            },
            {
                "title": "Created Date",
                "data": "CreatedDate",
                "visible": true
            },
            {
                "title": "Modified By",
                "data": "ModifiedBy",
                "visible": true
            },
            {
                "title": "Modified Date",
                "data": "ModifiedDate",
                "visible": true
            },
            {
                "title": "Action",
                "render": function () {
                    var btnEdit = $('<button id="btnEdit" class="btn btn-primary btn-sm" style="font-size: 0.8em;" type="button" title="Edit Record">');
                    btnEdit.append('<i class="fa fa-pencil-square-o"></i></button>');

                    var btnDelete = $('<button id="btnDelete" class="btn btn-primary btn-sm" style="font-size: 0.8em;" type="button" title="Delete Record">');
                    btnDelete.append('<i class="fa fa-trash-o"></i></button>');

                    return btnEdit.prop('outerHTML') + ' ' + btnDelete.prop('outerHTML');
                }
            }
        ]
    });
});