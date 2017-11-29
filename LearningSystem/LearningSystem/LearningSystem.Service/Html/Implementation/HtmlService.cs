﻿using Ganss.XSS;
using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Service.Html.Implementation
{
    public class HtmlService : IHtmlService
    {
        public readonly HtmlSanitizer htmlSanitizer;

        public HtmlService()
        {
            this.htmlSanitizer = new HtmlSanitizer();
            this.htmlSanitizer.AllowedAttributes.Add("class");
        }

        public string Sanitize(string htmlContent)
            => this.htmlSanitizer.Sanitize(htmlContent);
    }
}
