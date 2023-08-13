using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.ViewModels;
using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Movie;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Cinema
{
    public class MovieServices : IMovieServices
	{
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public MovieServices(IFileService fileService , ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

		public async Task<Response<movieViewModel>> GetAllForDataTable(Request request)
		{
			Response<movieViewModel> response = new Response<movieViewModel>() { Draw = request.Draw };

			var data = _db.Movies.Where(x => !x.IsDelete);

			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
					x.Category.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Director.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Cinema.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
                    x.Cinema.Address.ToLower().Contains(request.Search.Value.ToLower())


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

			response.Data = _mapper.Map<IEnumerable<movieViewModel>>(await data.Skip(request.Start).Take(request.Length)
				.ToListAsync());

			return response;
		}

		//public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
  //      {
  //          var queryble = _db.Cinemas.Where(x => !x.IsDelete).AsQueryable();
  //          var count = queryble.Count();
  //          var skipValue = pagination.GetSkipValue();
  //          var dataList = await queryble.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
  //          var user = _mapper.Map<List<UserViewModel>>(dataList);

  //          var pages = pagination.GetPages(count);
  //          var re = new ResponseDto
  //          {
  //              data = user,
  //              meta = new Meta
  //              {
  //                  page = pagination.Page,
  //                  perpage = pagination.PerPage,
  //                  pages = pagination.Pages,
  //                  total = pagination.Total
  //              }
  //          };
  //          return re;
  //      }



        public async Task<int> Create(CreateMovieDto dto)
        {
            var isExite = _db.Movies.Any(x => !x.IsDelete && (x.Name == dto.Name));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Movie = _mapper.Map<Movies>(dto);

            if (dto.ImageURL != null)
            {
                var ImageName = await _fileService.SaveImage(dto.ImageURL, "Image"); ;
                if (ImageName == "1")
                {
                    return -1;
                }
                else
                {
					Movie.ImageURL = ImageName;
				}
				
            }
            await _db.Movies.AddAsync(Movie);
            await _db.SaveChangesAsync();

            foreach(var Actor in dto.Actors)
            {
                var movieActor = new movieActor();
                movieActor.MovieId = Movie.Id;
				movieActor.ActorId = Actor;
				await _db.movieActors.AddAsync(movieActor);
				await _db.SaveChangesAsync();

			}

            
            return Movie.Id;
        }


        public async Task<int> Update(UpdateMovieDto dto)
        {
            var isExite = _db.Movies.Any(x => !x.IsDelete && (x.Name == dto.Name) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Movie = await _db.Movies.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var updatedMovie = _mapper.Map<UpdateMovieDto, Movies>(dto, Movie);
            if (dto.ImageURL != null)
            {
                var ImageName = await _fileService.SaveImage(dto.ImageURL, "Image");
                if (ImageName == "1")
                {
                    return -1;
                }
                else
                {
					updatedMovie.ImageURL = ImageName;
				}
				
            }
            _db.Movies.Update(updatedMovie);
            await _db.SaveChangesAsync();


            var delet = _db.movieActors.Where(x => x.MovieId ==dto.Id).ToList();
            foreach (var movieActors in delet)
            {
				_db.movieActors.Remove(movieActors);
				await _db.SaveChangesAsync();
			}

				foreach (var Actor in dto.Actors)
			{
				var movieActor = new movieActor();
				movieActor.MovieId = Movie.Id;
				movieActor.ActorId = Actor;
				await _db.movieActors.AddAsync(movieActor);
				await _db.SaveChangesAsync();
			}
			return Movie.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var movie = await _db.Movies.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (movie == null)
            {
                throw new EntityNotFoundExecption();
            }
			movie.IsDelete = true;
            _db.Movies.Update(movie);
            await _db.SaveChangesAsync();
            return movie.Id;
        }

        public async Task<UpdateMovieDto> Get(int Id)
        {
            var Movie = await _db.Movies.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Movie == null)
            {
                throw new EntityNotFoundExecption();
            }
            var MovieDto = _mapper.Map<UpdateMovieDto>(Movie);

            var act = new List<int>();

            var MovieActor = _db.movieActors.Where(x => !x.IsDelete && x.MovieId == MovieDto.Id);

            foreach (var actor in MovieActor)
            {
                act.Add(actor.ActorId);
            }

			MovieDto.Actors= act;


			return MovieDto;

        }

        public async Task<List<movieViewModel>> GetViewModels()
        {
            var users = await _db.Movies.Include(x => x.Cinema).Include(x => x.Category).Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<Movies>, List<movieViewModel>>(users);
        }


    }
}
