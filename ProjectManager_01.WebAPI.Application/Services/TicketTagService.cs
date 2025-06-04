using System.Data;
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

    public TicketTagService(
        ITicketTagRepository ticketTagRepository,
        IMapper mapper)
    {
        this.ticketTagRepository = ticketTagRepository;
        this.mapper = mapper;
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto, IDbTransaction dbTransaction)
    {
        var ticketTag = mapper.Map<TicketTag>(ticketTagCreateDto);
        await ticketTagRepository.CreateAsync(ticketTag, dbTransaction);
    }

    public async Task CreateTicketTagAsync(TicketTagCreateDto ticketTagCreateDto)
    {
        var ticketTag = mapper.Map<TicketTag>(ticketTagCreateDto);
        await ticketTagRepository.CreateAsync(ticketTag);
    }

    public async Task DeleteTicketTagAsync(Guid ticketId, Guid tagId)
    {
        await ticketTagRepository.DeleteAsync(ticketId, tagId);
    }

    public async Task<TicketTagDto> GetTicketTagByIdAsync(Guid ticketId, Guid tagId)
    {
        var ticketTag = await ticketTagRepository.GetByIdAsync(ticketId, tagId);

        return mapper.Map<TicketTagDto>(ticketTag);
    }

    public async Task<IEnumerable<TicketTagDto>> GetAllTicketTagsAsync()
    {
        var ticketTags = await ticketTagRepository.GetAllAsync();

        return mapper.Map<IEnumerable<TicketTagDto>>(ticketTags);
    }
}
