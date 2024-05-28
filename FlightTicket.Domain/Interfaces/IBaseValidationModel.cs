using FlightTicket.Domain.Messages;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightTicket.Domain.Interfaces
{
    public interface IBaseValidationModel
    {
        public void Validate(object validator, IBaseValidationModel modelObj);
    }

    public abstract class BaseValidationModel<T> : IBaseValidationModel
    {
        public void Validate(object validator, IBaseValidationModel modelObj)
        {
            var instance = (IValidator<T>)validator;
            var result = instance.Validate((T)modelObj);

            if (!result.IsValid && result.Errors.Any())
            {
                throw new ValidationException(result.Errors);
                
            }
        }
    }
}
