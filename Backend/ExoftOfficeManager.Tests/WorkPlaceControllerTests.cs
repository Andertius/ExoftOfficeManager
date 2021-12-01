using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Application.Bookings.Commands.AddBooking;
using ExoftOfficeManager.Application.Mappers;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Application.WorkPlaces.Queries;
using ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceById;
using ExoftOfficeManager.Application.WorkPlaces.Queries.FindWorkPlaceByPlaceNumber;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetAvailableWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetBookedWorkPlaces;
using ExoftOfficeManager.Application.WorkPlaces.Queries.GetWorkPlaces;
using ExoftOfficeManager.Controllers;
using ExoftOfficeManager.Domain.Dtos;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Domain.Exceptions.Booking;
using ExoftOfficeManager.Requests;
using ExoftOfficeManager.Tests.Helpers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace ExoftOfficeManager.Tests
{
    public class WorkPlaceControllerTests
    {
        [Fact]
        public async void GetAllPlaces_GetsAll()
        {
            #region Arrange
            var list = new List<WorkPlace>
            {
                new WorkPlace { Id = Guid.NewGuid() },
                new WorkPlace { Id = Guid.NewGuid() },
                new WorkPlace { Id = Guid.NewGuid() },
            };

            var repository = new Mock<IWorkPlaceRepository>();
            repository
                .Setup(x => x.GetAllWorkPlaces())
                .ReturnsAsync(list);

            var query = new GetWorkPlacesQuery();
            var handler = new GetWorkPlacesQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetWorkPlacesQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new WorkPlaceController(mediator.Object);
#           endregion

            #region Act
            var actual = ((await controller.GetAll()) as OkObjectResult).Value as WorkPlacesQueryResponse[];
            #endregion

            #region Assert
            var expected = list.Select(x => new WorkPlacesQueryResponse(WorkPlaceMapper.MapIntoDto(x))).ToList();

            Assert.Equal(expected, actual, new WorkPlaceResponseComparer());
            #endregion
        }

        [Fact]
        public async void GetBooked_GetsPlaces()
        {
            #region Arrange
            DateTime date = new DateTime(2021, 10, 10);
            Guid id = Guid.NewGuid();
            var list = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = id,
                    Bookings = new[]
                    {
                        new Booking
                        {
                            Type = BookingType.Booked,
                            Date = date,
                        }
                    }
                },
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = new[]
                    {
                        new Booking
                        {
                            Type = BookingType.Booked,
                            Date = date.AddDays(1),
                        }
                    }
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = new List<Booking>() },
            };

            var repository = new Mock<IWorkPlaceRepository>();
            repository
                .Setup(x => x.GetAllBookedWorkPlaces(date))
                .ReturnsAsync(() => list.Where(x => x.Bookings.Any(x => x.Date == date)).ToArray());

            var query = new GetBookedWorkPlacesQuery(date);
            var handler = new GetBookedWorkPlacesQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetBookedWorkPlacesQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetBooked(date)) as OkObjectResult).Value as WorkPlacesQueryResponse[];
            #endregion

            #region Assert
            var expected = new[]
            {
                new WorkPlacesQueryResponse(
                    new WorkPlaceDto
                    {
                        Id = id,
                        Bookings = new[]
                        {
                            new BookingDto
                            {
                                Type = BookingType.Booked,
                                Date = date,
                            }
                        }
                    }),
            };

            Assert.Equal(expected, actual, new WorkPlaceResponseComparer());
            #endregion
        }

        [Fact]
        public async void GetAllAvailable_GetsAllAvailable()
        {
            #region Arrange
            DateTime date = new DateTime(2021, 10, 10);
            Guid id = Guid.NewGuid();
            Guid id2 = Guid.NewGuid();
            var list = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = new[]
                    {
                        new Booking
                        {
                            Type = BookingType.Booked,
                            Date = date,
                        }
                    }
                },
                new WorkPlace
                {
                    Id = id,
                    Bookings = new[]
                    {
                        new Booking
                        {
                            Type = BookingType.Booked,
                            Date = date.AddDays(1),
                        }
                    }
                },
                new WorkPlace { Id = id2, Bookings = new List<Booking>() },
            };

            var repository = new Mock<IWorkPlaceRepository>();
            repository
                .Setup(x => x.GetAllAvailableWorkPlaces(date))
                .ReturnsAsync(() => list.Where(x => x.Bookings.All(x => x.Date != date)).ToArray());

            var query = new GetAvailableWorkPlacesQuery(date);
            var handler = new GetAvailableWorkPlacesQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetAvailableWorkPlacesQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetAllAvailable(date)) as OkObjectResult).Value as WorkPlacesQueryResponse[];
            #endregion

            #region Assert
            var expected = new[]
            {
                new WorkPlacesQueryResponse(
                new WorkPlaceDto
                {
                    Id = id,
                    Bookings = new[]
                    {
                        new BookingDto
                        {
                            Type = BookingType.Booked,
                            Date = date.AddDays(1),
                        }
                    }
                }),
                new WorkPlacesQueryResponse(
                    new WorkPlaceDto
                    {
                        Id = id2,
                        Bookings = new List<BookingDto>(),
                    }),
            };

            Assert.Equal(expected, actual, new WorkPlaceResponseComparer());
            #endregion
        }

        [Fact]
        public async void FindWorkPlace_FindsPlace()
        {
            #region Arrange
            int floor = 1;
            int place = 1;
            Guid id = Guid.NewGuid();
            var list = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place,
                    FloorNumber = floor,
                },
                new WorkPlace
                {
                    Id = id,
                    Bookings = Array.Empty<Booking>(),
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = Array.Empty<Booking>() },
            };

            var repository = new Mock<IWorkPlaceRepository>();
            repository
                .Setup(x => x.FindWorkPlaceById(id))
                .ReturnsAsync(() => list.FirstOrDefault(x => x.Id == id));

            var query = new FindWorkPlaceByIdQuery(id);
            var handler = new FindWorkPlaceByIdQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<FindWorkPlaceByIdQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.FindWorkPlace(id)) as OkObjectResult).Value as WorkPlacesQueryResponse;
            #endregion

            #region Assert
            Assert.Equal(id, actual.WorkPlace.Id);
            #endregion
        }

        [Fact]
        public async void FindWorkPlaceByPlaceNumber_FindsPlace()
        {
            #region Arrange
            int floor = 1;
            int place = 1;
            Guid id = Guid.NewGuid();
            var list = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = id,
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place,
                    FloorNumber = floor,
                },
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place + 1,
                    FloorNumber = floor,
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = Array.Empty<Booking>() },
            };

            var repository = new Mock<IWorkPlaceRepository>();
            repository
                .Setup(x => x.FindWorkPlaceByPlaceNumber(place, floor))
                .ReturnsAsync(() => list.FirstOrDefault(x => x.PlaceNumber == place && x.FloorNumber == floor));

            var query = new FindWorkPlaceByPlaceNumberQuery(place, floor);
            var handler = new FindWorkPlaceByPlaceNumberQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<FindWorkPlaceByPlaceNumberQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.FindWorkPlaceByPlaceNumber(place, floor)) as OkObjectResult).Value as WorkPlacesQueryResponse;
            #endregion

            #region Assert
            Assert.Equal(id, actual.WorkPlace.Id);
            #endregion
        }

        [Fact]
        public async void Book_Books()
        {
            #region Arrange
            int floor = 1;
            int place = 1;
            DateTime date = new DateTime(2021, 10, 10);
            Guid workPlaceId = Guid.NewGuid();
            Guid bookingId = Guid.NewGuid();

            var bookingList = new List<Booking>
            {
                new Booking
                {
                    Id = bookingId,
                    Date = date,
                    Type = BookingType.Booked,
                    Status = BookingStatus.Approved,
                }
            };

            var placeList = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = new[] { bookingList[0] },
                    PlaceNumber = place,
                    FloorNumber = floor,
                },
                new WorkPlace
                {
                    Id = workPlaceId,
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place + 1,
                    FloorNumber = floor,
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = Array.Empty<Booking>() },
            };

            var addBooking = new Action<Booking>(x => bookingList.Add(x));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.AddBooking(It.IsAny<Booking>()))
                .Callback(addBooking);

            bookingRepository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var tryFindAvailable = new Func<Guid, DateTime, (bool, WorkPlace)>((id, date) =>
            {
                var place = placeList.FirstOrDefault(place => place.Id == id);

                if (!place.Bookings.Any(booking => booking.Date == date &&
                    (booking.Type == BookingType.Booked || booking.Type == BookingType.BookedPermanently)))
                {
                    return (true, place);
                }

                return (false, null);
            });

            var placeRepository = new Mock<IWorkPlaceRepository>();
            placeRepository
                .Setup(x => x.TryFindAvailableWorkPlace(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync(tryFindAvailable);

            Guid userId = Guid.NewGuid();
            var command = new AddBookingCommand(workPlaceId, userId, BookingType.Booked, date, 1);
            var handler = new AddBookingCommandHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<AddBookingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Act
            await controller.Book(workPlaceId, new BookWorkPlaceRequest
            {
                UserId = userId,
                BookingDate = date,
                BookingType = BookingType.Booked,
                Days = 1,
            });
            #endregion

            #region Assert
            Assert.Equal(2, bookingList.Count);
            #endregion
        }

        [Fact]
        public async void Book_ThrowsBookingStatusException()
        {
            #region Arrange
            int floor = 1;
            int place = 1;
            DateTime date = new DateTime(2021, 10, 10);
            Guid workPlaceId = Guid.NewGuid();
            Guid bookingId = Guid.NewGuid();

            var bookingList = new List<Booking>
            {
                new Booking
                {
                    Id = bookingId,
                    Date = date,
                    Type = BookingType.FirstHalfBooked,
                    Status = BookingStatus.Approved,
                }
            };

            var placeList = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = workPlaceId,
                    Bookings = new[] { bookingList[0] },
                    PlaceNumber = place,
                    FloorNumber = floor,
                },
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place + 1,
                    FloorNumber = floor,
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = Array.Empty<Booking>() },
            };

            var addBooking = new Action<Booking>(x => bookingList.Add(x));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.AddBooking(It.IsAny<Booking>()))
                .Callback(addBooking);

            bookingRepository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var tryFindAvailable = new Func<Guid, DateTime, (bool, WorkPlace)>((id, date) =>
            {
                var place = placeList.FirstOrDefault(place => place.Id == id);

                if (!place.Bookings.Any(booking => booking.Date == date &&
                    (booking.Type == BookingType.Booked || booking.Type == BookingType.BookedPermanently)))
                {
                    return (true, place);
                }

                return (false, null);
            });

            var placeRepository = new Mock<IWorkPlaceRepository>();
            placeRepository
                .Setup(x => x.TryFindAvailableWorkPlace(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync(tryFindAvailable);

            Guid userId = Guid.NewGuid();
            var command = new AddBookingCommand(workPlaceId, userId, BookingType.Booked, date, 1);
            var handler = new AddBookingCommandHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<AddBookingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<BookingStatusException>(() =>
                controller.Book(workPlaceId, new BookWorkPlaceRequest
                {
                    UserId = userId,
                    BookingDate = date,
                    BookingType = BookingType.FirstHalfBooked,
                    Days = 1,
                }));

            await Assert.ThrowsAsync<BookingStatusException>(() =>
                controller.Book(workPlaceId, new BookWorkPlaceRequest
                {
                    UserId = userId,
                    BookingDate = date,
                    BookingType = BookingType.Booked,
                    Days = 1,
                }));
            #endregion
        }

        [Fact]
        public async void Book_ThrowsPlaceAlreadyBookedException()
        {
            #region Arrange
            int floor = 1;
            int place = 1;
            DateTime date = new DateTime(2021, 10, 10);
            Guid workPlaceId = Guid.NewGuid();
            Guid bookingId = Guid.NewGuid();

            var bookingList = new List<Booking>
            {
                new Booking
                {
                    Id = bookingId,
                    Date = date,
                    Type = BookingType.Booked,
                    Status = BookingStatus.Approved,
                }
            };

            var placeList = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = workPlaceId,
                    Bookings = new[] { bookingList[0] },
                    PlaceNumber = place,
                    FloorNumber = floor,
                },
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = Array.Empty<Booking>(),
                    PlaceNumber = place + 1,
                    FloorNumber = floor,
                },
                new WorkPlace { Id = Guid.NewGuid(), Bookings = Array.Empty<Booking>() },
            };

            var addBooking = new Action<Booking>(x => bookingList.Add(x));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.AddBooking(It.IsAny<Booking>()))
                .Callback(addBooking);

            bookingRepository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var tryFindAvailable = new Func<Guid, DateTime, (bool, WorkPlace)>((id, date) =>
            {
                var place = placeList.FirstOrDefault(place => place.Id == id);

                if (!place.Bookings.Any(booking => booking.Date == date &&
                    (booking.Type == BookingType.Booked || booking.Type == BookingType.BookedPermanently)))
                {
                    return (true, place);
                }

                return (false, null);
            });

            var placeRepository = new Mock<IWorkPlaceRepository>();
            placeRepository
                .Setup(x => x.TryFindAvailableWorkPlace(It.IsAny<Guid>(), It.IsAny<DateTime>()))
                .ReturnsAsync(tryFindAvailable);

            Guid userId = Guid.NewGuid();
            var command = new AddBookingCommand(workPlaceId, userId, BookingType.FirstHalfBooked, date, 1);
            var handler = new AddBookingCommandHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<AddBookingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new WorkPlaceController(mediator.Object);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<PlaceAlreadyBookedException>(() =>
                controller.Book(workPlaceId, new BookWorkPlaceRequest
                {
                    UserId = userId,
                    BookingDate = date,
                    BookingType = BookingType.FirstHalfBooked,
                    Days = 1,
                }));
            #endregion
        }
    }
}
