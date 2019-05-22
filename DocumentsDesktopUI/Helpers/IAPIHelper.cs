using System.Threading.Tasks;
using DocumentsDesktopUI.Models;

namespace DocumentsDesktopUI.Helpers
{
    public interface IAPIHelper
    {
        Task<AuthenticatedUser> Authenticate(string username, string password);
    }
}