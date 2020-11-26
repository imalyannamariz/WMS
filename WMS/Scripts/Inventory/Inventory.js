$(document).ready(function () {
    $('#btnNew').on('click', function () {
        //for options default value
        var test = $('#CategoryID').find('option:selected').text('Choose a Category').val(0);
        $('#WarehouseID').find('option:selected').text('Choose a Location').val(0);


        //for received date default value is date today
        var d = new Date();
        var today = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
        $('#ReceivedDate').val(today);
    });
               
    //datatable
    $('#tblInventory').DataTable({
        destroy: true,
        searching: true,
        pageLength: 20,
        order: [14, 'desc'],
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

                $.ajax({
                    url: GetRowDetails,
                    type: 'GET',
                    data: {
                        ItemID: row.ItemID
                    },
                    success: function (data) {                       
                        var date = data.ReceivedDate;
                        var result = date.split('-');
                        var newDate = (result[0] + '-' + result[1] + '-' + result[2].substring(0, 2));
                                                
                        $('#CategoryID option:selected').val(data.CategoryID).text(data.CategoryName);
                        $('#WarehouseID option:selected').val(data.WarehouseID).text(data.WarehouseName);   
                        $('#ItemID').val(data.ItemID);
                        $('#ItemName').val(data.ItemName);
                        $('#ItemDescription').val(data.ItemDescription);
                        $('#Stocks').val(data.Stocks);
                        $('#UsedStocks').val(data.UsedStocks);
                        $('#Unit').val(data.Unit);
                        $('#MaterialCode').val(data.MaterialCode);
                        $('#Origin').val(data.Origin);
                        $('#MISNo').val(data.MISNo);
                        document.getElementById("ReceivedDate").value = newDate;
                        $('#Remarks').val(data.Remarks);
                        $('#mode').val(1);
                        $('#mdlCreateUpdate').modal('show');
                        $('#mdlCreateUpdate').on('hidden.bs.modal', function () {
                            $('#mdlCreateUpdate form')[0].reset();
                        });
                    }
                });
            });

            $('#btnDelete', nRow).off().on('click', function (e) {
                var table = $('#tblInventory').DataTable();
                row = table.row($(this).closest('tr')).data();
                $.ajax({
                    url: GetDelete,
                    type: 'GET',
                    data: {
                        ItemID: row.ItemID
                    },
                    success: function (data) {
                        console.log(data.ItemID);
                        $('#mdlDelete #ItemID').val(data.ItemID);
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
                "title": "Item ID",
                "data": "ItemID",
                "visible": false
            },
            {
                "title": "Item Name",
                "data": "ItemName",
                "width": "8%"
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
                "visible": false
            },
            {
                "title": "Received Date",
                "data": "ReceivedDate",
                "visible": false
            },
            {
                "title": "Stocks",
                "data": "Stocks",
                "width": "5%",
            },
            {
                "title": "Used Stocks",
                "data": "UsedStocks",
                //"width": "5%",
            },
            {
                "title": "Avail. Stocks",
                "data": "AvailableStocks",                
                //"width": "20%"
            },
            {
                "title": "Created By",
                "data": "CreatedBy",
                "width": "10%",
                "visible": false
            },
            {
                "title": "Created Date",
                "data": "CreatedDate",
                "width": "10%",
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
                "width": "15%"
            },
            {
                "title": "DELETED BY",
                "data": "DeletedBy",
                "visible": false
            },
            {
                "title": "DATE DELETED",
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