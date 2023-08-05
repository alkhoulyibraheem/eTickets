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

namespace eTickets.infrastructure.Services.Category
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _UserManger;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CategoryServices(IFileService fileService , ApplicationDbContext db, UserManager<User> userManger , IMapper mapper)
        {
            _db = db;
            _UserManger = userManger;
            _mapper = mapper;
            _fileService = fileService;
        }

		public async Task<Response<categoryViewModel>> GetAllForDataTable(Request request)
		{
			Response<categoryViewModel> response = new Response<categoryViewModel>() { Draw = request.Draw };

			var data = _db.Catgerys.Where(x => !x.IsDelete);

			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.Name.ToLower().Contains(request.Search.Value.ToLower()) ||
					x.Name.ToLower().Contains(request.Search.Value.ToLower())
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

			response.Data = _mapper.Map<IEnumerable<categoryViewModel>>(await data.Skip(request.Start).Take(request.Length)
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



        public async Task<int> Create(CreateCategoryDto dto)
        {
            var isExite = _db.Catgerys.Any(x => !x.IsDelete && (x.Name == dto.Name));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Category = new Categories();
            Category.Name = dto.Name;
            await _db.Catgerys.AddAsync(Category);
            await _db.SaveChangesAsync();
            
            return Category.Id;
        }


        public async Task<int> Update(UpdateCategoryDto dto)
        {
            var isExite = _db.Catgerys.Any(x => !x.IsDelete && (x.Name == dto.Name) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Category = await _db.Catgerys.SingleOrDefaultAsync(x => x.Id == dto.Id);
            Category.Name= dto.Name;
            _db.Catgerys.Update(Category);
            await _db.SaveChangesAsync();
            return Category.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var Category = await _db.Catgerys.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Category == null)
            {
                throw new EntityNotFoundExecption();
            }
            Category.IsDelete = true;
            _db.Catgerys.Update(Category);
            await _db.SaveChangesAsync();
            return Category.Id;
        }

        public async Task<UpdateCategoryDto> Get(int Id)
        {
            var user = await _db.Catgerys.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundExecption();
            }
            var Updated = new UpdateCategoryDto();
            Updated.Id= user.Id;
            Updated.Name=user.Name;
            return Updated ;

        }
    }
}
