using System.ComponentModel.Design;
using System.Data;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
        _logger.LogInformation("Creating comment called. Ticket: {TicketId}, User: {UserId}",commentCreateDto.TicketId, commentCreateDto.UserId);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentCreateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentCreateDto);
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTimeOffset.UtcNow;

        // Check if operation is successful
        if (!await _commentRepository.CreateAsync(comment))
        {
            _logger.LogError("Creating comment failed. Ticket: {TicketId}, User: {UserId}", comment.TicketId, comment.UserId);
            throw new OperationFailedException("Create comment failed.");
        }

        _logger.LogInformation("Creating comment {CommentId} successfull. Ticket: {TicketId}, User: {UserId}", comment.Id, comment.TicketId, comment.UserId);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId, Guid projectId)
    {
        _logger.LogInformation("Getting comment called. Comment: {CommentId}", commentId);

        var comment = await _commentRepository.GetByIdAsync(commentId);

        // Check if operation is successful
        if (comment == null)
        {
            _logger.LogError("Getting comment failed. Comment: {CommentId}", commentId);
            throw new NotFoundException($"Comment not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);
        _logger.LogInformation("Getting comment succesfull. Comment: {CommentId}", commentId);

        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        _logger.LogWarning("Getting comments called.");

        var comments = await _commentRepository.GetAllAsync();
        _logger.LogInformation("Getting comments ({Count}) successfull.", comments.Count());

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto, Guid projectId)
    {
        _logger.LogInformation("Updating comment called. Comment: {CommentId}", commentUpdateDto.Id);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentUpdateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentUpdateDto);

        // Check if operation is successful
        if (!await _commentRepository.UpdateAsync(comment))
        {
            _logger.LogError("Udating comment failed. Comment: {CommentId}", commentUpdateDto.Id);
            throw new OperationFailedException($"Updating comment failed.");
        }

        _logger.LogInformation("Updating comment succesfull. Comment: {ComentId}", commentUpdateDto.Id);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid projectId)
    {
        _logger.LogInformation("Deleting comment called. Comment: {CommentId}", commentId);

        var comment = await _commentRepository.GetByIdAsync(commentId);

        // Check if comment exsists
        if (comment == null)
        {
            _logger.LogError("Deleting comment failed. Comment: {CommentId}", commentId);
            throw new NotFoundException("Comment not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteByIdAsync(commentId))
        {
            _logger.LogError("Deleting comment failed. Comment: {CommentId}", commentId);
            throw new OperationFailedException("Deleting comment failed.");
        }

        _logger.LogInformation("Deleting comment succesfull. Comment: {CommentId}", commentId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting comments by userId called. User: {UserId}", userId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteAllByUserIdAsync(userId, transaction))
        {
            _logger.LogError("Deleting comments by userId failed. User: {UserId}", userId);
            throw new OperationFailedException("Deleting comments failed.");
        }

        _logger.LogInformation("Deleting comments by userId successfull. User: {UserId}", userId);
    }

    public async Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting comments by ticketId called. Ticket: {TicketId}", ticketId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteAllByTicketIdAsync(ticketId, transaction))
        {
            _logger.LogError("Deleting comments by ticketId failed. Ticket: {TicketId}", ticketId);
            throw new OperationFailedException("Deleting comments failed.");
        }

        _logger.LogInformation("Deleting comments by ticketId successfull. Ticket: {TicketId}", ticketId);
    }

    public async Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId)
    {
        _logger.LogInformation("Getting comments by ticketId called. Ticket: {TicketId}", ticketId);

        var comments = await _commentRepository.GetAllByTicketIdAsync(ticketId);
        _logger.LogInformation("Getting comments ({Count}) successfull.", comments.Count());

        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}
