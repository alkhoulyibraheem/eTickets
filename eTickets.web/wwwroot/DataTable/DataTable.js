function DataTable(url, columns)
{
    if (url == undefined || url == null || url == "")
    {
        url = "GetDataTableData";
    }

    $("#kt_datatable_dom_positioning").DataTable(
    {
        "language": {
            "lengthMenu": "عرض _MENU_",
        },
        "dom":
            "<'row'" +
            "<'col-sm-6 d-flex align-items-center justify-conten-start'l>" +
            "<'col-sm-6 d-flex align-items-center justify-content-end'f>" +
            ">" +

            "<'table-responsive'tr>" +

            "<'row'" +
            "<'col-sm-12 col-md-5 d-flex align-items-center justify-content-center justify-content-md-start'i>" +
            "<'col-sm-12 col-md-7 d-flex align-items-center justify-content-center justify-content-md-end'p>" +
            ">",
        ajax: {
            url: url,
            type: "POST",
        },
        //bSort: false,
        processing: true,
        serverSide: true,
        filter: true,
        columns: columns,
        fnDrawCallback: function (data)
        {
            if (typeof _funAfterGetData == 'function')
            {
                _funAfterGetData();
            }
        }
    });
}