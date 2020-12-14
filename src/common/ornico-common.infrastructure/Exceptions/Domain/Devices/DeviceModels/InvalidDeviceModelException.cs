using System;

namespace magic.button.common.infrastructure.Exceptions.Domain.Devices.DeviceModels
{
    public class InvalidDeviceModelException : Exception
    {
        public string BrokenRules { get; private set; }

        public InvalidDeviceModelException(string brokenRules)
        {
            BrokenRules = brokenRules;
        }
    }
}