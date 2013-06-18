using System.Collections.Generic;

namespace Password.Models
{
    public class PasswordComplexityMatch
    {
        public PasswordComplexityMatch()
        {
            ComplexityErrors = new List<string>();
        }

        public bool PasswordMeetComplexity { get; set; }
        public IList<string> ComplexityErrors { get; set; }
    }
}
