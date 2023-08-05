using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.ViewModels;
using eTickets.data;
using eTickets.data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Cinema
{
    public class CinemaServices : ICinemaServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CinemaServices(IFileService fileService , ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
            _fileService = fileService;
        }

		public async Task<Response<cinemaViewModel>> GetAllForDataTable(Request request)
		{
			Response<cinemaViewModel> response = new Response<cinemaViewModel>() { Draw = request.Draw };

			var data = _db.Cinemas.Where(x => !x.IsDelete);

			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
					x.Address.ToLower().Contains(request.Search.Value.ToLower())
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

			response.Data = _mapper.Map<IEnumerable<cinemaViewModel>>(await data.Skip(request.Start).Take(request.Length)
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



        public async Task<int> Create(CreateCinemaDto dto)
        {
            var isExite = _db.Cinemas.Any(x => !x.IsDelete && (x.Name == dto.Name));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Cinema = _mapper.Map<Cinemas>(dto);

            if (dto.Logo != null)
            {
                Cinema.Logo = await _fileService.SaveFile(dto.Logo, "Image");
            }
            await _db.Cinemas.AddAsync(Cinema);
            await _db.SaveChangesAsync();

            
            return Cinema.Id;
        }


        public async Task<int> Update(UpdateCinemaDto dto)
        {
            var isExite = _db.Cinemas.Any(x => !x.IsDelete && (x.Name == dto.Name) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var cinema = await _db.Cinemas.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var updatedcinema = _mapper.Map<UpdateCinemaDto, Cinemas>(dto, cinema);
            if (dto.Logo != null)
            {
                updatedcinema.Logo = await _fileService.SaveFile(dto.Logo, "Image");
            }
            _db.Cinemas.Update(updatedcinema);
            await _db.SaveChangesAsync();
            return cinema.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var Cinema = await _db.Cinemas.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Cinema == null)
            {
                throw new EntityNotFoundExecption();
            }
            Cinema.IsDelete = true;
            _db.Cinemas.Update(Cinema);
            await _db.SaveChangesAsync();
            return Cinema.Id;
        }

        public async Task<UpdateCinemaDto> Get(int Id)
        {
            var cinema = await _db.Cinemas.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (cinema == null)
            {
                throw new EntityNotFoundExecption();
            }
            var CinemaDto = _mapper.Map<UpdateCinemaDto>(cinema);
            
            return CinemaDto;

        }


    }
}
