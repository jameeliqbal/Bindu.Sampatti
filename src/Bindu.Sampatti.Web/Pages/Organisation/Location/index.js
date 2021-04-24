$(function () {
    var l = abp.localization.getResource("Sampatti");

    var responseCallback = function (result) {
        // your custom code.
        abp.ui.clearBusy();

        return {
            recordsTotal: result.totalCount,
            recordsFiltered: result.totalCount,
            data: result.items
        };
    }

     abp.ui.setBusy("#LocationsTable");
 
    var locationsDataTable = $("#LocationsTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bindu.sampatti.locations.location.getList, null, responseCallback),
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
                            return l('LocationStatusActive')
                        else
                            return l('LocationStatusInActive')
                    }
                }

             
            ]

        })
    ); //datatable

     //ADD NEW LOCATION
    var createModal = new abp.ModalManager(abp.appPath + "organisation/location/createModal");

    createModal.onResult(function (e,d) {
         
        abp.ui.setBusy("#LocationsTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!","New Location");
    });

    createModal.onClose(function () {
       
          locationsDataTable.ajax.reload() ;

    });
     
    $("#NewLocationButton").click(function (e) {
        e.preventDefault();
        
        createModal.open();
    });


});