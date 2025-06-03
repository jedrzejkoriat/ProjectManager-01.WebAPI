using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class TicketTagService : ITicketTagService
{

    private readonly ITicketTagRepository ticketTagRepository;
    private readonly IMapper mapper;

    public TicketTagService(ITicketTagRepository ticketTagRepository, IMapper mapper)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.mapper = mapper;
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto)
    {
        TicketTag ticketTag = mapper.Map<TicketTag>(ticketTagCreateDto);
        await ticketTagRepository.CreateAsync(ticketTag);
    }

    public async Task DeleteTicketTagAsync(Guid ticketId, Guid tagId)
    {
        await ticketTagRepository.DeleteAsync(ticketId, tagId);
    }

    public async Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId)
    {
        TicketTag ticketTag = await ticketTagRepository.GetByIdAsync(ticketId, tagId);

        return mapper.Map<TicketTagDto>(ticketTag);
    }

    public async Task<List<TicketTagDto>> GetAllTicketTagsAsync()
    {
        List<TicketTag> ticketTags = await ticketTagRepository.GetAllAsync();

        return mapper.Map<List<TicketTagDto>>(ticketTags);
    }
}
