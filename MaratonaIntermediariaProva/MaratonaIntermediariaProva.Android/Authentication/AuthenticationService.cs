using Microsoft.WindowsAzure.MobileServices;
using MaratonaIntermediariaProva.Authentication;
using MaratonaIntermediariaProva.Droid.Authentication;
using MaratonaIntermediariaProva.Helpers;
using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(typeof(AuthenticationService))]
namespace MaratonaIntermediariaProva.Droid.Authentication
{
    public class AuthenticationService : IAuthentication
    {
        public async Task<MobileServiceUser> Authenticate(MobileServiceClient client, MobileServiceAuthenticationProvider provider, IDictionary<string, string> parameters = null)
        {
            try
            {
                var user = await client.LoginAsync(Forms.Context, provider);

                Settings.AuthToken = user?.MobileServiceAuthenticationToken ?? string.Empty;
                Settings.UserId = user?.UserId;

                return user;
            }
            catch (Exception)
            {
                //TODO: LogError
                return null;
            }
        }
    }
}