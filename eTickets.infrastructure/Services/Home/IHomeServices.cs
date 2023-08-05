using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Home
{
    public interface IHomeServices
	{

		Task<List<movieViewModel>> MoviesList();
		Task<movieViewModel> MovieProFile(int Id);
		Task<int> buying(int Id, string UserId);
		Task<List<MovieOrderViewModel>> OrderList(string userId);
		Task<int> CreateCustomer(CreateCustomerDto dto);
		Task<int> rolos();
		Task<int> Rating(RatingDto dto);
		Task<Response<MovieOrderViewModel>> GetAllForDataTable(Request request);


	}
}
