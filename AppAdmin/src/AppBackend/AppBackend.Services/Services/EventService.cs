using AppBackend.Application.Common.Mappings;
using AppBackend.Application.Common.Response;
using AppBackend.Application.Interface;
using AppBackend.Application.ModelsDTO;
using AppBackend.Data.Context;
using AppBackend.Data.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AppBackend.Application.Services
{
	public class EventService : IEventService
	{
		private readonly DataContext _dataContext;
		private readonly IMapper _mapper;
		public EventService(DataContext dataContext, IMapper mapper)
		{
			_dataContext = dataContext;
			_mapper = mapper;
		}


		public async Task<Response<EventDTO>> Get(string id)
		{
			var result = await _dataContext.Events.FirstOrDefaultAsync(o => o.Id == id);
			return new Response<EventDTO>() { IsSuccess = true, Status = 200, Value = _mapper.Map<EventDTO>(result) };
		}

		public async Task<Response<PaginatedList<EventDTO>>> GetAllPage(int pageSize, int pageNum, string? searchName)
		{
			var query = _dataContext.Events.AsNoTracking().Where(x => x.Title.Contains(searchName) || searchName.IsNullOrEmpty());
			var objs = await query.ProjectTo<EventDTO>(_mapper.ConfigurationProvider).PaginatedListAsync(pageNum, pageSize);
			return new Response<PaginatedList<EventDTO>> { IsSuccess = true, Status = 200, Value = objs };
		}

		public async Task<Response<string>> Post(EventDTO eventDTO)
		{
			try
			{
				eventDTO.Id = Guid.NewGuid().ToString();
				var eventData = _mapper.Map<Event>(eventDTO);
				await _dataContext.Events.AddAsync(eventData);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = eventDTO.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Put(EventDTO eventDTO)
		{
			try
			{
				var eventdata = await _dataContext.Events.FirstOrDefaultAsync(x => x.Id == eventDTO.Id);
				if (eventdata == null) return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found event!" };
				eventdata.Title = eventDTO.Title;
				eventdata.DateStart = eventDTO.DateStart;
				eventdata.Expense = eventDTO.Expense;
				eventdata.Description = eventDTO.Description;
				eventdata.EmployeeID = eventDTO.EmployeeID;

				_dataContext.Events.Update(eventdata);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = eventdata.Id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

		public async Task<Response<string>> Remove(string id)
		{
			try
			{
				var eventData = await _dataContext.Events.FirstOrDefaultAsync(x => x.Id == id);
				if (eventData == null)
					return new Response<string> { IsSuccess = false, Status = 404, Value = "Not found event " };
				_dataContext.Events.Remove(eventData);
				await _dataContext.SaveChangesAsync();
				return new Response<string> { IsSuccess = true, Status = 200, Value = id };
			}
			catch (Exception ex)
			{
				return new Response<string> { IsSuccess = false, Status = 404, Message = ex.Message };
			}
		}

	}
}
