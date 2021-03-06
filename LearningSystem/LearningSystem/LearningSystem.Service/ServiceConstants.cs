﻿using System;
using System.Collections.Generic;
using System.Text;

namespace LearningSystem.Service
{
    public static class ServiceConstants
    {
        public const int BlogArticlesPageSize = 10;

        public const string PdfCertificateFormat = @"
<h1>Certificate</h1>
<h2>{3} - Grade: {4}</h2>
<br />
<h2>{0} Course</h2>
<h3>{1} - {2}</h3>
<h4>Signed By {5}</h4>
<h5>Issued date: {6}</h5>";
    }
}
