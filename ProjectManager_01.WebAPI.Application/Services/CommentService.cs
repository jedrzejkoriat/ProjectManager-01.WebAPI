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
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDto commentCreateDto)
    {
        var comment = _mapper.Map<Comment>(commentCreateDto);
        await _commentRepository.CreateAsync(comment);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto)
    {
        var comment = _mapper.Map<Comment>(commentUpdateDto);
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
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
}
