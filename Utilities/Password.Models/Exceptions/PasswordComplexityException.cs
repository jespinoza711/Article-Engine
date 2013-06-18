using System;
using System.Collections.Generic;

namespace Password.Models.Exceptions
{
    public class PasswordComplexityException : Exception
    {
        public PasswordComplexityException()
        {
            ComplexityErrors = new List<string>();
        }

        public IList<string> ComplexityErrors { get; set; }
    }
}