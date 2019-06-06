using System.Collections.Generic;
using System.Threading.Tasks;
using DocumentsDesktopUI.Library.Models;

namespace DocumentsDesktopUI.Library.Api
{
    public interface IProductEndpoint
    {
        Task<List<ProductModel>> GetAll();
    }
}