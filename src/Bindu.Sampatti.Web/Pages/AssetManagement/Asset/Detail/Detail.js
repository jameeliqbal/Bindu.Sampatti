$(function () {
    var capitalizeAssetModal = new abp.ModalManager(  "/AssetManagement/Asset/Detail/CapitalizeModal");

    $("#CapitalizeAsset").click(function (e) {
        e.preventDefault();
        capitalizeAssetModal.open();
    });

});