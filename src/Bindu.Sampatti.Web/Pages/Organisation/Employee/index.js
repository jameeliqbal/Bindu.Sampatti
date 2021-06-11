$(function () {
    var l = abp.localization.getResource("Sampatti");

    abp.ui.setBusy("#EmployeeTable");

    var responseCallback = function (result) {
        // your custom code.
        abp.ui.clearBusy();

        return {
            recordsTotal: result.totalCount,
            recordsFiltered: result.totalCount,
            data: result.items
        };
    }

    var editModal = new abp.ModalManager(abp.appPath + "organisation/depot/editmodal");

    var employeeDataTable = $("#EmployeeTable").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: abp.libs.datatables.createAjax(bindu.sampatti.employees.employee.getList, null, responseCallback),
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
                                        return l('DepotDeleteConfirmationText', data.record.name.toUpperCase());
                                    },
                                    action: function (data) {
                                        bindu.sampatti.depots.depot
                                            .deleteDepot(data.record.id)
                                            .then(function () {
                                                abp.notify.success(data.record.name.toUpperCase() + " " + l("SuccessfullyDeleted"), "Delete Depot");
                                                abp.ui.setBusy("#DepotTable");
                                                depotDataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('EmployeeName'),
                    data: "name"
                },
                {
                    title: l('EmployeeCode'),
                    data:"code"
                },
                {
                    title: l('EmployeeDesignation'),
                    data: "designationName"
                },
                {
                    title: l('EmployeeDepartment'),
                    data: "departmentName"
                },
                {
                    title: l('EmployeeStatus'),
                    data: "status",
                    render: function (data) {
                        if (data == true)
                            return l('EmployeeStatusActive')
                        else
                            return l('EmployeeStatusInActive')
                    }
                }
            ]

        })
    ); //datatable


    //ADD NEW DEPOT
    var createModal = new abp.ModalManager(abp.appPath + "organisation/depot/createModal");

    $("#NewDepotButton").click(function (e) {
        e.preventDefault();

        createModal.open();
    });

    createModal.onResult(function (e, d) {

        abp.ui.setBusy("#DepotTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!", "New Depot");
    });

    createModal.onClose(function () {

        depotDataTable.ajax.reload();

    });


    //EDIT DEPOT
    editModal.onResult(function (e, d) {
        abp.ui.setBusy("#DepotTable");
        abp.notify.success(d.responseText.toUpperCase() + " saved successfully!", "Update Depot");
    });

    editModal.onClose(function () {

        depotDataTable.ajax.reload();

    });

});//closure