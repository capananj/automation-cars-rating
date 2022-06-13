namespace BuggyCars.Settings
{
    public class BrowserSettingsContext
    {
        public string Browser { get; }
        public bool IsHeadless { get; }

        public BrowserSettingsContext(AppSettingsLoader appSettingsLoader)
        {
            Browser = appSettingsLoader.Configuration["browser"];
            IsHeadless = bool.Parse(appSettingsLoader.Configuration["headless"]);
        }
    }
}
