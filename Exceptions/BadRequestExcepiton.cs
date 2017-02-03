using System;
using System.Runtime.Serialization;

namespace DiceGamingSystem.Exceptions
{
    [Serializable]
    internal class BadRequestExcepiton : Exception
    {
        public BadRequestExcepiton ( )
        {
        }

        public BadRequestExcepiton ( string message ) : base(message)
        {
        }

        public BadRequestExcepiton ( string message , Exception innerException ) : base(message , innerException)
        {
        }

        protected BadRequestExcepiton ( SerializationInfo info , StreamingContext context ) : base(info , context)
        {
        }
    }
}