using System;
using System.Collections.Generic;
using System.Linq;

namespace RazorRenderServices
{
    public class RazorValidationResult
    {
        public bool IsValid => Errors == null || !Errors.Any();

        public List<RazorValidationError> Errors { get; }

        public RazorValidationResult()
        {
            Errors = new List<RazorValidationError>();
        }

        public void AddError(string property, string message)
        {
            Errors.Add(new RazorValidationError
            {
                Property = property,
                Message = message
            });
        }

        public void AddError(RazorValidationError error)
        {
            if (error == null)
                throw new ArgumentNullException(nameof(error));

            Errors.Add(error);
        }
    }

    public class RazorValidationError
    {
        public string Property { get; set; }
        public string Message { get; set; }

        public override string ToString()
        {
            return $"{Property}: {Message}";
        }
    }
}