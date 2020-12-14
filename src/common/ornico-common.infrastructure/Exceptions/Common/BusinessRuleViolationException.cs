using System;

namespace ornico.common.infrastructure.Exceptions.Common
{
    public class BusinessRuleViolationException : Exception
    {
        public BusinessRuleViolationException(string incorrectTaskStatus) : base(incorrectTaskStatus)
        {
        }
    }
}