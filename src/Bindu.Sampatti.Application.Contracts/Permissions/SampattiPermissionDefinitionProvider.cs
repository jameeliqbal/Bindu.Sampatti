using Bindu.Sampatti.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bindu.Sampatti.Permissions
{
    public class SampattiPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(SampattiPermissions.GroupName);

            //Define your own permissions here. Example:
            //myGroup.AddPermission(SampattiPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<SampattiResource>(name);
        }
    }
}
