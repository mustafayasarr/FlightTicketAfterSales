﻿using FlightTicket.Domain.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading;

namespace FlightTicket.API.Filter
{
    public class ModelValidatorAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var actionArgument in context.ActionArguments)
            {
                //validate that model is having validator and resolve it
                if (actionArgument.Value is IBaseValidationModel model)
                {
                    var modelType = actionArgument.Value.GetType();
                    var genericType = typeof(IValidator<>).MakeGenericType(modelType);
                    var validator = context.HttpContext.RequestServices.GetService(genericType);

                    if (validator != null)
                    {
                        // execute validator to validate model
                        model.Validate(validator, model);
                    
                    }
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
