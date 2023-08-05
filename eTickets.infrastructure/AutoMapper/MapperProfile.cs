using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.ViewModels;
using eTickets.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eTickets.infrastructure.AutoMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {

            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<CreateUserDto, User>().ForMember(x => x.ImageURL, x => x.Ignore()).ForMember(x => x.Status, x => x.MapFrom(x => x.Status.ToString())).ForMember(x => x.UserType, x => x.MapFrom(x => x.UserType.ToString()));
            CreateMap<UpdateUserDto, User>().ForMember(x => x.ImageURL, x => x.Ignore());
            CreateMap<User, UpdateUserDto>().ForMember(x => x.ImageURL, x => x.Ignore());

            CreateMap<Categories, categoryViewModel>();
            CreateMap<categoryViewModel, Categories>();

            CreateMap<Cinemas,cinemaViewModel>();
            CreateMap<cinemaViewModel, Cinemas>();
            CreateMap<CreateCinemaDto, Cinemas>().ForMember(x => x.Logo, x => x.Ignore());
            CreateMap<UpdateCinemaDto, Cinemas>().ForMember(x => x.Logo, x => x.Ignore());
            CreateMap<Cinemas, UpdateCinemaDto>().ForMember(x => x.Logo, x => x.Ignore());


            CreateMap<Directors, DirectorViewModel>().ForMember(x => x.User, x => x.MapFrom(x => x.User)).ForMember(x => x.DOB, x => x.MapFrom(x => x.DOB.ToString("yyyy/MM/dd"))).ForMember(x => x.Gender, x => x.MapFrom(x => x.Gender.ToString()));
            CreateMap<DirectorViewModel, Directors>().ForMember(x => x.User, x => x.MapFrom(x => x.User));
            CreateMap<CreateDirectorDto, Directors>().ForMember(x => x.ImageURl, x => x.Ignore()).ForMember(x => x.User, x => x.MapFrom(x => x.User));
            CreateMap<UpdateDirectorDto, Directors>().ForMember(x => x.ImageURl, x => x.Ignore());
            CreateMap<Directors, UpdateDirectorDto>().ForMember(x => x.ImageURl, x => x.Ignore());

            CreateMap<Actors, ActorViewModel>().ForMember(x => x.User, x => x.MapFrom(x => x.User)).ForMember(x => x.DOB, x => x.MapFrom(x => x.DOB.ToString("yyyy/MM/dd"))).ForMember(x => x.Gender, x => x.MapFrom(x => x.Gender.ToString()));
            CreateMap<ActorViewModel, Actors>().ForMember(x => x.User, x => x.MapFrom(x => x.User));
            CreateMap<CreateActorDto, Actors>().ForMember(x => x.ImageURl, x => x.Ignore()).ForMember(x => x.User, x => x.MapFrom(x => x.User));
            CreateMap<UpdateActorDto, Actors>().ForMember(x => x.ImageURl, x => x.Ignore());
            CreateMap<Actors, UpdateActorDto>().ForMember(x => x.ImageURl, x => x.Ignore());


			CreateMap<Movies, movieViewModel>().ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString("yyyy/MM/dd"))).ForMember(x => x.StartDate, x => x.MapFrom(x => x.StartDate.ToString("yyyy/MM/dd"))).ForMember(x => x.EndDate, x => x.MapFrom(x => x.EndDate.ToString("yyyy/MM/dd"))).ForMember(x => x.Actors, x => x.MapFrom(x => x.Actors)).ForMember(x => x.Director, x => x.MapFrom(x => x.Director)).ForMember(x => x.Cinema, x => x.MapFrom(x => x.Cinema)).ForMember(x => x.Category, x => x.MapFrom(x => x.Category));
			CreateMap<movieViewModel, Movies>().ForMember(x => x.CreatedAt, x => x.MapFrom(x => x.CreatedAt.ToString())).ForMember(x => x.Actors, x => x.MapFrom(x => x.Actors)).ForMember(x => x.Director, x => x.MapFrom(x => x.Cinema)).ForMember(x => x.Director, x => x.MapFrom(x => x.Director)).ForMember(x => x.Category, x => x.MapFrom(x => x.Category));
			CreateMap<CreateMovieDto, Movies>().ForMember(x => x.ImageURL, x => x.Ignore()).ForMember(x => x.Actors, x => x.Ignore());
			CreateMap<UpdateMovieDto, Movies>().ForMember(x => x.ImageURL, x => x.Ignore()).ForMember(x => x.Actors, x => x.Ignore());
			CreateMap<Movies, UpdateMovieDto>().ForMember(x => x.ImageURL, x => x.Ignore()).ForMember(x => x.Actors, x => x.Ignore());


			CreateMap<Customer, CustomerViewModel>().ForMember(x => x.User, x => x.MapFrom(x => x.User)).ForMember(x => x.DOB, x => x.MapFrom(x => x.DOB.ToString("yyyy/MM/dd"))).ForMember(x => x.Gender, x => x.MapFrom(x => x.Gender.ToString()));
			CreateMap<CustomerViewModel, Customer>().ForMember(x => x.User, x => x.MapFrom(x => x.User));
			CreateMap<CreateCustomerDto, Customer>().ForMember(x => x.ImageURl, x => x.Ignore()).ForMember(x => x.User, x => x.MapFrom(x => x.User));
			CreateMap<UpdateCustomerDto, Customer>().ForMember(x => x.ImageURl, x => x.Ignore());
			CreateMap<Customer, UpdateCustomerDto>().ForMember(x => x.ImageURl, x => x.Ignore());


            CreateMap<Orders, OrderViewModel>().ForMember(x => x.User, x => x.MapFrom(x => x.User)).ForMember(x => x.Movies, x => x.MapFrom(x => x.Movies));


            CreateMap<MovieOrder, MovieOrderViewModel>().ForMember(x => x.Movie, x => x.MapFrom(x => x.Movie)).ForMember(x => x.order, x => x.MapFrom(x => x.order));

		}
    }
}
