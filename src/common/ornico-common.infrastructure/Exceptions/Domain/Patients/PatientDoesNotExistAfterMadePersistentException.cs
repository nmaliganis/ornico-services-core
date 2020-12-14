using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Patients
{
  public class PatientDoesNotExistAfterMadePersistentException : Exception
  {
    public string Mobile { get; private set; }

    public PatientDoesNotExistAfterMadePersistentException(string mobile)
    {
      Mobile = mobile;
    }

    public override string Message => $" Patient with Mobile: {Mobile} was not made Persistent!";
  }
}