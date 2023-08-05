using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.Enums;
using eTickets.core.ViewModels;
using eTickets.data;
using eTickets.data.Models;
using eTickets.infrastructure.Services.Customers;
using eTickets.infrastructure.Services.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Customers
{
    public class CustomerServices : ICustomerServices
	{
        private readonly ApplicationDbContext _db;
        private readonly IUserServices _UserServices;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public CustomerServices(IFileService fileService , ApplicationDbContext db, IUserServices UserServices, IMapper mapper)
        {
            _db = db;
            _UserServices = UserServices;
            _mapper = mapper;
            _fileService = fileService;
        }

		public async Task<Response<CustomerViewModel>> GetAllForDataTable(Request request)
		{
			Response<CustomerViewModel> response = new Response<CustomerViewModel>() { Draw = request.Draw };

			var data = _db.Customers.Include(x => x.User).Where(x => !x.IsDelete);

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

			response.Data = _mapper.Map<IEnumerable<CustomerViewModel>>(await data.Skip(request.Start).Take(request.Length)
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



        public async Task<int> Create(CreateCustomerDto dto)
        {
            var isExite = _db.Customers.Any(x => !x.IsDelete && (x.Name == dto.User.FullName));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var UserId = await _UserServices.Create(dto.User);

            var Customer = _mapper.Map<Customer>(dto);

			Customer.UserId = UserId;
			Customer.Name = dto.User.FullName;

            if (dto.ImageURl != null)
            {
				Customer.ImageURl = await _fileService.SaveFile(dto.ImageURl, "Image");
            }
			Customer.User = null;
            await _db.Customers.AddAsync(Customer);
            await _db.SaveChangesAsync();
            return Customer.Id;
        }


        public async Task<int> Update(UpdateCustomerDto dto)
        {
            var isExite = _db.Customers.Any(x => !x.IsDelete && (x.Name == dto.Name) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var Customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var updatedCustomer = _mapper.Map<UpdateCustomerDto, Customer>(dto, Customer);
            if (dto.ImageURl != null)
            {
				updatedCustomer.ImageURl = await _fileService.SaveFile(dto.ImageURl, "Image");
            }
            _db.Customers.Update(updatedCustomer);
            await _db.SaveChangesAsync();
            return Customer.Id;
        }


        public async Task<int> Delete(int Id)
        {
            var Customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Customer == null)
            {
                throw new EntityNotFoundExecption();
            }
			Customer.IsDelete = true;
            _db.Customers.Update(Customer);
            await _db.SaveChangesAsync();
            return Customer.Id;
        }

        public async Task<UpdateCustomerDto> Get(int Id)
        {
            var Customer = await _db.Customers.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (Customer == null)
            {
                throw new EntityNotFoundExecption();
            }
            var CustomerDto = _mapper.Map<UpdateCustomerDto>(Customer);
            
            return CustomerDto;

        }


    }
}
