using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Service.Html
{
    public interface IHtmlService
    {
        string Sanitize(string htmlContent);
    }
}
