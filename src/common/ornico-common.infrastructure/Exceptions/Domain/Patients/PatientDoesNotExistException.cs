using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Patients
{
    public class PatientDoesNotExistException : Exception
    {
        public Guid PatientId { get; }

        public PatientDoesNotExistException(Guid patientId)
        {
            PatientId = patientId;
        }

        public override string Message => $"Patient with Id: {PatientId}  doesn't exists!";
    }
}
