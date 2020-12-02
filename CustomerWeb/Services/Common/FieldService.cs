using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using CustomerWeb.Models.Common;
using System;
using System.Collections;

namespace CustomerWeb.Services.Common
{
    public class FieldService : IFieldService
    {
        public IEnumerable<SelectListItem> GetDropDownList<T>(BaseResult<T> responseAPI)
        {
            if (responseAPI.Success && responseAPI.Data != null)
            {
                if (responseAPI.Data is IList)
                    return new SelectList
                    (
                        (IEnumerable<object>)responseAPI.Data,
                        "Id",
                        "Name"
                    );
                else
                    return new SelectList
                    (
                        new[] { responseAPI.Data } ,
                        "Id",
                        "Name"
                    );


            }

            return null;
        }
    }
}
