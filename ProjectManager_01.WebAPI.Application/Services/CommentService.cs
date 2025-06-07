using System.ComponentModel.Design;
using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Auth;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Comments;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IProjectAccessValidator _projectAccessValidator;
    private readonly ILogger<CommentService> _logger;
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        IProjectAccessValidator projectAccessValidator,
        ILogger<CommentService> logger,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _projectAccessValidator = projectAccessValidator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDto commentCreateDto, Guid projectId)
    {
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentCreateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentCreateDto);
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTimeOffset.UtcNow;

        var commentId = await _commentRepository.CreateAsync(comment);

        if (commentId == Guid.Empty)
        {
            _logger.LogError("Failed to create comment in {TicketId} by {UserId}", comment.TicketId, comment.UserId);
            throw new OperationFailedException("Failed to create comment.");
        }
        _logger.LogInformation("Comment created with ID: {CommentId}", commentId);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId, Guid projectId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        var comments = await _commentRepository.GetAllAsync();

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto, Guid projectId)
    {
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentUpdateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentUpdateDto);
        await _commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid projectId)
    {
        var comment = await _commentRepository.GetByIdAsync(commentId);
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);

        await _commentRepository.DeleteByIdAsync(commentId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        await _commentRepository.DeleteAllByUserIdAsync(userId, transaction);
    }

    public async Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        await _commentRepository.DeleteAllByTicketIdAsync(ticketId, transaction);
    }

    public async Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId)
    {
        var comments = await _commentRepository.GetAllByTicketIdAsync(ticketId);
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}
