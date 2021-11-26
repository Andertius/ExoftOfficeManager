using System;

namespace ExoftOfficeManager.Domain.Exceptions
{
    public abstract class DatabaseException : Exception
    {
        public DatabaseException(string message)
            : base(message)
        {
        }
    }
}
