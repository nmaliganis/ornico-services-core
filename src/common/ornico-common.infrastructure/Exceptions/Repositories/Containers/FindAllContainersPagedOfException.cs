using System;

namespace magic.button.common.infrastructure.Exceptions.Repositories.Containers
{
  public class FindAllContainersPagedOfException : Exception
  {
    private readonly string _messageDetails;

    public FindAllContainersPagedOfException(string messageDetails)
    {
      this._messageDetails = messageDetails;
    }

    public override string Message => $"Find all Containers error: {_messageDetails}";
  }
}
