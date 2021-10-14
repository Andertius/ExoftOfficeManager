using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Business.Services.Interfaces;

namespace ExoftOfficeManager.Business.Services
{
    public class MockedAdminService : IAdminService
    {
        public bool CancelMeeting(long id)
        {
            throw new NotImplementedException();
        }

        public bool CancelWorkPlaceReservation(long id)
        {
            throw new NotImplementedException();
        }

        public bool SetPermanentlyBooked(long id)
        {
            throw new NotImplementedException();
        }
    }
}
