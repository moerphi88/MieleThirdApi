using MieleThirdApi.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public interface ILoginManager
    {
        string GetAccessToken();
        Task<bool> IsLoggedIn();
        Task<bool> LoginAsync(Credential credential);
        bool Logout();
        Task<bool> Refresh();

        event EventHandler LoggedOut;

    }
}
