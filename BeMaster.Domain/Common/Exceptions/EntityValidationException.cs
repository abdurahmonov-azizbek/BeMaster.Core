// --------------------------------------------------------
// Copyright (c) Coalition of Good-Hearted Engineers
// Developed by Abdurahmonov-azizbek
// --------------------------------------------------------

namespace BeMaster.Domain.Common.Exceptions
{
    public class EntityValidationException : Exception
    {
        public EntityValidationException(Type type)
            : base($"{type.Name} validation error occured.")
        { }

        public EntityValidationException(string message)
            : base(message)
        { }
    }
}
