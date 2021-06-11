﻿$(function () {
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

    var editModal = new abp.ModalManager(abp.appPath + "organisation/employee/editmodal");

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
                                        return l('EmployeeDeleteConfirmationText', data.record.name.toUpperCase());
                                    },
                                    action: function (data) {
                                        bindu.sampatti.employees.employee
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.success(data.record.name.toUpperCase() + " " + l("SuccessfullyDeleted"),
                                                    "Delete Employee");
                                                abp.ui.setBusy("#EmployeeTable");
                                                employeeDataTable.ajax.reload();
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
    var createModal = new abp.ModalManager(abp.appPath + "organisation/employee/createModal");

    $("#NewEmployeeButton").click(function (e) {
        e.preventDefault();

        createModal.open();
    });

    createModal.onResult(function (e, d) {

        abp.ui.setBusy("#EmployeeTable");
        abp.notify.success(d.responseText.toUpperCase() + " added successfully!", "New Employee");
    });

    createModal.onClose(function () {

        employeeDataTable.ajax.reload();

    });


    //EDIT DEPOT
    editModal.onResult(function (e, d) {
        abp.ui.setBusy("#EmployeeTable");
        abp.notify.success(d.responseText.toUpperCase() + " saved successfully!", "Update Employee");
    });

    editModal.onClose(function () {

        employeeDataTable.ajax.reload();

    });

});//closure