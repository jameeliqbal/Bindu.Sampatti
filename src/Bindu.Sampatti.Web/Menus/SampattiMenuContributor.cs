using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using Bindu.Sampatti.Localization;
using Bindu.Sampatti.MultiTenancy;
using Volo.Abp.TenantManagement.Web.Navigation;
using Volo.Abp.UI.Navigation;

namespace Bindu.Sampatti.Web.Menus
{
    public class SampattiMenuContributor : IMenuContributor
    {
        public async Task ConfigureMenuAsync(MenuConfigurationContext context)
        {
            if (context.Menu.Name == StandardMenus.Main)
            {
                await ConfigureMainMenuAsync(context);
            }
        }

        private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
        {
            if (!MultiTenancyConsts.IsEnabled)
            {
                var administration = context.Menu.GetAdministration();
                administration.TryRemoveMenuItem(TenantManagementMenuNames.GroupName);
            }

            var l = context.GetLocalizer<SampattiResource>();

            context.Menu.Items.Insert(0, new ApplicationMenuItem(SampattiMenus.Home, l["Menu:Home"], "~/"));

            var menuProcurement = new ApplicationMenuItem(SampattiMenus.Procurement, l["Menu:Procurement"], "#");
            context.Menu.Items.Insert(1,menuProcurement);
            menuProcurement.AddItem(new ApplicationMenuItem( SampattiMenus.PurchaseRequisitions, l["Menu:Procurement.PurchaseRequisitions"], "/Procurement/PurchaseRequisition"));
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.Quotations, l["Menu:Procurement.Quotations"], "/Procurement/VendorQuotation"));
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.PurchaseOrders, l["Menu:Procurement.PurchaseOrders"], "/Procurement/PurchaseOrder"));
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.Vendors, l["Menu:Procurement.Vendors"], "/Procurement/Vendor"));

            var menuAssetManagement = new ApplicationMenuItem(SampattiMenus.AssetManagement, l["Menu:AssetManagement"], "#");
            context.Menu.Items.Insert(2, menuAssetManagement);
            menuAssetManagement.AddItem(new ApplicationMenuItem(SampattiMenus.Assets, l["Menu:AssetManagement.Assets"], "/AssetManagement/Asset"));
            menuAssetManagement.AddItem(new ApplicationMenuItem(SampattiMenus.Classes, l["Menu:AssetManagement.Classes"], "/AssetManagement/AssetClass"));
            menuAssetManagement.AddItem(new ApplicationMenuItem(SampattiMenus.AssetAudit, l["Menu:AssetManagement.AssetAudit"], "#"));

            var menuDepriciation = new ApplicationMenuItem(SampattiMenus.Depriciation, l["Menu:Depriciation"], "#");
            context.Menu.Items.Insert(3, menuDepriciation);
            menuDepriciation.AddItem(new ApplicationMenuItem(SampattiMenus.DepriciationRates, l["Menu:Depriciation.DepriciationRates"], "/Depriciation/Rate"));
            menuDepriciation.AddItem(new ApplicationMenuItem(SampattiMenus.DepriciationReport, l["Menu:Depriciation.DepriciationReport"], "#"));

            var menuOrg = new ApplicationMenuItem(SampattiMenus.Organization, l["Menu:Organization"], "#");
            context.Menu.Items.Insert(4, menuOrg);
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Employees, l["Menu:Organization.Employees"], "#"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Designations, l["Menu:Organization.Designations"], "/organisation/designation"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Departments, l["Menu:Organization.Departments"], "/organisation/department"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Locations, l["Menu:Organization.Locations"], "/organisation/location"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Depots, l["Menu:Organization.Depots"], "/organisation/depot"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Plants, l["Menu:Organization.Plants"], "/organisation/plant"));

        }
    }
}
