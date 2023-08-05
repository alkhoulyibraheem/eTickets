using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Director
{
    public interface IDirectorServices
    {
        Task<Response<DirectorViewModel>> GetAllForDataTable(Request request);
        Task<int> Create(CreateDirectorDto dto);
        Task<int> Update(UpdateDirectorDto dto);
        Task<int> Delete(int Id);
        Task<UpdateDirectorDto> Get(int Id);


    }
}
