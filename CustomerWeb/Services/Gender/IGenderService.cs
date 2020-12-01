using System.Collections.Generic;
using CustomerWeb.Models.Common;
using CustomerWeb.Models.Gender.ViewModel;

namespace CustomerWeb.Services.Gender
{
    public interface IGenderService
    {
        BaseResult<IEnumerable<GenderViewModel>> Get();
    }
}
