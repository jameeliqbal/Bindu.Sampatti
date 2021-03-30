$(function () {
    var l = abp.localization.getResource('Sampatti');

    var dataTable = $("#vendorsList").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: "https://localhost:44353/api/testdatav/getvendors",
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    //visible: abp.auth.isGranted('BookStore.Books.Edit'),
                                    action: function (data) {
                                        editModal.open({ vendorID: data.record[1] });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    //visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                    confirmMessage: function (data) {
                                        return l('VendorDeletionConfirmationMessage', data.record[1].toUpper());
                                    },
                                    action: function (data) {
                                        acme.bookStore.books.book
                                            .delete(data.record.id)
                                            .then(function () {
                                                abp.notify.info(l('SuccessfullyDeleted'));
                                                dataTable.ajax.reload();
                                            });
                                    }
                                }
                            ]
                    }
                },
                {
                    title: l('VendorName'),
                    //data: "name"
                    render: function (data) {
                        return "<a href=\"vendor/quotations/index?quotationNumber=" + data + "\">" + data + "</a>";
                    }
                },
                {
                    title: l('Phone'),
                    //data: "publishDate",
                    //render: function (data) {
                    //    return luxon.DateTime.fromISO(data, {
                    //        locale: abp.localization.currentCulture.name
                    //    }).toLocaleString();
                    //}
                },
                {
                    title: l('Email'),
                    //data: "authorName"
                }  

            ]

        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'procurement/vendor/CreateModal');
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    var editModal = new abp.ModalManager(abp.appPath + 'procurement/vendor/EditModal');
    editModal.onResult(function () {
        dataTable.ajax.reload();
    })

    $('#NewVendor').click(function (e) {
        e.preventDefault();
        createModal.open();

    });


});