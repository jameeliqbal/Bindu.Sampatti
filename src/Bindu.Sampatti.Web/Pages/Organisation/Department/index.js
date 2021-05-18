$(function () {
    var l = abp.localization.getResource("Sampatti");

    abp.ui.setBusy("#DepartmentTable");

    var responseCallback = function (result) {
        // your custom code.
        abp.ui.clearBusy();

        return {
            recordsTotal: result.totalCount,
            recordsFiltered: result.totalCount,
            data: result.items
        };
    }

    var editModal = new abp.ModalManager(abp.appPath + "organisation/department/editmodal");

    var departmentDataTable = $("#DepartmentTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bindu.sampatti.departments.department.getList, null, responseCallback),
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
                                                abp.notify.success(data.record.name.toUpperCase() + " " + l("SuccessfullyDeleted"), "Delete Location");
                                                abp.ui.setBusy("#DepotTable");
                                                plantDataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('DepartmentName'),
                    data: "name"
                },                 
                {
                    title: l('DepartmentStatus'),
                    data: "status",
                    render: function (data) {
                        if (data == true)
                            return l('DepartmentStatusActive')
                        else
                            return l('DepartmentStatusInActive')
                    }
                }


            ]

        })
    ); //datatable


    //ADD NEW DEPOT
    var createModal = new abp.ModalManager(abp.appPath + "organisation/department/createModal");

    $("#NewDepartmentButton").click(function (e) {
        e.preventDefault();

        createModal.open();
    });

    createModal.onResult(function (e, d) {

        abp.ui.setBusy("#DepartmentTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!", "New Department");
    });

    createModal.onClose(function () {

        departmentDataTable.ajax.reload();

    });


    //EDIT DEPOT
    editModal.onResult(function (e, d) {
        abp.ui.setBusy("#DepartmentTable");
        abp.notify.success(d.responseText.toUpperCase() + " saved successfully!", "Update Department");
    });

    editModal.onClose(function () {

        departmentDataTable.ajax.reload();

    });

});//closure