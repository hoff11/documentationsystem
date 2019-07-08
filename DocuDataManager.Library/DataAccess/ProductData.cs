using DocuDataManager.Library.Internal.DataAccess;
using DocuDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuDataManager.Library.DataAccess
{
    public class ProductData
    {
        public List<ProductModel> Getproducts()
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProductLookup_GetAll", new { }, "DocuData");

            return output;
        }
        public ProductModel GetProductById(int productId)
        {
            SqlDataAccess sql = new SqlDataAccess();

            var output = sql.LoadData<ProductModel, dynamic>("dbo.spProductLookup_GetById", new { Id = productId}, "DocuData").FirstOrDefault();

            return output;
        }
    }
}
