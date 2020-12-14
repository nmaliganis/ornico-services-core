using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Patients
{
    public class InvalidPatientException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidPatientException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}
