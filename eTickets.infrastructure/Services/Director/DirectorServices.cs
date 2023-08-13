using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.Enums;
using eTickets.core.ViewModels;
using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Director
{
    public class DirectorServices : IDirectorServices
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserServices _UserServices;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public DirectorServices(IFileService fileService , ApplicationDbContext db, IUserServices UserServices, IMapper mapper)
        {
            _db = db;
            _UserServices = UserServices;
            _mapper = mapper;
            _fileService = fileService;
        }

		public async Task<Response<DirectorViewModel>> GetAllForDataTable(Request request)
		{
			Response<DirectorViewModel> response = new Response<DirectorViewModel>() { Draw = request.Draw };

			var data = _db.Directors.Include(x => x.User).Where(x => !x.IsDelete);

			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
					x.User.UserName.ToLower().Contains(request.Search.Value.ToLower())
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

			response.Data = _mapper.Map<IEnumerable<DirectorViewModel>>(await data.Skip(request.Start).Take(request.Length)
				.ToListAsync());

			return response;
		}

		//public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
  //      {
  //          var queryble = _db.Users.Where(x => !x.IsDelete).AsQueryable();
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



        public async Task<int> Create(CreateDirectorDto dto)
        {
            var isExite = _db.Directors.Any(x => !x.IsDelete && (x.Name == dto.User.FullName));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var UserId = await _UserServices.Create(dto.User);

            var Director = _mapper.Map<Directors>(dto);

            Director.UserId = UserId;
            Director.Name = dto.User.FullName;

            if (dto.ImageURl != null)
            {
                var ImageName = await _fileService.SaveImage(dto.ImageURl, "Image");
                if (ImageName =="1")
                {
                    return -1;
                }
                else
                {
					Director.ImageURl = ImageName;
				}

				
            }
            Director.User = null;
            await _db.Directors.AddAsync(Director);
            await _db.SaveChangesAsync();
            return Director.Id;
        }


        public async Task<int> Update(UpdateDirectorDto dto)
        {
            var isExite = _db.Directors.Any(x => !x.IsDelete && (x.Name == dto.Name) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Director = await _db.Directors.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var updatedDirector = _mapper.Map<UpdateDirectorDto, Directors>(dto, Director);
            if (dto.ImageURl != null)
            {
                var ImageName = await _fileService.SaveImage(dto.ImageURl, "Image");
                if (ImageName == "1")
                {
                    return -1;
                }
                else
                {
					updatedDirector.ImageURl = ImageName;
				}
				
            }
            _db.Directors.Update(updatedDirector);
            await _db.SaveChangesAsync();
            return Director.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var director = await _db.Directors.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (director == null)
            {
                throw new EntityNotFoundExecption();
            }
            director.IsDelete = true;
            _db.Directors.Update(director);
            await _db.SaveChangesAsync();
            return director.Id;
        }

        public async Task<UpdateDirectorDto> Get(int Id)
        {
            var director = await _db.Directors.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (director == null)
            {
                throw new EntityNotFoundExecption();
            }
            var directorDto = _mapper.Map<UpdateDirectorDto>(director);
            
            return directorDto;

        }


    }
}
