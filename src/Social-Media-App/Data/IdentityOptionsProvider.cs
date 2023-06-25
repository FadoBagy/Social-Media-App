namespace Social_Media_App.Data
{
    using Microsoft.AspNetCore.Identity;

    public class IdentityOptionsProvider
    {
        public static void GetIdentityOptions(IdentityOptions options)
        {
            options.SignIn.RequireConfirmedAccount = true;

            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequiredLength = 6;
        }
    }
}
