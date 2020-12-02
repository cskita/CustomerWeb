using System;
using System.Web;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using CustomerWeb.Models.Common;

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

        public string GetQueryString(object obj)
        {

            var properties = from p in obj.GetType().GetProperties()
                             where p.GetValue(obj, null) != null
                             select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return String.Join("&", properties.ToArray());
        }
    }
}
