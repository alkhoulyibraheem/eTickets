using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Category
{
    public interface ICategoryServices
    {
        //Task<ResponseDto> GetAll(Pagination pagination, Query query);
        Task<int> Create(CreateCategoryDto dto);
        Task<int> Update(UpdateCategoryDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCategoryDto> Get(int Id);
        Task<Response<categoryViewModel>> GetAllForDataTable(Request request);

	}
}
