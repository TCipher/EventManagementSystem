using AutoMapper;
using EventHub.TicketManagement.Application.Contracts.Infrastructure;
using EventHub.TicketManagement.Application.Contracts.Persistence;
using EventHub.TicketManagement.Application.Model.Mail;
using EventHub.TicketManagement.Domain.Entities;
using MediatR;

namespace EventHub.TicketManagement.Application.Features.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, Guid>
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;


        public CreateEventCommandHandler(IMapper mapper, IEventRepository eventRepository, IEmailService emailService)
        {
            _mapper = mapper;
            _eventRepository = eventRepository;
            _emailService = emailService;
        }

        public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            var @event = _mapper.Map<Event>(request);
            var validator = new CreateEventCommandValidator(_eventRepository);
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new Exceptions.ValidationException(validationResult);
          

            @event = await _eventRepository.AddAsync(@event);

            //Sending email notification to admin address
            var email = new Email()
            {
                To = "toching@gmail.com",
                Body = $"A New Event was Created: {request}",
                Subject = "A new Event was created"
            };
            try
            {
                await _emailService.SendEmail(email);
            }
            catch (Exception)
            {

                throw;
            }

            return @event.EventId;
        }
    }
}