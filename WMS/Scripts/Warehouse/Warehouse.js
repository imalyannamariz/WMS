$(document).ready(function () {
    $(".alert").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);
    });
    
    $('#tblWarehouse').DataTable({
        destroy: true,
        pageLength: 20,
        searching: true,
        order: [4, "desc"],
        dom: 'ftp',
        ajax: {
            url: GetRecord,
            type: "GET",
            dataSrc: "",
            complete: function (response, request) {

            }
        },
        fnRowCallback: function (nRow, aData, iDisplayIndex) {
            $('#btnEdit', nRow).off().on('click', function (e) {
                e.preventDefault();

                var table = $('#tblWarehouse').DataTable();
                var row = table.row($(this).closest('tr')).data();

                $.ajax({
                    url: GetRowDetails,
                    type: 'GET',
                    data: {
                        WarehouseID: row.WarehouseID
                    },
                    success: function (data) {
                        $('#WarehouseID').val(data.WarehouseID);
                        $('#WarehouseName').val(data.WarehouseName);
                        $('#WarehouseAddress').val(data.WarehouseAddress);
                        $('#Mode').val(1);
                        $('#mdlCreateUpdate').modal('show');
                        $('#mdlCreateUpdate').on('hidden.bs.modal', function () {
                            $('#mdlCreateUpdate form')[0].reset();
                        });
                    }
                });
            });

            $('#btnDelete', nRow).off().on('click', function (e) {
                e.preventDefault();                                

                var table = $('#tblWarehouse').DataTable();
                var row = table.row($(this).closest('tr')).data();

                $.ajax({
                    url: GetDelete,
                    type: 'GET',
                    data: {
                        WarehouseID: row.WarehouseID
                    },
                    success: function (data) {
                        $('#mdlDelete #WarehouseID').val(data.WarehouseID);
                        $('#mdlDelete').modal('show'); 
                    }
                });
                                    
            });
        },
        columns: [
            {
                "title": "Warehouse ID",
                "data": "WarehouseID",
                "visible": false
            },
            {
                "title": "Warehouse Name",
                "data": "WarehouseName",
                "visible": true
            },
            {
                "title": "Warehouse Address",
                "data": "WarehouseAddress",
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