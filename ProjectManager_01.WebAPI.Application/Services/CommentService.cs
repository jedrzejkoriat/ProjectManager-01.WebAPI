using System.ComponentModel.Design;
using System.Data;
using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Comments;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly ITicketService _ticketService;
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        ITicketService ticketService,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _ticketService = ticketService;
        _mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDto commentCreateDto, Guid projectId)
    {
        await ValidateProjectIdAsync(commentCreateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentCreateDto);
        await _commentRepository.CreateAsync(comment);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId, Guid projectId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        await ValidateProjectIdAsync(comment.TicketId, projectId);

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto, Guid projectId)
    {
        await ValidateProjectIdAsync(commentUpdateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentUpdateDto);
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid projectId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        await ValidateProjectIdAsync(comment.TicketId, projectId);

        await _commentRepository.DeleteAsync(commentId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await _commentRepository.DeleteByUserIdAsync(userId, transaction);
    }

    public async Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        await _commentRepository.DeleteByTicketIdAsync(ticketId, transaction);
    }

    public async Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId)
    {
        var comments = await _commentRepository.GetByTicketIdAsync(ticketId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    // This method validates if the comment belongs to the projectId provided through the route
    private async Task ValidateProjectIdAsync(Guid ticketId, Guid projectId)
    {
        var ticket = await _ticketService.GetTicketByIdAsync(ticketId);

        if (ticket == null || ticket.Project.Id != projectId)
            throw new Exception("Comment does not belong to the specified project.");
    }
}
