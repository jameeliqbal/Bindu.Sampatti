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


    var farList = $('#farList').DataTable(nc);

    function CreateColumns() {
        return [
            {
                title: "Asset Code",
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

});

