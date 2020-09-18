$(document).ready(function () {
    $('#tblInventory').DataTable({
        destroy: true,
        searching: true,
        pageLength: 20,
        order: [11, 'desc'],
        dom: 'ftp',
        ajax: {
            url: GetRecord,
            type: 'GET',
            dataSrc: '',
            complete: function (response, request) {
                //console
            }
        },
        fnRowCallback: function (nRow, aData, iDisplayIndex) {
            $('#btnEdit', nRow).off().on('click', function (e) {
                var table = $('#tblInventory').DataTable();
                var row = table.row($(this).closest('tr')).data();

                $('#mdlCreateUpdate').modal('show');
            });
        },
        columns: [
            {
                "title": "Item ID",
                "data": "ItemID",
                "visible": false
            },
            {
                "title": "Item Description",
                "data": "ItemDescription",
                "width": "15%"
            },
            {
                "title": "Warehouse",
                "data": "WarehouseName",
                "width": "10%"
            },
            {
                "title": "Category",
                "data": "CategoryName",
                "width": "10%"
            },
            {
                "title": "Material Code",
                "data": "MaterialCode",
                "width": "8%",
                "visible": false
            },
            {
                "title": "MIS No",
                "data": "MISNo",
                "width": "5%",
                "visible": true
            },
            {
                "title": "Origin",
                "data": "Origin",
                "visible": true,
                "width": "8%",
            },
            {
                "title": "Received Date",
                "data": "ReceivedDate",
                "visible": false
            },
            {
                "title": "Stocks",
                "data": "Stocks",
                //"width": "25%"
            },
            {
                "title": "Used Stocks",
                "data": "UsedStocks",
                "width": "7%"
            },
            {
                "title": "Created By",
                "data": "CreatedBy",
                "width": "10%",
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
                "width": "10%",
                "visible": false
            },
            {
                "title": "Modified Date",
                "data": "ModifiedDate",
                "visible": false
            },
            {
                "title": "Remarks",
                "data": "Remarks",
                "width": "10%"
            },
            {
                "title": "DELETEDBY",
                "data": "DeletedBy",
                "visible": false
            },
            {
                "title": "DATEDELETED",
                "data": "DeletedDate",
                "visible": false
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