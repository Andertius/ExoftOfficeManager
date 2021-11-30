using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ExoftOfficeManager.Application.Bookings.Commands.ChangeBookingStatus;
using ExoftOfficeManager.Application.Bookings.Queries;
using ExoftOfficeManager.Application.Bookings.Queries.GetPendingBookings;
using ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Controllers;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Enums;
using ExoftOfficeManager.Tests.Helpers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace ExoftOfficeManager.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public async void CancelMeeting_Cancels_Meeting()
        {
            #region Arrange
            Guid id = Guid.NewGuid();
            var list = new List<Meeting> { new Meeting { Id = id }, new Meeting { Id = Guid.NewGuid() } };
            var remove = new Action(() => { list.RemoveAll(x => x.Id == id); });

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.RemoveMeeting(It.IsAny<Guid>()))
                .Callback(remove);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var command = new RemoveMeetingCommand(Guid.NewGuid());
            var handler = new RemoveMeetingCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<RemoveMeetingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new AdminController(mediator.Object);
            #endregion

            #region Act
            await controller.CancelMeeting(id);
            #endregion

            #region Assert
            Assert.Single(list);
            #endregion
        }

        [Fact]
        public async void GetAllPendingBookings_GetsAllPendingBookings()
        {
            #region Arrange
            var list = new List<Booking>
            {
                new Booking { Status = BookingStatus.Approved },
                new Booking { Status = BookingStatus.Approved },
                new Booking { Status = BookingStatus.Pending },
                new Booking { Status = BookingStatus.Pending },
                new Booking { Status = BookingStatus.Approved },
            };

            var repository = new Mock<IBookingRepository>();
            repository
                .Setup(x => x.GetAllPendingBookings())
                .ReturnsAsync(() => list.Where(x => x.Status == BookingStatus.Pending).ToArray());

            var command = new GetPendingBookingsQuery();
            var handler = new GetPendingBookingsQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetPendingBookingsQuery>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new AdminController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetAllPendingBookings()) as OkObjectResult).Value as BookingsQueryResponse[];
            #endregion

            #region Assert
            Assert.All(actual, x => Assert.Equal(BookingStatus.Pending, x.Booking.Status));
            Assert.Equal(2, actual.Length);
            #endregion
        }

        [Fact]
        public async void ApproveBooking_ApprovesBooking()
        {
            #region Arrange
            Guid id = Guid.NewGuid();
            var list = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
                new Booking { Id = id, Status = BookingStatus.Pending },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Pending },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
            };

            var update = new Action<Booking>(x => list[list.IndexOf(list.Find(y => x.Id == y.Id))] = x);
            var findById = new Func<Guid, Booking>(id => list.Find(x => x.Id == id));

            var repository = new Mock<IBookingRepository>();
            repository
                .Setup(x => x.UpdateBooking(It.IsAny<Booking>()))
                .Callback(update);

            repository
                .Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(findById);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var command = new ChangeBookingStatusCommand(id, BookingStatus.Approved);
            var handler = new ChangeBookingStatusCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<ChangeBookingStatusCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new AdminController(mediator.Object);
            #endregion

            #region Act
            await controller.ApproveBooking(id);
            #endregion

            #region Assert
            var actual = list.Find(x => x.Id == id).Status;
            Assert.Equal(BookingStatus.Approved, actual);
            #endregion
        }

        [Fact]
        public async void DeclineBooking_DeclinesBooking()
        {
            #region Arrange
            Guid id = Guid.NewGuid();
            var list = new List<Booking>
            {
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
                new Booking { Id = id, Status = BookingStatus.Pending },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Pending },
                new Booking { Id = Guid.NewGuid(), Status = BookingStatus.Approved },
            };

            var update = new Action<Booking>(x => list[list.IndexOf(list.Find(y => x.Id == y.Id))] = x);
            var findById = new Func<Guid, Booking>(id => list.Find(x => x.Id == id));

            var repository = new Mock<IBookingRepository>();
            repository
                .Setup(x => x.UpdateBooking(It.IsAny<Booking>()))
                .Callback(update);

            repository
                .Setup(x => x.FindById(It.IsAny<Guid>()))
                .ReturnsAsync(findById);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var command = new ChangeBookingStatusCommand(id, BookingStatus.Declined);
            var handler = new ChangeBookingStatusCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<ChangeBookingStatusCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new AdminController(mediator.Object);
            #endregion

            #region Act
            await controller.DeclineBooking(id);
            #endregion

            #region Assert
            var actual = list.Find(x => x.Id == id).Status;
            Assert.Equal(BookingStatus.Declined, actual);
            #endregion
        }
    }
}
