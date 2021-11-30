using System;
using System.Collections.Generic;
using System.Linq;

using ExoftOfficeManager.Application.Meetings.Commands.AddMeeting;
using ExoftOfficeManager.Application.Meetings.Commands.RemoveMeeting;
using ExoftOfficeManager.Application.Meetings.Queries;
using ExoftOfficeManager.Application.Meetings.Queries.FindMeetingById;
using ExoftOfficeManager.Application.Meetings.Queries.GetAvailableHours;
using ExoftOfficeManager.Application.Meetings.Queries.GetMeetings;
using ExoftOfficeManager.Application.Services.Repositories;
using ExoftOfficeManager.Controllers;
using ExoftOfficeManager.Domain.Entities;
using ExoftOfficeManager.Domain.Exceptions.Meetings;
using ExoftOfficeManager.Tests.Helpers;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace ExoftOfficeManager.Tests
{
    public class MeetingControllerTests
    {
        [Fact]
        public async void GetMeetings_GetsMeetings()
        {
            #region Arrange
            DateTime date = new DateTime(2021, 10, 10);
            var list = new List<Meeting>
            {
                new Meeting { Id = Guid.NewGuid(), DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0) },
                new Meeting { Id = Guid.NewGuid(), DateAndTime = new DateTime(2021, 10, 11, 13, 30, 0) },
                new Meeting { Id = Guid.NewGuid(), DateAndTime = new DateTime(2021, 10, 11, 13, 30, 0) },
            };

            var getMeetings = new Func<Meeting[]>(() => list.Where(x => x.DateAndTime.Date == date).ToArray());

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.GetAllMeetings(It.IsAny<DateTime>()))
                .ReturnsAsync(getMeetings);

            var command = new GetMeetingsQuery(date);
            var handler = new GetMeetingsQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetMeetingsQuery>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.GetAllMeetings(date)) as OkObjectResult).Value as MeetingsQueryResponse[];
            #endregion

            #region Assert
            Assert.Single(actual);
            Assert.All(actual, x => Assert.Equal(date, x.Meeting.DateAndTime.Date));
            #endregion
        }

        [Fact]
        public async void GetAllAvailableHours_GetsHours()
        {
            #region Arrange
            DateTime date = new DateTime(2021, 10, 10);
            int roomNumber = 1;
            var list = new List<Meeting>
            {
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                    RoomNumber = roomNumber,
                    Duration = new TimeSpan(0, 30, 0),
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 13, 30, 0),
                    RoomNumber = roomNumber,
                    Duration = new TimeSpan(0, 30, 0),
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 14, 00, 0),
                    RoomNumber = roomNumber,
                    Duration = new TimeSpan(0, 30, 0),
                },
            };

            var getMeetings = new Func<DateTime, Meeting[]>(input => list.Where(x => x.DateAndTime.Date == input).ToArray());

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.GetAllMeetings(It.IsAny<DateTime>()))
                .ReturnsAsync(getMeetings);

            var command = new GetAvailableHoursQuery(date, roomNumber);
            var handler = new GetAvailableHoursQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<GetAvailableHoursQuery>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller
                .GetAllAvailableHours(date, roomNumber)) as OkObjectResult)
                    .Value as GetAvailableHoursQueryResponse[];
            #endregion

            #region Assert
            var expected = new List<GetAvailableHoursQueryResponse>
            {
                new GetAvailableHoursQueryResponse(new TimeSpan(10, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(10, 30, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(11, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(11, 30, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(12, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(13, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(14, 30, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(15, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(15, 30, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(16, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(16, 30, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(17, 0, 0)),
                new GetAvailableHoursQueryResponse(new TimeSpan(17, 30, 0)),
            };

            Assert.Equal(expected, actual, new AvailableHoursResponseComparer());
            #endregion
        }

        [Fact]
        public async void FindMeeting_FindsMeeting()
        {
            #region Arrange
            Guid id = Guid.NewGuid();
            var list = new List<Meeting>
            {
                new Meeting { Id = Guid.NewGuid(), },
                new Meeting { Id = id, },
                new Meeting { Id = Guid.NewGuid(), },
            };

            var findMeeting = new Func<Guid, Meeting>(id => list.FirstOrDefault(x => x.Id == id));

            var repository = new Mock<IMeetingRepository>();

            repository
                .Setup(x => x.FindMeetingById(It.IsAny<Guid>()))
                .ReturnsAsync(findMeeting);

            var command = new FindMeetingByIdQuery(id);
            var handler = new FindMeetingByIdQueryHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<FindMeetingByIdQuery>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Act
            var actual = ((await controller.Find(id)) as OkObjectResult).Value as MeetingsQueryResponse;
            #endregion

            #region Assert
            Assert.Equal(id, actual.Meeting.Id);
            #endregion
        }

        [Fact]
        public async void ReserveMeeting_ReservesMeeting()
        {
            #region Arrange
            var list = new List<Meeting>
            {
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 13, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
            };

            var addMeeting = new Action<Meeting>(x => list.Add(x));
            var getMeetings = new Func<DateTime, Meeting[]>(date => list.Where(x => x.DateAndTime.Date == date).ToArray());

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.GetAllMeetings(It.IsAny<DateTime>()))
                .ReturnsAsync(getMeetings);

            repository
                .Setup(x => x.AddMeeting(It.IsAny<Meeting>()))
                .Callback(addMeeting);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var newMeeting = new Meeting
            {
                Id = Guid.NewGuid(),
                DateAndTime = new DateTime(2021, 10, 10, 10, 0, 0),
                Duration = new TimeSpan(0, 30, 0),
                RoomNumber = 1,
                MeetingPurpose = "test",
            };

            var command = new AddMeetingCommand(newMeeting);
            var handler = new AddMeetingCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<AddMeetingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Act
            await controller.ReserveMeeting(new Requests.ReserveMeetingRequest
            {
                DateAndTime = new DateTime(2021, 10, 10, 10, 0, 0),
                DurationMinutes = 30,
                RoomNumber = 1,
                MeetingPurpose = "test",
            });
            #endregion

            #region Assert
            Assert.Equal(4, list.Count);
            #endregion
        }

        [Fact]
        public async void ReserveMeeting_ThrowMeetingsIntersectException()
        {
            #region Arrange
            var list = new List<Meeting>
            {
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 13, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
            };

            var addMeeting = new Action<Meeting>(x => list.Add(x));
            var getMeetings = new Func<DateTime, Meeting[]>(date => list.Where(x => x.DateAndTime.Date == date).ToArray());

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.GetAllMeetings(It.IsAny<DateTime>()))
                .ReturnsAsync(getMeetings);

            repository
                .Setup(x => x.AddMeeting(It.IsAny<Meeting>()))
                .Callback(addMeeting);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var newMeeting = new Meeting
            {
                Id = Guid.NewGuid(),
                DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                Duration = new TimeSpan(0, 30, 0),
                RoomNumber = 1,
                MeetingPurpose = "test",
            };

            var command = new AddMeetingCommand(newMeeting);
            var handler = new AddMeetingCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<AddMeetingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Assert
            await Assert.ThrowsAsync<MeetingsIntersectException>(() =>
                controller.ReserveMeeting(new Requests.ReserveMeetingRequest
                {
                    DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                    DurationMinutes = 30,
                    RoomNumber = 1,
                    MeetingPurpose = "test",
                }));
            #endregion
        }

        [Fact]
        public async void CancelMeeting_Cancels_Meeting()
        {
            #region Arrange
            Guid id = Guid.NewGuid();
            var list = new List<Meeting>
            {
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 12, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = id,
                    DateAndTime = new DateTime(2021, 10, 10, 12, 30, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
                new Meeting
                {
                    Id = Guid.NewGuid(),
                    DateAndTime = new DateTime(2021, 10, 10, 13, 0, 0),
                    Duration = new TimeSpan(0, 30, 0),
                    RoomNumber = 1,
                },
            };

            var remove = new Action<Guid>(id => list.RemoveAll(x => x.Id == id));

            var repository = new Mock<IMeetingRepository>();
            repository
                .Setup(x => x.RemoveMeeting(It.IsAny<Guid>()))
                .Callback(remove);

            repository
                .Setup(x => x.Commit())
                .Callback(CommitHelper.MockedCommit);

            var command = new RemoveMeetingCommand(id);
            var handler = new RemoveMeetingCommandHandler(repository.Object);

            var mediator = new Mock<IMediator>();
            mediator
                .Setup(x => x.Send(It.IsAny<RemoveMeetingCommand>(), default))
                .Returns(handler.Handle(command, default));

            var controller = new MeetingController(mediator.Object);
            #endregion

            #region Act
            await controller.CancelMeeting(id);
            #endregion

            #region Assert
            Assert.Equal(2, list.Count);
            Assert.All(list, x => Assert.NotEqual(id, x.Id));
            #endregion
        }
    }
}
