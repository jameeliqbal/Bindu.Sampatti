$(function () {
    var l = abp.localization.getResource('Sampatti');

    var dataTable = $("#prLineItems").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: "/api/testdatapr/getprlines",
            columnDefs: [
                {
                    title: l('Actions'),
                    rowAction: {
                        items:
                            [
                                {
                                    text: l('Edit'),
                                    iconClass:"fas fa-edit",
                                    //visible: abp.auth.isGranted('BookStore.Books.Edit'),
                                    action: function (data) {
                                        editModal.open({ prNumber: data.record[1] });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    iconClass:"fas fa-trash-alt",
                                    //visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                    confirmMessage: function (data) {
                                        return l('PRLineItemDeletionConfirmationMessage', data.record[1]);
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
                    title: l('Description'),
                    //data: "name"
                    //render: function (data) {
                    //    return "<a href=\"purchaserequisition/lineitems?prNumber=" + data + "\">" + data + "</a>";
                    //}
                },
                {
                    title: l('Quantity'),
                    //data: "publishDate",
                    //render: function (data) {
                    //    return luxon.DateTime.fromISO(data, {
                    //        locale: abp.localization.currentCulture.name
                    //    }).toLocaleString();
                    //}
                },
                {
                    title: l('Rate'),
                    //data: "authorName"
                },
                {
                    title: l('Total'),
                    //data: "type",
                    //render: function (data) {
                    //    return l('Enum:BookType:' + data);
                    //}
                } 

            ],
            select:true,
            buttons: ['selectAll','selectNone']


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


})