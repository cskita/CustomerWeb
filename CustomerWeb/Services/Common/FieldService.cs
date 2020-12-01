using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using CustomerWeb.Models.Common;

namespace CustomerWeb.Services.Common
{
    public class FieldService : IFieldService
    {
        public IEnumerable<SelectListItem> GetDropDownList<T>(BaseResult<T> responseAPI)
        {
            if (responseAPI.Success)
            {
                return new SelectList
                (
                    (IEnumerable<T>)responseAPI.Data,
                    "Id",
                    "Name"
                );
            }

            return null;
        }
    }
}
