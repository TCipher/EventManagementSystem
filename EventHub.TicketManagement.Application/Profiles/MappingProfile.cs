using AutoMapper;
using EventHub.TicketManagement.Application.Features.Categories.Commands.CreateCateogry;
using EventHub.TicketManagement.Application.Features.Categories.Queries.GetCategoriesList;
using EventHub.TicketManagement.Application.Features.Categories.Queries.GetCategoriesListWithEvents;
using EventHub.TicketManagement.Application.Features.Events.Commands.CreateEvent;
using EventHub.TicketManagement.Application.Features.Events.Commands.UpdateEvent;
using EventHub.TicketManagement.Application.Features.Events.Queries.GetEventDetail;
using EventHub.TicketManagement.Application.Features.Events.Queries.GetEventList;
using EventHub.TicketManagement.Application.Features.Events.Queries.GetEventsExport;
using EventHub.TicketManagement.Application.Features.Orders.GetOrdersForMonth;
using EventHub.TicketManagement.Domain.Entities;

namespace EventHub.TicketManagement.Application.Profiles
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Event, EventListVm>().ReverseMap();
            CreateMap<Event, CreateEventCommand>().ReverseMap();
            CreateMap<Event, UpdateEventCommand>().ReverseMap();
            CreateMap<Event, EventDetailVm>().ReverseMap();
            CreateMap<Event, CategoryEventDto>().ReverseMap();
           CreateMap<Event, EventExportDto>().ReverseMap();

            CreateMap<Category, CategoryDto>();
            CreateMap<Category, CategoryListVm>();
            CreateMap<Category, CategoryEventListVm>();
            CreateMap<Category, CreateCategoryCommand>();
            CreateMap<Category, CreateCategoryDto>();

            CreateMap<Order, OrdersForMonthDto>();
        }
    }
}
