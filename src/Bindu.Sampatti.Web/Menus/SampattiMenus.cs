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

        public const string Assets = Prefix + ".Assets";
        public const string Register = Assets + ".Register";
        public const string Audit = Assets + ".Audit";
        public const string Tracking = Assets + ".Tracking";
        public const string Classes = Assets + ".Classes";
        public const string Components = Assets + ".Components";
        public const string Tagging = Assets + ".Tagging";
        public const string Depriciation = Assets + ".Depriciation";
        public const string DisposedAndRetired = Assets + ".DisposedAndRetired";

        public const string Organization = Prefix + ".Organization";
        public const string Employees = Organization + ".Employees";
        public const string Departments = Organization + ".Departments";
        public const string Sections = Organization + ".Sections";
        public const string Locations = Organization + ".Locations";

    }
}