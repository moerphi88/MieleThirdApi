using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MieleThirdApi.Data
{
    public interface ILoginManager
    {
        bool IsLoggedIn();
        Task<bool> LoginAsync();
        Task<bool> Logout();

        Task<bool> Refresh();

        event EventHandler LoggedOut;

    }
}
