using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketTagService : ITicketTagService
{
    private readonly ITicketTagRepository _ticketTagRepository;
    private readonly IMapper _mapper;

    public TicketTagService(
        ITicketTagRepository ticketTagRepository,
        IMapper mapper)
    {
        _ticketTagRepository = ticketTagRepository;
        _mapper = mapper;
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, IDbTransaction dbTransaction)
    {
        var ticketTag = _mapper.Map<TicketTag>(ticketTagCreateDto);
        await _ticketTagRepository.CreateAsync(ticketTag, dbTransaction);
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto)
    {
        var ticketTag = _mapper.Map<TicketTag>(ticketTagCreateDto);
        await _ticketTagRepository.CreateAsync(ticketTag);
    }

    public async Task DeleteTicketTagAsync(Guid ticketId, Guid tagId)
    {
        await _ticketTagRepository.DeleteAsync(ticketId, tagId);
    }

    public async Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId)
    {
        var ticketTag = await _ticketTagRepository.GetByIdAsync(ticketId, tagId);

        return _mapper.Map<TicketTagDto>(ticketTag);
    }

    public async Task<IEnumerable<TicketTagDto>> GetAllTicketTagsAsync()
    {
        var ticketTags = await _ticketTagRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<TicketTagDto>>(ticketTags);
    }
}
