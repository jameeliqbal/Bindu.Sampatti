namespace Bindu.Sampatti.Web.Menus
{
    public class SampattiMenus
    {
        private const string Prefix = "Sampatti";
        public const string Home = Prefix + ".Home";

        //Add your menu items here...
        public const string Procurement = Prefix + ".Procurement";
        public const string PurchaseRequisitions = Procurement + ".PurchaseRequisitions";
        public const string Quotations = Procurement + ".Quotations";
        public const string PurchaseOrders = Procurement + ".PurchaseOrders";
        public const string Vendors = Procurement + ".Vendors";

        public const string AssetManagement = Prefix + ".AssetManagement";
        public const string Assets = AssetManagement + ".Assets";
        public const string Classes = AssetManagement + ".Classes";
        public const string AssetAudit = AssetManagement + ".AssetAudit";

        public const string Depriciation = Prefix + ".Depriciation";
        public const string DepriciationRates = Depriciation + ".DepriciationRates";
        public const string DepriciationReport = Depriciation + ".DepriciationReport";

        public const string Organization = Prefix + ".Organization";
        public const string Employees = Organization + ".Employees";
        public const string Departments = Organization + ".Departments";
        public const string Sections = Organization + ".Sections";
        public const string Locations = Organization + ".Locations";

    }
}