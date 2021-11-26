namespace ExoftOfficeManager.Domain.Exceptions.Meetings
{
    public class MeetingsException : DatabaseException
    {
        public MeetingsException(string message)
            : base (message)
        {
        }
    }
}
