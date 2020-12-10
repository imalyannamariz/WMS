$(document).ready(function () {   
    //for start/end date default value is date today
    var today;
    var d = new Date();
    if (d.getDate() < 10) {
        today = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + '0' + d.getDate();
    } else {
        today = d.getFullYear() + '-' + (d.getMonth() + 1) + '-' + d.getDate();
    }

    $('#EndDate').val(today);
    $('#StartDate').val(today);

    $(".alert").fadeTo(2000, 500).slideUp(500, function () {
        $(".alert").slideUp(500);
    });

    $('#btnShow').on('click', function () {
        var CategoryID = $('#CategoryID').val();
        var WarehouseID = $('#WarehouseID').val();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();
        fnTable(CategoryID, WarehouseID, StartDate, EndDate);
    });

    $('#btnExport').on('click', function () {
        var CategoryID = $('#CategoryID').val();
        var WarehouseID = $('#WarehouseID').val();
        var StartDate = $('#StartDate').val();
        var EndDate = $('#EndDate').val();

        $.ajax({
            url: ExportRecord,
            type: 'GET',
            data: {
                CategoryID: CategoryID,
                WarehouseID: WarehouseID,
                StartDate: StartDate,
                EndDate: EndDate
            },
            success: function (model) {
                if (model != "") {
                    window.location.href = model;

                    var message = "Successfully downloaded! Open file to view records.";
                    alert(message);
                    location.reload();
                }
            }
        });
    });

    fnTable = function (CategoryID, WarehouseID, StartDate, EndDate) {
        $('#tblReport').DataTable({
            destroy: true,
            searching: true,
            pageLength: 15,
            order: [14],
            dom: 'ftp',
            ajax: {
                url: ShowRecord,
                type: 'GET',
                data: {
                    CategoryID: CategoryID,
                    WarehouseID: WarehouseID,
                    StartDate: StartDate,
                    EndDate: EndDate
                },
                dataSrc: '',
                complete: function (response, request) {
                    //console
                }
            },
            fnRowCallback: function (nRow, aData, iDisplayIndex) {

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
                    "width": "14%"
                },
                {
                    "title": "Warehouse",
                    "data": "WarehouseName",
                    "width": "8%"
                },
                {
                    "title": "Category",
                    "data": "CategoryName",
                    "width": "8%"
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
                    "width": "5%",
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
                    "visible": false
                },
                {
                    "title": "Used Stocks",
                    "data": "UsedStocks",
                    "width": "6%",
                    "visible": false
                },
                {
                    "title": "Reserved Stocks",
                    "data": "ReservedStocks",
                    "width": "8%",
                },
                {
                    "title": "Avail. Stocks",
                    "data": "AvailableStocks",
                    "width": "7%"
                },
                {
                    "title": "Created By",
                    "data": "CreatedBy",
                    "width": "8%",
                    "visible": false
                },
                {
                    "title": "Created Date",
                    "data": "CreatedDate",
                    "width": "8%",
                    "visible": true,
                    "render": function (data) {
                        var date = new Date(parseInt(data.substr(6)));
                        return $.format.date(date, "MM/dd/yyyy hh:mm:ss a")
                    },
                },
                {
                    "title": "Modified By",
                    "data": "ModifiedBy",
                    "width": "10%",
                    "visible": true
                },
                {
                    "title": "Modified Date",
                    "data": "ModifiedDate",
                    "visible": false
                },
                {
                    "title": "Remarks",
                    "data": "Remarks",
                    "width": "14%"
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
                }
            ]
        });
    }

    fnExport = function (CategoryID, WarehouseID, StartDate, EndDate) {
        $.ajax({
            url: ExportRecord,
            type: 'GET',
            data: {

            },
            dataSrc: '',
            complete: function (response, request) {
                //console
            }
        });
    }
});