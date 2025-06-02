using AutoMapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Comments;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository;
    private readonly IMapper mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        this.commentRepository = commentRepository;
        this.mapper = mapper;
    }

    public async Task CreateCommentAsync(CommentCreateDto commentCreateDto)
    {
        Comment comment = mapper.Map<Comment>(commentCreateDto);
        await commentRepository.CreateAsync(comment);
    }

    public async Task<CommentDto> GetCommentAsync(Guid commentId)
    {
        Comment comment = await commentRepository.GetByIdAsync(commentId);

        return mapper.Map<CommentDto>(comment);
    }

    public async Task<List<CommentDto>> GetAllCommentsAsync()
    {
        List<Comment> comments = await commentRepository.GetAllAsync();

        return mapper.Map<List<CommentDto>>(comments);
    }

    public async Task UpdateCommentAsync(CommentUpdateDto commentUpdateDto)
    {
        Comment comment = mapper.Map<Comment>(commentUpdateDto);
        await commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteCommentAsync(Guid commentId)
    {
        await commentRepository.DeleteAsync(commentId);
    }
}
