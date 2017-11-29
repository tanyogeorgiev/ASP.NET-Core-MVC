﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningSystem.Web.Infrastructures.Filters
{
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {

                var controller = context.Controller as Controller;
                if (controller == null)
                {
                    return;
                }

                var model = context.ActionArguments.FirstOrDefault(a => a.Key.Contains("model")).Value;

                if( model == null)
                {
                    return;
                }

                context.Result = controller.View(model);
            }
        }
    }
}
