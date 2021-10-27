﻿using System;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.CommandHandlers.Interfaces
{
    public interface IWorkPlaceCommandHandler
    {
        Task BookCommand(long id, long developerId, BookingType status, DateTime date, int days);

        Task UpdateCommand(WorkPlace place);

        Task RemoveCommand(long id);
    }
}