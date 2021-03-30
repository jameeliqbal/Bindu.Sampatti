$(function () {
    var l = abp.localization.getResource('Sampatti');

    var dataTable = $("#poList").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: "https://localhost:44353/api/testdatapo/getpos",
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
                                        editModal.open({ poNumber: data.record[1] });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    //visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                    confirmMessage: function (data) {
                                        return l('PODeletionConfirmationMessage', data.record[1]);
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
                    title: l('PONumber'),
                    //data: "name"
                    render: function (data) {
                        return "<a href=\"purchaseorder/lineitems/index?poNumber=" + data + "\">" + data + "</a>";
                    }
                },
                {
                    title: l('PODate'),
                    //data: "publishDate",
                    //render: function (data) {
                    //    return luxon.DateTime.fromISO(data, {
                    //        locale: abp.localization.currentCulture.name
                    //    }).toLocaleString();
                    //}
                },
                {
                    title: l('Vendor'),
                    //data: "authorName"
                },
                {
                    title: l('RelatedVQ'),
                    //data: "type",
                    render: function (data) {
                        return "<a href=\"vendorquotation/lineitems/index?quotationNumber=" + data + "\">" + data + "</a>";
                    }

                } 

            ]

        })
    );

    var createModal = new abp.ModalManager(abp.appPath + 'procurement/purchaseRequisition/CreateModal');
    createModal.onResult(function () {
        dataTable.ajax.reload();
    });

    var editModal = new abp.ModalManager(abp.appPath + 'procurement/purchaseRequisition/EditModal');
    editModal.onResult(function () {
        dataTable.ajax.reload();
    })

    $('#NewPR').click(function (e) {
        e.preventDefault();
        createModal.open();

    });


});