

namespace LearningSystem.Web.Infrastructures.Extensions
{
    using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
