using System;
namespace Truextend.Scheduling.Logic.Exceptions
{
	public class AlreadyExistException : Exception
	{
		public AlreadyExistException(string message) : base(message) { }

        public AlreadyExistException(string message, Exception innerException) : base(string.Format(message), innerException) { }
    }
}

