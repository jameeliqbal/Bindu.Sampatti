$(function () {

    var nc = abp.libs.datatables.normalizeConfiguration(
        {
            serverSide: true,
            paging: true,
            order: [[1, true]],
            searching: false,
            scrollX: true,
            ajax: "/api/testdata/GetAssets",   
            columnDefs: CreateColumns()
        }
    );


    var farList = $('#assetList').DataTable(nc);

    function CreateColumns() {
        return [
            {
                title: "Asset Code",
                render: function (data) {
                    return "<a href=\"/AssetManagement/Asset/Detail\">" + data + "</>";
                }
                //data: "assetCode"
            },
            {
                title: "Description",
                //data: "description"
            },
            {
                title: "Asset Class",
                //data: "assetClass"
            } 
             
        ];
    }


    var createModal = new abp.ModalManager(abp.appPath + "AssetManagement/Asset/CreateModal");

    $("#NewFixedAsset").click(function (e) {
        e.preventDefault();
        createModal.open();

    });
});

