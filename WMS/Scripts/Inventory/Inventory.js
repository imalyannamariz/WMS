$(document).ready(function () {
    $('#CategoryID').on('change', function (e) {
        var CatID = $(this).val();
        $('#SubCategoryID').empty();
        $.ajax({
            url: SubcategoryOptions,
            type: 'GET',
            data: {
                CategoryID: CatID
            },
            success: function (data) {
                $.each(data, function (key, value) {
                    $('#SubCategoryID').append(new Option(value.SubCategoryName, value.SubCategoryID));
                });                
            }            
        });
    });

    $('#tblInventory').DataTable({
        destroy: true,
        searching: true,
        pageLength: 20,
        order: [12, 'desc'],
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

                        console.log(data.CategoryID);
                        console.log(data.CategoryName);
                        
                        $('#CategoryID option:selected').val(data.CategoryID).text(data.CategoryName);
                        $('#SubCategoryID option:selected').val(data.SubCategoryID).text(data.SubCategoryName); 
                        $('#WarehouseID option:selected').val(data.WarehouseID).text(data.WarehouseName);   
                        $('#ItemID').val(data.ItemID);
                        $('#ItemName').val(data.ItemName);
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
                "width": "10%"
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
                "title": "Subcategory",
                "data": "SubCategoryName",
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