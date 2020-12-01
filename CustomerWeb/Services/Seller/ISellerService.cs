using System.Collections.Generic;
using CustomerWeb.Models.Common;
using SellerModel = CustomerWeb.Models.Seller.Seller;

namespace CustomerWeb.Services.Seller
{
    public interface ISellerService
    {
        BaseResult<IEnumerable<SellerModel>> Get();
    }
}
