using System.Collections.Generic;
using CustomerWeb.Models.City.ViewModel;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.City
{
    public interface ICityService
    {
        BaseResult<IEnumerable<CityViewModel>> Get();
    }
}
