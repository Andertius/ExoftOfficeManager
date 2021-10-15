using ExoftOfficeManager.DataAccess;

namespace ExoftOfficeManager.Business.Services.Interfaces
{
    public interface IUserService
    {
        User Find(long id);
    }
}
