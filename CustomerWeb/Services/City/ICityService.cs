using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CityModel = CustomerWeb.Models.City.City;

namespace CustomerWeb.Services.City
{
    public interface ICityService
    {
        BaseResult<IEnumerable<CityModel>> Get();
        BaseResult<CityModel> GetById(int? id);
    }
}
