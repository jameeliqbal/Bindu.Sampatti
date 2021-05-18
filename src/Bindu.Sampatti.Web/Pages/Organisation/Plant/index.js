$(function () {
    var l = abp.localization.getResource("Sampatti");

    abp.ui.setBusy("#PlantTable");

    var responseCallback = function (result) {
        // your custom code.
        abp.ui.clearBusy();

        return {
            recordsTotal: result.totalCount,
            recordsFiltered: result.totalCount,
            data: result.items
        };
    }

    var editModal = new abp.ModalManager(abp.appPath + "organisation/plant/editmodal");

    var plantDataTable = $("#PlantTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bindu.sampatti.plants.plant.getList, null, responseCallback),
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
                                    text: l('Delete'),
                                    confirmMessage: function (data) {
                                        return l('PlantDeleteConfirmationText', data.record.name.toUpperCase());
                                    },
                                    action: function (data) {
                                        bindu.sampatti.plants.plant
                                            .deletePlant(data.record.id)
                                            .then(function () {
                                                abp.notify.success(data.record.name.toUpperCase() + " " + l("SuccessfullyDeleted"), "Delete Plant");
                                                abp.ui.setBusy("#DepotTable");
                                                plantDataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('PlantName'),
                    data: "name"
                },
                {
                    title: l('PlantLocation'),
                    data:"locationName"
                },
                {
                    title: l('PlantStatus'),
                    data: "status",
                    render: function (data) {
                        if (data == true)
                            return l('PlantStatusActive')
                        else
                            return l('PlantStatusInActive')
                    }
                }


            ]

        })
    ); //datatable


    //ADD NEW DEPOT
    var createModal = new abp.ModalManager(abp.appPath + "organisation/plant/createModal");

    $("#NewPlantButton").click(function (e) {
        e.preventDefault();

        createModal.open();
    });

    createModal.onResult(function (e, d) {

        abp.ui.setBusy("#PlantTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!", "New Plant");
    });

    createModal.onClose(function () {

        plantDataTable.ajax.reload();

    });


    //EDIT DEPOT
    editModal.onResult(function (e, d) {
        abp.ui.setBusy("#PlantTable");
        abp.notify.success(d.responseText.toUpperCase() + " saved successfully!", "Update Plant");
    });

    editModal.onClose(function () {

        plantDataTable.ajax.reload();

    });

});//closure