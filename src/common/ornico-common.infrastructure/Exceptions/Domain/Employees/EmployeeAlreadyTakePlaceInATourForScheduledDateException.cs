using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Employees
{
  public class EmployeeAlreadyTakePlaceInATourForScheduledDateException : Exception
  {
    public Guid EmployeeId { get; set; }
    public Guid ExistingTour { get; }
    public DateTime ScheduledDate { get; }

    public EmployeeAlreadyTakePlaceInATourForScheduledDateException(Guid employeeId, Guid existingTour, DateTime scheduledDate)
    {
      EmployeeId = employeeId;
      ExistingTour = existingTour;
      ScheduledDate = scheduledDate;
    }

    public override string Message => $"Employee with id: {EmployeeId} are already exist for Tour :{ExistingTour} at : {ScheduledDate}";
  }
}