using eTickets.core.Dto;
using eTickets.core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static eTickets.core.DataTable.DataTable;

namespace eTickets.infrastructure.Services.Actor
{
    public interface IActorServices
	{
        Task<Response<ActorViewModel>> GetAllForDataTable(Request request);
        Task<int> Create(CreateActorDto dto);
        Task<int> Update(UpdateActorDto dto);
        Task<int> Delete(int Id);
        Task<UpdateActorDto> Get(int Id);


    }
}
