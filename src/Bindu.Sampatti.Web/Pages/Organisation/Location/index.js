$(function(){
    var l = abp.localization.getResource("Sampatti");

    var locationsDataTable = $("#LocationsTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bindu.sampatti.locations.location.getList),
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    //action: function (data) {

                                    //}
                                },
                                {
                                    text: l('Delete')
                                }
                            ]
                    }
                },
                {
                    title: l('LocationName'),
                    data: "name"
                },
                {
                    title: l('LocationStatus'),
                    data: "isEnabled",
                    render: function (data) {
                        if (data == true)
                            return "Active"
                        else
                            return "In-Active"
                    }
                }

             
            ]

        })
    ); //datatable

});