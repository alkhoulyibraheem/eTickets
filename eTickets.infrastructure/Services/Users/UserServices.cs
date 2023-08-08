using AutoMapper;
using eTickets.core.Dto;
using eTickets.core.Eceptions;
using eTickets.core.Enums;
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

namespace eTickets.infrastructure.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _UserManger;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;
		private readonly IEmailService _emailService;

		public UserServices(IEmailService emailService , IFileService fileService , ApplicationDbContext db, UserManager<User> userManger , IMapper mapper)
        {
            _db = db;
            _UserManger = userManger;
            _mapper = mapper;
            _fileService = fileService;
            _emailService = emailService;
        }
		

		public UserViewModel GetUserByUsername(string username)
        {
            var user = _db.Users.SingleOrDefault(x => x.UserName == username && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundExecption();
            }
            return _mapper.Map<UserViewModel>(user);
        }

        public async Task<Response<UserViewModel>> GetAllForDataTable(Request request)
		{
			Response<UserViewModel> response = new Response<UserViewModel>() { Draw = request.Draw };

			var data = _db.Users.Where(x => !x.IsDelete);

			response.RecordsTotal = data.Count();

			if (request.Search.Value != null)
			{
				data = data.Where(x =>
					string.IsNullOrEmpty(request.Search.Value) ||
					x.FullName.ToLower().Contains(request.Search.Value.ToLower()) ||
					x.UserName.ToLower().Contains(request.Search.Value.ToLower())
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

			response.Data = _mapper.Map<IEnumerable<UserViewModel>>(await data.Skip(request.Start).Take(request.Length)
				.ToListAsync());

			return response;
		}

		public async Task<ResponseDto> GetAll(Pagination pagination, Query query)
        {
            var queryble = _db.Users.Where(x => !x.IsDelete).AsQueryable();
            var count = queryble.Count();
            var skipValue = pagination.GetSkipValue();
            var dataList = await queryble.Skip(skipValue).Take(pagination.PerPage).ToListAsync();
            var user = _mapper.Map<List<UserViewModel>>(dataList);

            var pages = pagination.GetPages(count);
            var re = new ResponseDto
            {
                data = user,
                meta = new Meta
                {
                    page = pagination.Page,
                    perpage = pagination.PerPage,
                    pages = pagination.Pages,
                    total = pagination.Total
                }
            };
            return re;
        }



        public async Task<string> Create(CreateUserDto dto)
        {
            var isExite = _db.Users.Any(x => !x.IsDelete && (x.PhoneNumber == dto.PhoneNumber || x.Email == dto.Email));
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var user = _mapper.Map<User>(dto);

            user.UserName = dto.Email;
            if (dto.ImageURL != null)
            {
                user.ImageURL = await _fileService.SaveFile(dto.ImageURL, "Image");
            }

			var password = GenratePassword();

			var result = await _UserManger.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new OperationFailedException();
            }

			await _emailService.Send(user.Email, "New Account !", $"Username is : {user.Email} and Password is {password}");

			if (dto.UserType ==UserType.Admin)
			{
				await _UserManger.AddToRoleAsync(user, "Admin");
			}else if (dto.UserType == UserType.director)
			{
				await _UserManger.AddToRoleAsync(user, "director");
			}
			else if (dto.UserType == UserType.Actor)
			{
				await _UserManger.AddToRoleAsync(user, "Actor");
			}
			else if (dto.UserType == UserType.Customer)
			{
				await _UserManger.AddToRoleAsync(user, "Customer");
			}
			return user.Id;
        }


        public async Task<string> Update(UpdateUserDto dto)
        {
            var isExite = _db.Users.Any(x => !x.IsDelete && (x.PhoneNumber == dto.PhoneNumber || x.Email == dto.Email) && x.Id != dto.Id);
            if (isExite)
            {
                throw new DoublictPhoneOrEmailEexption();
            }
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == dto.Id);
            var updatedUser = _mapper.Map<UpdateUserDto, User>(dto, user);
            if (dto.ImageURL != null)
            {
                updatedUser.ImageURL = await _fileService.SaveFile(dto.ImageURL, "Image");
            }
            _db.Users.Update(updatedUser);
            await _db.SaveChangesAsync();
            return user.Id;
        }


        public async Task<string> Delete(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundExecption();
            }
            user.IsDelete = true;
            _db.Users.Update(user);
            await _db.SaveChangesAsync();
            return user.Id;
        }

        public async Task<UpdateUserDto> Get(string Id)
        {
            var user = await _db.Users.SingleOrDefaultAsync(x => x.Id == Id && !x.IsDelete);
            if (user == null)
            {
                throw new EntityNotFoundExecption();
            }
            var userDto = _mapper.Map<UpdateUserDto>(user);
            
            return userDto ;

        }

        public async Task<List<UserViewModel>> GetViewModels()
        {
            var users = await _db.Users.Where(x => !x.IsDelete).ToListAsync();
            return _mapper.Map<List<User> , List<UserViewModel>>(users);

        }

		private string GenratePassword()
		{
			return Guid.NewGuid().ToString().Substring(1, 8);
		}
	}

}
