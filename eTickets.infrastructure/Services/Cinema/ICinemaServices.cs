using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Cinema
{
    public interface ICinemaServices
    {
        Task<int> Create(CreateCinemaDto dto);
        Task<int> Update(UpdateCinemaDto dto);
        Task<int> Delete(int Id);
        Task<UpdateCinemaDto> Get(int Id);
        Task<Response<cinemaViewModel>> GetAllForDataTable(Request request);

	}
}
