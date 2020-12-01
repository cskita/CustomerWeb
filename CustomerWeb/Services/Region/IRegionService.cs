using System.Collections.Generic;
using CustomerWeb.Models.Common;
using RegionModel = CustomerWeb.Models.Region.Region;

namespace CustomerWeb.Services.Region
{
    public interface IRegionService
    {
        BaseResult<IEnumerable<RegionModel>> Get();
        BaseResult<RegionModel> GetById(int? id);
    }
}
