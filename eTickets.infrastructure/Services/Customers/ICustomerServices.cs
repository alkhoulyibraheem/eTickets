using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Customers
{
    public interface ICustomerServices
	{
        Task<Response<CustomerViewModel>> GetAllForDataTable(Request request);
        Task<int> Create(CreateCustomerDto dto);
        Task<int> Update(UpdateCustomerDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCustomerDto> Get(int Id);


    }
}
