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
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.Quotations, l["Menu:Procurement.Quotations"], "#"));
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.PurchaseOrders, l["Menu:Procurement.PurchaseOrders"], "#"));
            menuProcurement.AddItem(new ApplicationMenuItem(SampattiMenus.Vendors, l["Menu:Procurement.Vendors"], "#"));

            var menuAssets = new ApplicationMenuItem(SampattiMenus.Assets, l["Menu:Assets"], "#");
            context.Menu.Items.Insert(2, menuAssets);
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Register, l["Menu:Assets.Register"], "/Assets/Register"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Audit, l["Menu:Assets.Audit"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Tracking, l["Menu:Assets.Tracking"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Classes, l["Menu:Assets.Classes"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Components, l["Menu:Assets.Components"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Tagging, l["Menu:Assets.Tagging"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.Depriciation, l["Menu:Assets.Depriciation"], "#"));
            menuAssets.AddItem(new ApplicationMenuItem(SampattiMenus.DisposedAndRetired, l["Menu:Assets.DisposedAndRetired"], "#"));

            var menuOrg = new ApplicationMenuItem(SampattiMenus.Organization, l["Menu:Organization"], "#");
            context.Menu.Items.Insert(3, menuOrg);
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Employees, l["Menu:Organization.Employees"], "#"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Departments, l["Menu:Organization.Departments"], "#"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Sections, l["Menu:Organization.Sections"], "#"));
            menuOrg.AddItem(new ApplicationMenuItem(SampattiMenus.Locations, l["Menu:Organization.Locations"], "#"));

        }
    }
}
