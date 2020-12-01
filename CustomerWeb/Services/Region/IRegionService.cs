using System.Collections.Generic;
using CustomerWeb.Models.Region.ViewModel;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Region
{
    public interface IRegionService
    {
        BaseResult<IEnumerable<RegionViewModel>> Get();
    }
}
