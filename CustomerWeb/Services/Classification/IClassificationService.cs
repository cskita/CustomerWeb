using System.Collections.Generic;
using CustomerWeb.Models.Common;
using ClassificationModel = CustomerWeb.Models.Classification.Classification;

namespace CustomerWeb.Services.Classification
{
    public interface IClassificationService
    {
        BaseResult<IEnumerable<ClassificationModel>> Get();
    }
}
