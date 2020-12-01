using System.Collections.Generic;
using CustomerWeb.Models.Classification.ViewModel;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Classification
{
    public interface IClassificationService
    {
        BaseResult<IEnumerable<ClassificationViewModel>> Get();
    }
}
