using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Application.Bookings.Commands.RemoveBookingByWorkplace;
using ExoftOfficeManager.Application.Bookings.Queries;
using ExoftOfficeManager.Application.Bookings.Queries.FindBooking;
using ExoftOfficeManager.Application.Bookings.Queries.GetBookings;
using ExoftOfficeManager.Application.Bookings.Queries.GetBookingsByUser;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Controllers;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Tests.Helpers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace ExoftOfficeManager.Tests
{
    public class BookingControllerTests
    {
        [Fact]
        public async void GetBookings_GetsBookings()
        {
            #region Arrange
            DateTime date = new DateTime(2021, 10, 10);
            var list = new List<Booking>
            {
                new Booking { Date = date },
                new Booking { Date = new DateTime(2021, 12, 10) },
                new Booking { Date = date },
                new Booking { Date = new DateTime(2021, 10, 11) },
            };

            var getBookings = new Func<DateTime, IList<Booking>>(date => list.Where(x => x.Date.Value.Date == date).ToArray());

            var repository = new Mock<IBookingRepository>();
            repository
                .Setup(x => x.GetAllBookings(It.IsAny<DateTime>()))
                .ReturnsAsync(getBookings);

            var query = new GetBookingsQuery(date);
            var handler = new GetBookingsQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetBookingsQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new BookingController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetBookings(date)) as OkObjectResult).Value as BookingsQueryResponse[];
            #endregion

            #region Assert
            Assert.Equal(2, actual.Length);
            Assert.All(actual, x => Assert.Equal(date, x.Booking.Date.Value.Date));
            #endregion
        }

        [Fact]
        public async void GetBookingsByUser_GetsBookings()
        {
            #region Arrange
            var id = Guid.NewGuid();
            var list = new List<Booking>
            {
                new Booking { User = new User { Id = id } },
                new Booking { User = new User { Id = Guid.NewGuid() } },
                new Booking { User = new User { Id = id } },
            };

            var getBookings = new Func<Guid, IList<Booking>>(id => list.Where(x => x.User.Id == id).ToArray());

            var repository = new Mock<IBookingRepository>();
            repository
                .Setup(x => x.GetBookingsByUser(It.IsAny<Guid>()))
                .ReturnsAsync(getBookings);

            var query = new GetBookingsByUserQuery(id);
            var handler = new GetBookingsByUserQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetBookingsByUserQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new BookingController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetBookingsByUser(id)) as OkObjectResult).Value as BookingsQueryResponse[];
            #endregion

            #region Assert
            Assert.Equal(2, actual.Length);
            Assert.All(actual, x => Assert.Equal(id, x.Booking.User.Id));
            #endregion
        }

        [Fact]
        public async void RemoveBooking_RemovesBooking()
        {
            #region Arrange
            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var testDate = new DateTime(2021, 10, 10);

            var list = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = placeId,
                    Bookings = new List<Booking>
                    {
                        new Booking { Date = testDate, User = new User { Id = userId } },
                        new Booking { Date = new DateTime(2021, 10, 11), User = new User { Id = userId } },
                    }
                },
            };

            var remove = new Action<Guid>(id => list[0].Bookings.Remove(list[0].Bookings.FirstOrDefault(x => x.Id == id)));
            var findById = new Func<Guid, WorkPlace>(id => list.FirstOrDefault(x => x.Id == id));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.RemoveBooking(It.IsAny<Guid>()))
                .Callback(remove);

            bookingRepository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var placeRepository = new Mock<IWorkPlaceRepository>();
            placeRepository
                .Setup(x => x.FindWorkPlaceById(It.IsAny<Guid>()))
                .ReturnsAsync(findById);

            var command = new RemoveBookingByWorkplaceCommand(placeId, testDate, userId);
            var handler = new RemoveBookingByWorkplaceCommandHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<RemoveBookingByWorkplaceCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new BookingController(mediator.Object);
            #endregion

            #region Act
            await controller.RemoveBooking(placeId, testDate, userId);
            #endregion

            #region Assert
            Assert.Single(list[0].Bookings);
            Assert.All(list[0].Bookings, x => Assert.NotEqual(testDate, x.Date));
            #endregion
        }

        [Fact]
        public async void FindBookingById_FindsBooking()
        {
            #region Arrange
            var id = Guid.NewGuid();

            var list = new List<Booking>
            {
                new Booking { Id = id },
                new Booking { Id = id },
            };

            var findById = new Func<Guid, Booking>(id => list.FirstOrDefault(x => x.Id == id));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(findById);

            var placeRepository = new Mock<IWorkPlaceRepository>();

            var query = new FindBookingQuery(id);
            var handler = new FindBookingQueryHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<FindBookingQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new BookingController(mediator.Object);
            #endregion

            #region Act
            var actual = (await controller.FindBooking(id) as OkObjectResult).Value as BookingsQueryResponse;
            #endregion

            #region Assert
            Assert.Equal(id, actual.Booking.Id);
            #endregion
        }

        [Fact]
        public async void FindBookingByPlaceDateUser_FindsBooking()
        {
            #region Arrange
            var placeId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var testDate = new DateTime(2021, 10, 10);

            var list = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), WorkPlace = new WorkPlace { Id = placeId }, Date = testDate, User = new User{ Id = userId } },
                new Booking { Id = Guid.NewGuid(), Date = testDate, User = new User{ Id = userId } },
            };

            var placeList = new List<WorkPlace>
            {
                new WorkPlace
                {
                    Id = placeId,
                    Bookings = new[] { list[0], }
                },
                new WorkPlace
                {
                    Id = Guid.NewGuid(),
                    Bookings = new[] { list[1], }
                },
            };

            var findById = new Func<Guid, Booking>(id => list.FirstOrDefault(x => x.Id == id));
            var findPlaceById = new Func<Guid, WorkPlace>(id => placeList.FirstOrDefault(x => x.Id == id));

            var bookingRepository = new Mock<IBookingRepository>();
            bookingRepository
                .Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(findById);

            var placeRepository = new Mock<IWorkPlaceRepository>();
            placeRepository
                .Setup(x => x.FindWorkPlaceById(It.IsAny<Guid>()))
                .ReturnsAsync(findPlaceById);

            var query = new FindBookingQuery(placeId, testDate, userId);
            var handler = new FindBookingQueryHandler(bookingRepository.Object, placeRepository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<FindBookingQuery>(), default))
                .Returns(handler.Handle(query, default));

            var controller = new BookingController(mediator.Object);
            #endregion

            #region Act
            var actual = (await controller.FindBooking(placeId, testDate, userId) as OkObjectResult).Value as BookingsQueryResponse;
            #endregion

            #region Assert
            Assert.Equal(placeId, actual.Booking.WorkPlace.Id);
            Assert.Equal(testDate, actual.Booking.Date);
            Assert.Equal(userId, actual.Booking.User.Id);
            #endregion
        }
    }
}
