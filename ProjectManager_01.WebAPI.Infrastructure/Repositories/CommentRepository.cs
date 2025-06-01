using System.Data;
using Dapper;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Infrastructure.Repositories;
internal class CommentRepository : ICommentRepository
{
    private readonly IDbConnection dbConnection;

    public CommentRepository(IDbConnection dbConnection)
    {
        this.dbConnection = dbConnection;
    }

    public async Task<List<Comment>> GetByTicketIdAsync(Guid ticketId)
    {
        var sql = @"SELECT c.Id, c.TicketId, c.UserId, c.Content, c.CreatedAt,
                    u.Id, u.UserName, u.Email, u.IsDeleted, u.CreatedAt
                    FROM Comments c
                    JOIN Users u ON c.UserId = u.Id
                    WHERE c.TicketId = @TicketId";
        var comments = await dbConnection.QueryAsync<Comment, User, Comment>(sql, (comment, user) =>
        {
            comment.User = user;
            return comment;
        },
        splitOn: "UserId");

        return comments.ToList();
    }

    public async Task<List<Comment>> GetByUserAndProjectIdAsync(Guid userId, Guid projectId)
    {
        var sql = @"SELECT c.*, t.Id, t.TicketNumber, t.Title, p.Id, p.Key
					FROM Comments c
					JOIN Tickets t ON c.TicketId = t.Id
					JOIN Projects p ON t.ProjectId = p.Id
					WHERE c.UserId = @UserId AND p.Id = @ProjectId";
        var comments = await dbConnection.QueryAsync<Comment, Ticket, Project, Comment>(sql, (comment, ticket, project) =>
        {
            ticket.Project = project;
            comment.Ticket = ticket;
            return comment;
        },
        new { UserId = userId, ProjectId = projectId },
        splitOn: "Id,Id"
        );


        return comments.ToList();
    }

    // ============================= CRUD =============================
    public async Task<Guid> CreateAsync(Comment comment)
    {
        var sql = @"INSERT INTO Comments (Id, TicketId, UserId, Content, CreatedAt)
                VALUES (@Id, @TicketId, @UserId, @Content, @CreatedAt)";
        comment.Id = Guid.NewGuid();
        comment.CreatedAt = DateTimeOffset.UtcNow;
        var result = await dbConnection.ExecuteAsync(sql, comment);

        if (result > 0)
            return comment.Id;
        else
            throw new Exception("Insert failed");
    }

    public async Task<Comment> GetByIdAsync(Guid id)
    {
        var sql = @"SELECT * FROM Comments WHERE Id = @Id";
        var result = await dbConnection.QueryFirstAsync<Comment>(sql, new { Id = id });

        return result;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        var sql = @"SELECT * FROM Comments";
        var result = await dbConnection.QueryAsync<Comment>(sql);

        return result.ToList();
    }

    public async Task<bool> UpdateAsync(Comment comment)
    {
        var sql = @"UPDATE Comments
                SET TicketId = @TicketId,
                    UserId = @UserId,
                    Content = @Content
                WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, comment);

        return result > 0;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var sql = "DELETE FROM Comments WHERE Id = @Id";
        var result = await dbConnection.ExecuteAsync(sql, new { Id = id });

        return result > 0;
    }
}
