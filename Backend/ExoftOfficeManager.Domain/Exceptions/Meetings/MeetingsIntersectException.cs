namespace ExoftOfficeManager.Domain.Exceptions.Meetings
{
    public class MeetingsIntersectException : MeetingsException
    {
        public MeetingsIntersectException(string message)
            : base(message)
        {
        }
    }
}
