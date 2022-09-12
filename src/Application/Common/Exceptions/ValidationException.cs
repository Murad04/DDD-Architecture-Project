using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation.Results;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("One or more validation failures have occurred")
        {
            Failures = new Dictionary<string, string[]>();
        }


        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            var failureGroups = failures.
                GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailure = failureGroup.ToArray();

                Failures.Add(propertyName, propertyFailure);
            }
        }

        public IDictionary<string, string[]> Failures { get; }
    }
}
