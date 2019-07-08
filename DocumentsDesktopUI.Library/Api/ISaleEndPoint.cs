using System.Threading.Tasks;
using DocumentsDesktopUI.Library.Models;

namespace DocumentsDesktopUI.Library.Api
{
    public interface ISaleEndPoint
    {
        Task PostSale(SaleModel sale);
    }
}