using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Infrastructure.Identity;

using Microsoft.AspNetCore.Identity;

namespace ExoftOfficeManager.Infrastructure
{
    public class SeedData
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly RoleManager<AppIdentityRole> _roleManager;

        private readonly List<User> users = new()
        {
            new User
            {
                FullName = "Norbert Moses",
                Avatar = "avatar_path",
                Role = UserRole.Admin,
                Email = "norses@generic.email.address",
            },

            new User
            {
                FullName = "John Doe",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "johndoe@example.com",
            },

            new User
            {
                FullName = "Painis Dickens",
                Avatar = "avatar_path",
                Role = UserRole.Admin,
                Email = "soldier@team.fortress",
            },

            new User
            {
                FullName = "Pootis Pencer",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "heavy@team.fortress",
            },

            new User
            {
                FullName = "Bob",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "boooooooob@aaa.aaa",
            },

            new User
            {
                FullName = "James Hetfield",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "meta@lli.ca",
            },

            new User
            {
                FullName = "Dave Mustaine",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "mega@de.th",
            },

            new User
            {
                FullName = "John Petrucci",
                Avatar = "avatar_path",
                Role = UserRole.Developer,
                Email = "dream@theat.er",
            }
        };

        public SeedData(UserManager<AppIdentityUser> userManger, RoleManager<AppIdentityRole> roleManager)
        {
            _userManager = userManger;
            _roleManager = roleManager;
        }

        public async Task EnsurePopulated(AppDbContext context, AppIdentityDbContext identityContext)
        {
            #region Roles
            string[] roleNames = { "Admin", "Developer" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);

                if (!roleExist)
                {
                    roleResult = await _roleManager.CreateAsync(new AppIdentityRole() { Name = roleName });
                }
            }
            #endregion

            #region IdentityUsers
            if (!identityContext.Users.Any())
            {
                foreach (var user in users)
                {
                    var userToAdd = new AppIdentityUser
                    {
                        UserName = user.Email.Split('@')[0],
                        Email = user.Email,
                    };

                    var result = await _userManager.CreateAsync(userToAdd, "Secret123$");

                    if (result.Succeeded)
                    {
                        await _userManager.AddToRoleAsync(userToAdd, Enum.GetName(typeof(UserRole), user.Role));
                    }
                }
            }
            #endregion

            #region Users
            if (!context.Users.Any())
            {
                context.Users.AddRange(users);
                context.SaveChanges();
            }
            #endregion

            #region WorkPlaces
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
            #endregion

            #region Meetings
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
            #endregion

            #region Bookings
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
            #endregion
        }
    }
}
