using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Patients
{
  public class PatientAlreadyExistsException : Exception
  {
    public string Mobile { get; }
    public string BrokenRules { get; }

    public PatientAlreadyExistsException(string mobile, string brokenRules)
    {
      Mobile = mobile;
      BrokenRules = brokenRules;
    }

    public override string Message => $" Patient with Mobile:{Mobile} already Exists!\n Additional info:{BrokenRules}";
  }
}
