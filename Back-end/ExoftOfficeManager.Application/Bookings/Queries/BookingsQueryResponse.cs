﻿using System;

using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Application.Bookings.Queries
{
    public class BookingsQueryResponse
    {
        public BookingsQueryResponse(BookingDto booking)
        {
            Booking = booking;
        }

        public BookingDto Booking { get; private set; }
    }
}
