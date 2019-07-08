using DocuDataManager.Library.Internal.DataAccess;
using DocuDataManager.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuDataManager.Library.DataAccess
{
    public class SaleData
    {
        public void SaveSale(SaleModel saleInfo, string cashierId)
        {
            //TODO: make this solid/dry/better
            //start filling in models we will save to db
            List<SaleDetailDBModel> details = new List<SaleDetailDBModel>();
            ProductData products = new ProductData();
            var taxRate = ConfigHelper.GetTaxRate()/100;

            foreach (var item in saleInfo.SaleDetails)
            {
                var detail = new SaleDetailDBModel
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                };
                //get the info about this product
                var productInfo = products.GetProductById(detail.ProductId);
                if(productInfo == null)
                {
                    throw new Exception($"The product id of {detail.ProductId} could not be found in the database");
                }
                detail.PurchasePrice = (productInfo.RetailPrice * detail.Quantity);

                if (productInfo.IsTaxable)
                {
                    detail.Tax = (detail.PurchasePrice * taxRate);
                }

                details.Add(detail);
            }
            //fill in available info
            //create salemodel
            SaleDBModel sale = new SaleDBModel
            {
                SubTotal = details.Sum(x => x.PurchasePrice),
                Tax = details.Sum(x => x.Tax),
                CashierId = cashierId
            };
            sale.Total = sale.SubTotal + sale.Tax;

            //save the sale model
            SqlDataAccess sql = new SqlDataAccess();
            sql.SaveData("dbo.spSale_Insert", sale, "DocuData");

            //public List<ProductModel> Getproducts()
            //{
            //    SqlDataAccess sql = new SqlDataAccess();

            //    var output = sql.LoadData<ProductModel, dynamic>("dbo.spProductLookup_GetAll", new { }, "DocuData");

            //    return output;
            //}

            //get id from sale model
            sale.Id = sql.LoadData<int, dynamic>("dbo.spSale_LookUp", new { sale.CashierId, sale.SaleDate }, "DocuData").FirstOrDefault();

            //finish filling in the sale detail models
            foreach(var item in details)
            {
                item.SaleId = sale.Id;
                //save the sale deatil model
                sql.SaveData("dbo.spSaleDetail_Insert", item, "DocuData");
            }
        }
    }
}
