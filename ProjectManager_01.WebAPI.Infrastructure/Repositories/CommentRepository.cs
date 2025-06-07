using System.Data;
using Dapper;
using Microsoft.Extensions.Logging;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;

internal class CommentRepository : ICommentRepository
{
    private readonly IDbConnection _dbConnection;

    public CommentRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    // ============================== QUERIES =============================
    public async Task<IEnumerable<Comment>> GetAllByTicketIdAsync(Guid ticketId)
    {
        var sql = @"SELECT c.*,
                    u.Id AS UserId, u.UserName, u.Email, u.IsDeleted, u.CreatedAt
                    FROM Comments c
                    JOIN Users u ON c.UserId = u.Id
                    WHERE c.TicketId = @TicketId";
        var comments = await _dbConnection.QueryAsync<Comment, User, Comment>(sql, (comment, user) =>
        {
            comment.User = user;
            return comment;
        },
        new { TicketId = ticketId },
        splitOn: "UserId");

        return comments.ToList();
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT c.*, 
                    u.Id AS UserId, u.UserName, u.Email, u.IsDeleted, u.CreatedAt
                    FROM Comments c
                    JOIN Users u ON c.UserId = u.Id
                    WHERE c.Id = @Id";
        var result = await _dbConnection.QueryAsync<Comment, User, Comment>(sql, (comment, user) =>
        {
            comment.User = user;
            return comment;
        },
        new { Id = id },
        splitOn: "UserId");

        return result.First();
    }

    public async Task<IEnumerable<Comment>> GetAllAsync()
    {
        var sql = @"SELECT c.*, 
                    u.Id AS UserId, u.UserName, u.Email, u.IsDeleted, u.CreatedAt
                    FROM Comments c
                    JOIN Users u ON c.UserId = u.Id";
        var comments = await _dbConnection.QueryAsync<Comment, User, Comment>(sql, (comment, user) =>
        {
            comment.User = user;
            return comment;
        },
        splitOn: "UserId");

        return comments.ToList();
    }

    // ============================== COMMANDS ============================
    public async Task<bool> DeleteAllByTicketIdAsync(Guid ticketId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Comments
                    WHERE TicketId = @TicketId";
        var result = await _dbConnection.ExecuteAsync(sql, new { TicketId = ticketId }, transaction);

        return result > 0;
    }

    public async Task<bool> DeleteAllByUserIdAsync(Guid userId, IDbTransaction transaction)
    {
        var sql = @"DELETE FROM Comments
                    WHERE UserId = @UserId";
        var result = await _dbConnection.ExecuteAsync(sql, new { UserId = userId }, transaction);

        return result > 0;
    }

    public async Task<Guid> CreateAsync(Comment comment)
    {
        var sql = @"INSERT INTO Comments (Id, TicketId, UserId, Content, CreatedAt)
                    VALUES (@Id, @TicketId, @UserId, @Content, @CreatedAt)";
        var result = await _dbConnection.ExecuteAsync(sql, comment);

        if (result > 0)
            return comment.Id;
        else
            return Guid.Empty;
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        var sql = @"UPDATE Comments
                    SET Content = @Content
                    WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, comment);

        return result > 0;
    }

    public async Task<bool> DeleteByIdAsync(Guid id)
    {
        var sql = "DELETE FROM Comments WHERE Id = @Id";
        var result = await _dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
