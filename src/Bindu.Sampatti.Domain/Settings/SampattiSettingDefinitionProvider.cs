using Volo.Abp.Settings;

namespace Bindu.Sampatti.Settings
{
    public class SampattiSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(SampattiSettings.MySetting1));
        }
    }
}
