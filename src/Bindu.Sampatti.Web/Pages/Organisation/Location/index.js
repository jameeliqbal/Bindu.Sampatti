$(function () {
    var l = abp.localization.getResource("Sampatti");

    abp.ui.setBusy("#LocationsTable");

    var responseCallback = function (result) {
        // your custom code.
        abp.ui.clearBusy();

        return {
            recordsTotal: result.totalCount,
            recordsFiltered: result.totalCount,
            data: result.items
        };
    }

    var editModal = new abp.ModalManager(abp.appPath + "organisation/location/editmodal");

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
                                    action: function (data) {
                                        editModal.open({ id: data.record.id });
                                    }
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

    $("#NewLocationButton").click(function (e) {
        e.preventDefault();
        
        createModal.open();
    });

    createModal.onResult(function (e,d) {
         
        abp.ui.setBusy("#LocationsTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!","New Location");
    });

    createModal.onClose(function () {
       
          locationsDataTable.ajax.reload() ;

    });
     

    //EDIT LOCATION
    editModal.onResult(function () {
        abp.ui.setBusy("#LocationsTable");
        abp.notify.success(d.responseText.toUpperCase() + " saved successfully!", "Update Location");
    });

});