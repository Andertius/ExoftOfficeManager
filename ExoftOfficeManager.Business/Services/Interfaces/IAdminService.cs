namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IAdminService
    {
        bool SetPermanentlyBooked(long id);

        bool CancelWorkPlaceReservation(long id);

        bool CancelMeeting(long id);
    }
}
