using FluentValidation.Results;
using System;
using System.Collections.Generic;

namespace Bcan.Backend.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IList<string> Errors { get; private set; }

        public ValidationException(ValidationResult validationResult)
        {
            Errors = new List<string>();
            foreach (var e in validationResult.Errors)
                Errors.Add(e.ErrorMessage);
        }
    }

}