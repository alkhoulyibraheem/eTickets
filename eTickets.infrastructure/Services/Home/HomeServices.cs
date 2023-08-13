using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.Enums;
using eTickets.core.ViewModels;
using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Customers;
using eTickets.infrastructure.Services.Movie;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Home
{
    public class HomeServices : IHomeServices
	{
		private readonly IMovieServices _MovieServices;
		private readonly ApplicationDbContext _db;
		private RoleManager<IdentityRole> _roleManger;
		private readonly IMapper _mapper;
		private readonly ICustomerServices _CustomerServices;


		public HomeServices(IMapper mapper, ICustomerServices CustomerServices, RoleManager<IdentityRole> roleManger, IMovieServices movieServices, ApplicationDbContext db)
		{
			_MovieServices = movieServices;
			_db = db;
			_roleManger = roleManger;
			_CustomerServices = CustomerServices;
			_mapper = mapper;
		}


		public async Task<Response<MovieOrderViewModel>> GetAllForDataTable(Request request)
		{
			Response<MovieOrderViewModel> response = new Response<MovieOrderViewModel>() { Draw = request.Draw };

			var data = _db.MovieOrders.Include(x => x.Movie).Include(x => x.order).ThenInclude(x => x.User).Where(x => !x.IsDelete);
			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.Movie.Name.ToLower().Contains(request.Search.Value.ToLower()) 
				);
			}
			response.RecordsFiltered = await data.CountAsync();

			//if (request.Order != null && request.Order.Count > 0)
			//{
			//	var sortColumn = request.Columns.ElementAt(request.Order.FirstOrDefault().Column).Name;
			//	var sortDirection = request.Order.FirstOrDefault().Dir;

			//	if (sortDirection == "asc")
			//		data = data.OrderBy(sortColumn);
			//	else if (sortDirection == "desc")
			//		data = data.OrderByDescending(sortColumn);
			//}

			response.Data = _mapper.Map<IEnumerable<MovieOrderViewModel>>(await data.Skip(request.Start).Take(request.Length)
				.ToListAsync());

			return response;
		}


		public async Task<List<movieViewModel>> MoviesList()
		{
			return await _MovieServices.GetViewModels();
		}

		public async Task<movieViewModel> MovieProFile(int Id)
		{
			var movie =  await _db.Movies
				.Include(X => X.Actors).Include(X => X.Cinema).Include(X => X.Director).Include(X => X.Category)
				.FirstOrDefaultAsync(x => !x.IsDelete && x.Id == Id);

			if(movie == null)
			{
				throw new EntityNotFoundExecption();
			}

			var MovieDto = _mapper.Map<movieViewModel>(movie);
			var ListOfRating = _db.MovieRatings.Where(x => x.MoviId== Id).ToList();
			int Sum = 0;
			foreach(var rating in ListOfRating)
			{
				Sum += rating.Rating;
			}
			if (ListOfRating.Count() != 0)
			{
				Sum /= ListOfRating.Count();
			}
			else
			{
				Sum = 0;
			}
			

			MovieDto.Ratint= Sum;

			return MovieDto;

		}

		public async Task<int> buying(int Id ,string UserId)
		{
			
			var IsFound = await _db.Orders.FirstOrDefaultAsync(x => x.UserId == UserId);
			var Order = new Orders();
			if (IsFound == null)
			{
				Order.UserId = UserId;
				await _db.Orders.AddAsync(Order);
				_db.SaveChanges();
			}
			else
			{
				Order = IsFound;
			}


			var movie = await _db.Movies.FirstOrDefaultAsync(x => x.Id == Id);


			var IsExit = await _db.MovieOrders.FirstOrDefaultAsync(x => x.MovieId == Id && x.orderId == Order.Id);
			if (IsExit == null)
			{
				var buy = new MovieOrder();
				buy.MovieId = Id;
				buy.orderId = Order.Id;
				buy.Amount = 1;
				buy.Price = movie.Price;
				_db.MovieOrders.Add(buy);
				_db.SaveChanges();
			}
			else if (IsExit != null)
			{
				IsExit.Price += movie.Price;
				IsExit.Amount++;
				_db.MovieOrders.Update(IsExit);
				_db.SaveChanges();
			}
			return 1;

		}

		public async Task<List<MovieOrderViewModel>> OrderList(string userId)
		{

			var order = await _db.Orders.FirstOrDefaultAsync(x => x.UserId == userId);
			var data = new List<MovieOrder>();
			if (order == null)
			{
				return _mapper.Map<List<MovieOrderViewModel>>(data);

			}

			data = await _db.MovieOrders.Include(x => x.Movie).Where(x => x.orderId == order.Id).ToListAsync();

			return _mapper.Map<List<MovieOrderViewModel>> (data);
		}

		public async Task<int> CreateCustomer(CreateCustomerDto dto)
		{
			var a = await _CustomerServices.Create(dto);
			return a;
		}

		public async Task<int> rolos()
		{
			if (_db.Roles.Any())
			{
				var roles = new List<string>();
				roles.Add("Actor");
				roles.Add("director");
				roles.Add("Admin");
				roles.Add("Customer");
				foreach (var role in roles)
				{
					await _roleManger.CreateAsync(new IdentityRole(role));
				}
			}
			return 1;
		}

		public async Task<int> Rating(RatingDto dto)
		{
			var movie = await _db.Movies.FirstOrDefaultAsync(x => x.Id == dto.MoviId);
			if (movie == null)
			{
				throw new EntityNotFoundExecption();
			}

			var IsEixt = await _db.MovieRatings.FirstOrDefaultAsync(x => x.MoviId == dto.MoviId && x.UserId == dto.UserId);
			
			if (IsEixt == null)
			{
				var Rated = new MovieRating();
				Rated.MoviId = dto.MoviId;
				Rated.UserId = dto.UserId;
				Rated.Rating = dto.Rating;
				await _db.MovieRatings.AddAsync(Rated);
				_db.SaveChanges();
				return Rated.Id;
			}
			else
			{
				return -1;
			}
			
			
		}



		//public async Task<int> Rating(RatingDto dto)
		//{
		//	var movie = await _db.Movies.FirstOrDefaultAsync(x => x.Id == dto.Id);
		//	if(movie == null)
		//	{
		//		throw new EntityNotFoundExecption();
		//	}
		//	movie.NumberOfStars += dto.NumberOfStars;
		//	++movie.NumberRater;
		//	_db.Movies.Update(movie);
		//	_db.SaveChanges();
		//	return movie.Id;

		//}


	}
}
