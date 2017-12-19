using System;
using System.Collections.Generic;
using System.Text;

namespace HealthR.Web.Infrastructure.Exstensions
{
   public static class TempDataDictionaryExtensions
    {
        public static void AddSuccessMessage(this ITempDataDictionary tempdata, string message)
        {
            tempdata[WebConstants.TempDataSuccessMessageKey] = message;
        }

        public static void AddErrorMessage(this ITempDataDictionary tempdata, string message)
        {

            tempdata[WebConstants.TempDataErrorMessageKey] = message;
        }
    }
}
