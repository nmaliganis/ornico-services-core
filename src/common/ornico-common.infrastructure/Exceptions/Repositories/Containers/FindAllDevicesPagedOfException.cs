using System;

namespace magic.button.common.infrastructure.Exceptions.Repositories.Containers
{
  public class FindAllDevicesPagedOfException : Exception
  {
    private readonly string _messageDetails;

    public FindAllDevicesPagedOfException(string messageDetails)
    {
      this._messageDetails = messageDetails;
    }

    public override string Message => $"Find all Devices error: {_messageDetails}";
  }
}