using System.Collections.Generic;
using CustomerWeb.Models.Common;
using GenderModel = CustomerWeb.Models.Gender.Gender;

namespace CustomerWeb.Services.Gender
{
    public interface IGenderService
    {
        BaseResult<IEnumerable<GenderModel>> Get();
    }
}
