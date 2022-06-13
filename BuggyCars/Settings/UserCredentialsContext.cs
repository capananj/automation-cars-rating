namespace BuggyCars.Settings
{
    public class UserCredentialsContext
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public UserCredentialsContext(AppSettingsLoader appSettingsLoader)
        {
            Username = appSettingsLoader.Configuration["defaultValues:username"];
            Password = appSettingsLoader.Configuration["defaultValues:password"];
        }
    }
}
