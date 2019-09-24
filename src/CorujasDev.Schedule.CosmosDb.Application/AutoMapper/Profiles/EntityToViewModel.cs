using AutoMapper;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.Contact;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.TodoItem;
using CorujasDev.Schedule.CosmosDb.Application.ViewModel.User;
using CorujasDev.Schedule.CosmosDb.Domain.Entities;

namespace CorujasDev.Schedule.CosmosDb.Application.AutoMapper.Profiles
{
    public class EntityToViewModel : Profile
    {
        public EntityToViewModel()
        {
            CreateMap<ContactEntity, ContactViewModel>()
                .ReverseMap();

            CreateMap<TodoItemEntity, TodoItemViewModel>()
                .ReverseMap();

            CreateMap<UserEntity, UserViewModel>()
                .ReverseMap();
        }
    }
}
