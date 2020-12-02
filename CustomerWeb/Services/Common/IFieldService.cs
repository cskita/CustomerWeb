using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Common
{
    public interface IFieldService
    {
        IEnumerable<SelectListItem> GetDropDownList<T>(BaseResult<T> responseAPI);
        string GetQueryString(object obj);
    }
}
