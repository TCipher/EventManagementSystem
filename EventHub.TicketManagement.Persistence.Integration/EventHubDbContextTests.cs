
using EventHub.TicketManagement.Application.Contracts;
using EventHub.TicketManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using Shouldly;

namespace EventHub.TicketManagement.Persistence.IntegrationTests
{
    public class EventHubDbContextTests
    {
        private readonly EventHubDbContext _eventHubDbContext;
        private readonly Mock<ILoggedInUserService> _loggedInUserServiceMock;
        private readonly string _loggedInUserId;

        public EventHubDbContextTests()
        {
            var dbContextOptions = new DbContextOptionsBuilder<EventHubDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _loggedInUserId = "00000000-0000-0000-0000-000000000000";
            _loggedInUserServiceMock = new Mock<ILoggedInUserService>();
            _loggedInUserServiceMock.Setup(m => m.UserId).Returns(_loggedInUserId);

            _eventHubDbContext = new EventHubDbContext(dbContextOptions, _loggedInUserServiceMock.Object);
        }

        [Fact]
        public async void Save_SetCreatedByProperty()
        {
            var ev = new Event() {EventId = Guid.NewGuid(), Name = "Test event" };

            _eventHubDbContext.Events.Add(ev);
            await _eventHubDbContext.SaveChangesAsync();

            ev.CreatedBy.ShouldBe(_loggedInUserId);
        }
    }
}
