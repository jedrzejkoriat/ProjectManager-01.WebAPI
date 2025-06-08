using System.ComponentModel.Design;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
    private readonly IUserAccessValidator _userAccessValidator;
    private readonly ILogger<CommentService> _logger;
    private readonly IMapper _mapper;

    public CommentService(
        ICommentRepository commentRepository,
        IProjectAccessValidator projectAccessValidator,
        IUserAccessValidator userAccessValidator,
        ILogger<CommentService> logger,
        IMapper mapper)
    {
        _commentRepository = commentRepository;
        _projectAccessValidator = projectAccessValidator;
        _userAccessValidator = userAccessValidator;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDto commentCreateDto, Guid projectId)
    {
        // Get userId from token
        var userId = _userAccessValidator.GetAuthenticatedUser();

        _logger.LogInformation("Creating Comment called. Ticket: {TicketId}, User: {UserId}",commentCreateDto.TicketId, userId);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentCreateDto.TicketId, projectId);

        var comment = _mapper.Map<Comment>(commentCreateDto);
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTimeOffset.UtcNow;
        comment.UserId = userId;

        // Check if operation is successful
        if (!await _commentRepository.CreateAsync(comment))
        {
            _logger.LogError("Creating Comment failed. Ticket: {TicketId}, User: {UserId}", comment.TicketId, comment.UserId);
            throw new OperationFailedException("Create comment failed.");
        }

        _logger.LogInformation("Creating Comment {CommentId} successfull. Ticket: {TicketId}, User: {UserId}", comment.Id, comment.TicketId, comment.UserId);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId, Guid projectId)
    {
        _logger.LogInformation("Getting Comment called. Comment: {CommentId}", commentId);

        var comment = await _commentRepository.GetByIdAsync(commentId);

        // Check if operation is successful
        if (comment == null)
        {
            _logger.LogError("Getting Comment failed. Comment: {CommentId}", commentId);
            throw new NotFoundException($"Comment not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);

        _logger.LogInformation("Getting Comment succesfull. Comment: {CommentId}", commentId);
        return _mapper.Map<CommentDto>(comment);
    }

    public async Task<IEnumerable<CommentDto>> GetAllCommentsAsync()
    {
        _logger.LogWarning("Getting Comments called.");

        var comments = await _commentRepository.GetAllAsync();

        _logger.LogInformation("Getting Comments ({Count}) successfull.", comments.Count());
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto, Guid projectId)
    {
        _logger.LogInformation("Updating Comment called. Comment: {CommentId}", commentUpdateDto.Id);

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(commentUpdateDto.TicketId, projectId);

        // Validate if comment owner matches with requesting user
        var userId = (await _commentRepository.GetByIdAsync(commentUpdateDto.Id)).UserId;
        _userAccessValidator.ValidateUserIdAsync(userId);

        var comment = _mapper.Map<Comment>(commentUpdateDto);

        // Check if operation is successful
        if (!await _commentRepository.UpdateAsync(comment))
        {
            _logger.LogError("Udating Comment failed. Comment: {CommentId}", commentUpdateDto.Id);
            throw new OperationFailedException($"Updating Comment failed.");
        }

        _logger.LogInformation("Updating Comment succesfull. Comment: {ComentId}", commentUpdateDto.Id);
    }

    public async Task DeleteCommentAsync(Guid commentId, Guid projectId)
    {
        _logger.LogInformation("Deleting Comment called. Comment: {CommentId}", commentId);

        var comment = await _commentRepository.GetByIdAsync(commentId);

        // Check if comment exsists
        if (comment == null)
        {
            _logger.LogError("Deleting Comment failed. Comment: {CommentId}", commentId);
            throw new NotFoundException("Comment not found.");
        }

        // Validate if project access is allowed
        await _projectAccessValidator.ValidateTicketProjectIdAsync(comment.TicketId, projectId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteByIdAsync(commentId))
        {
            _logger.LogError("Deleting Comment failed. Comment: {CommentId}", commentId);
            throw new OperationFailedException("Deleting Comment failed.");
        }

        _logger.LogInformation("Deleting Comment succesfull. Comment: {CommentId}", commentId);
    }

    public async Task DeleteByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting Comments by userId called. User: {UserId}", userId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteAllByUserIdAsync(userId, transaction))
        {
            _logger.LogWarning("No Comments found related to User: {UserId}", userId);
            return;
        }

        _logger.LogInformation("Deleting Comments by userId successfull. User: {UserId}", userId);
    }

    public async Task DeleteByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        _logger.LogInformation("Deleting Comments by ticketId called. Ticket: {TicketId}", ticketId);

        // Check if operation is successful
        if (!await _commentRepository.DeleteAllByTicketIdAsync(ticketId, transaction))
        {
            _logger.LogWarning("No comments related to ticket found. Ticket: {TicketId}", ticketId);
            return;
        }

        _logger.LogInformation("Deleting Comments by ticketId successfull. Ticket: {TicketId}", ticketId);
    }

    public async Task<IEnumerable<CommentDto>> GetByTicketIdAsync(Guid ticketId)
    {
        _logger.LogInformation("Getting Comments by ticketId called. Ticket: {TicketId}", ticketId);

        var comments = await _commentRepository.GetAllByTicketIdAsync(ticketId);

        _logger.LogInformation("Getting Comments ({Count}) successfull.", comments.Count());
        return _mapper.Map<IEnumerable<CommentDto>>(comments);
    }
}
