using System;
using System.Linq;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;

namespace ExoftOfficeManager.Infrastructure
{
    public class SeedData
    {
        public static void EnsurePopulated(AppDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                    new User { FullName = "Norbert Moses", Avatar = "avatar_path", Role = UserRole.Admin },
                    new User { FullName = "John Doe", Avatar = "avatar_path", Role = UserRole.Developer },
                    new User { FullName = "Painis Dickens", Avatar = "avatar_path", Role = UserRole.Admin },
                    new User { FullName = "Pootis Pencer", Avatar = "avatar_path", Role = UserRole.Developer },
                    new User { FullName = "Bob", Avatar = "avatar_path", Role = UserRole.Developer },
                    new User { FullName = "James Hetfield", Avatar = "avatar_path", Role = UserRole.Developer },
                    new User { FullName = "Dave Mustaine", Avatar = "avatar_path", Role = UserRole.Developer },
                    new User { FullName = "John Petrucci", Avatar = "avatar_path", Role = UserRole.Developer }
                );

                context.SaveChanges();
            }

            if (!context.WorkPlaces.Any())
            {
                context.WorkPlaces.AddRange(
                    new WorkPlace { FloorNumber = 5, PlaceNumber = 1 },
                    new WorkPlace { FloorNumber = 5, PlaceNumber = 2 },
                    new WorkPlace { FloorNumber = 5, PlaceNumber = 3 },
                    new WorkPlace { FloorNumber = 5, PlaceNumber = 4 },
                    new WorkPlace { FloorNumber = 5, PlaceNumber = 5 },
                    new WorkPlace { FloorNumber = 4, PlaceNumber = 1 },
                    new WorkPlace { FloorNumber = 4, PlaceNumber = 2 },
                    new WorkPlace { FloorNumber = 4, PlaceNumber = 3 },
                    new WorkPlace { FloorNumber = 4, PlaceNumber = 4 },
                    new WorkPlace { FloorNumber = 4, PlaceNumber = 5 }
                );

                context.SaveChanges();
            }

            if (!context.Meetings.Any())
            {
                context.Meetings.AddRange(
                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0),
                        Duration = new TimeSpan(0, 30, 0),
                        RoomNumber = 1,
                        MeetingPurpose = "Talk about the future",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "Norbert Moses")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 10, 30, 0),
                        Duration = new TimeSpan(0, 30, 0),
                        RoomNumber = 1,
                        MeetingPurpose = "Meeting with client",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "John Doe")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 16, 11, 30, 0),
                        Duration = new TimeSpan(0, 30, 0),
                        RoomNumber = 1,
                        MeetingPurpose = "Play call of duty",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "Painis Dickens")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 12, 0, 0),
                        Duration = new TimeSpan(2, 0, 0),
                        RoomNumber = 1,
                        MeetingPurpose = "Have some tea",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "Pootis Pencer")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 10, 0, 0),
                        Duration = new TimeSpan(0, 30, 0),
                        RoomNumber = 2,
                        MeetingPurpose = "To poop",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "Bob")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 11, 0, 0),
                        Duration = new TimeSpan(1, 0, 0),
                        RoomNumber = 2,
                        MeetingPurpose = "Stand up comedy",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "James Hetfield")
                    },

                    new Meeting
                    {
                        DateAndTime = new DateTime(2021, 10, 15, 13, 0, 0),
                        Duration = new TimeSpan(1, 30, 0),
                        RoomNumber = 2,
                        MeetingPurpose = "Look at memes",
                        Owner = context.Users.FirstOrDefault(user => user.FullName == "John Petrucci")
                    }
                );

                context.SaveChanges();
            }

            if (!context.Bookings.Any())
            {
                context.Bookings.AddRange(
                    new Booking
                    {
                        Date = null,
                        User = context.Users.FirstOrDefault(user => user.FullName == "Norbert Moses"),
                        Type = BookingType.BookedPermanently,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 2),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = null,
                        User = context.Users.FirstOrDefault(user => user.FullName == "John Doe"),
                        Type = BookingType.BookedPermanently,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 3),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 10),
                        User = context.Users.FirstOrDefault(user => user.FullName == "Bob"),
                        Type = BookingType.FirstHalfBooked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 5),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 10),
                        User = context.Users.FirstOrDefault(user => user.FullName == "James Hetfield"),
                        Type = BookingType.Booked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 4 && x.PlaceNumber == 1),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 10),
                        User = context.Users.FirstOrDefault(user => user.FullName == "Painis Dickens"),
                        Type = BookingType.Booked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 4 && x.PlaceNumber == 5),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 11),
                        User = context.Users.FirstOrDefault(user => user.FullName == "Pootis Pencer"),
                        Type = BookingType.FirstHalfBooked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 1),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 11),
                        User = context.Users.FirstOrDefault(user => user.FullName == "Bob"),
                        Type = BookingType.SecondHalfBooked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 1),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 11),
                        User = context.Users.FirstOrDefault(user => user.FullName == "John Petrucci"),
                        Type = BookingType.Booked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 5 && x.PlaceNumber == 4),
                        Status = BookingStatus.Approved
                    },

                    new Booking
                    {
                        Date = new DateTime(2021, 10, 11),
                        User = context.Users.FirstOrDefault(user => user.FullName == "Painis Dickens"),
                        Type = BookingType.Booked,
                        WorkPlace = context.WorkPlaces.FirstOrDefault(x => x.FloorNumber == 4 && x.PlaceNumber == 5),
                        Status = BookingStatus.Approved
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
