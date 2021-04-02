$(function () {
    var l = abp.localization.getResource('Sampatti');

    var dataTable = $("#prList").DataTable(
        abp.libs.datatables.normalizeConfiguration({
            serverSide: true,
            paging: true,
            order: [[1, "asc"]],
            searching: false,
            scrollX: true,
            ajax: "/api/testdatapr/getprs",
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
                                        editModal.open({ prNumber: data.record[1] });
                                    }
                                },
                                {
                                    text: l('Delete'),
                                    //visible: abp.auth.isGranted('BookStore.Books.Delete'),
                                    confirmMessage: function (data) {
                                        return l('PRDeletionConfirmationMessage', data.record[1]);
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
                    title:  l('PRNumber') ,
                    //data: "name"
                    render: function (data) {
                        return "<a href=\"purchaserequisition/lineitems/index?prNumber=" + data + "\">" + data + "</a>";
                    }
                },
                {
                    title: l('Date'),
                    //data: "publishDate",
                    //render: function (data) {
                    //    return luxon.DateTime.fromISO(data, {
                    //        locale: abp.localization.currentCulture.name
                    //    }).toLocaleString();
                    //}
                },
                {
                    title: l('Requisitioner'),
                    //data: "authorName"
                },
                {
                    title: l('Department'),
                    //data: "type",
                    //render: function (data) {
                    //    return l('Enum:BookType:' + data);
                    //}
                },
                       {
                    title: l('Section'),
                    //data: "price"
                },
                      
            {
                title: l('Status'),
                //data: "type",
                //render: function (data) {
                //    return l('Enum:BookType:' + data);
                //}
               
                } 
                //{
                //    title: l('CreationTime'),
                //    data: "creationTime",
                //    render: function (data) {
                //        return luxon.DateTime.fromISO(data, {
                //            locale: abp.localization.currentCulture.name
                //        }).toLocaleString(luxon.DateTime.DATETIME_SHORT);
                //    }
                //}

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