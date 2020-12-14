using System;

namespace magic.button.common.infrastructure.Exceptions.Repositories.Containers
{
  public class MultipleContainersForAnIdException : Exception
  {
    private Guid _containerId;

    public MultipleContainersForAnIdException(Guid id)
    {
      this._containerId = id;
    }

    public override string Message => $"Multiple Containers found for: {_containerId}";
  }
}
