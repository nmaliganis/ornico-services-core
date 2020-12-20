using ornico.common.infrastructure.Domain;

namespace ornico.core.model.Users
{
    public class UserBusinessRules
    {
        public static BusinessRule Displayname => new BusinessRule("User", "User with Zero value in DisplayName is not valid!");
        public static BusinessRule Username => new BusinessRule("User", "User with Zero value in Username is not valid!");
        public static BusinessRule Password => new BusinessRule("User", "User with Zero value in Password is not valid!");
        public static BusinessRule Email => new BusinessRule("User", "User with Zero value in Email is not valid!");
    }
}