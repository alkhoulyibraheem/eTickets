using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Movie
{
    public interface IMovieServices
	{
        Task<int> Create(CreateMovieDto dto);
        Task<int> Update(UpdateMovieDto dto);
        Task<int> Delete(int Id);
        Task<UpdateMovieDto> Get(int Id);
        Task<Response<movieViewModel>> GetAllForDataTable(Request request);
        Task<List<movieViewModel>> GetViewModels();


    }
}
