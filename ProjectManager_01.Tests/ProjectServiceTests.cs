using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Moq;
using ProjectManager_01.Application.Constants;
using ProjectManager_01.Application.Contracts.Repositories;
using ProjectManager_01.Application.Contracts.Services;
using ProjectManager_01.Application.DTOs.Projects;
using ProjectManager_01.Application.Exceptions;
using ProjectManager_01.Application.Services;
using ProjectManager_01.Domain.Models;
using Xunit;

namespace ProjectManager_01.Tests;

public class ProjectServiceTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly Mock<ITicketService> _ticketServiceMock = new();
    private readonly Mock<IDbConnection> _dbConnectionMock = new();
    private readonly Mock<IProjectRoleService> _projectRoleServiceMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly Mock<IProjectUserRoleService> _projectUserRoleServiceMock = new();
    private readonly Mock<ITagService> _tagServiceMock = new();
    private readonly Mock<ILogger<ProjectService>> _loggerMock = new();

    private ProjectService CreateService() =>
        new(
            _projectRepositoryMock.Object,
            _mapperMock.Object,
            _ticketServiceMock.Object,
            _dbConnectionMock.Object,
            _projectRoleServiceMock.Object,
            _httpContextAccessorMock.Object,
            _projectUserRoleServiceMock.Object,
            _tagServiceMock.Object,
            _loggerMock.Object);

    [Fact]
    public async Task CreateProjectAsync_CreateProject_WhenRepositorySucceeds()
    {
        // Arrange
        var projectCreateDto = new ProjectCreateDto("TestProject", "ABC");
        var project = new Project { Name = projectCreateDto.Name, Key = projectCreateDto.Key };

        _mapperMock.Setup(m => m.Map<Project>(projectCreateDto)).Returns(project);
        _projectRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Project>())).ReturnsAsync(true);

        var service = CreateService();

        // Act
        await service.CreateProjectAsync(projectCreateDto);

        // Assert
        _projectRepositoryMock.Verify(r => r.CreateAsync(It.Is<Project>(p => p.Name == projectCreateDto.Name && p.Key == projectCreateDto.Key.ToUpper())), Times.Once);
    }

    [Fact]
    public async Task CreateProjectAsync_ThrowOperationFailedException_WhenRepositoryFails()
    {
        // Arrange
        var projectCreateDto = new ProjectCreateDto("TestProject", "ABC");
        var project = new Project { Name = projectCreateDto.Name, Key = projectCreateDto.Key };

        _mapperMock.Setup(m => m.Map<Project>(projectCreateDto)).Returns(project);
        _projectRepositoryMock.Setup(r => r.CreateAsync(It.IsAny<Project>())).ReturnsAsync(false);

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<OperationFailedException>(() => service.CreateProjectAsync(projectCreateDto));
    }

    [Fact]
    public async Task UpdateProjectAsync_UpdateProject_WhenRepositoryReturnsTrue()
    {
        // Arrange
        var projectCreateDto = new ProjectUpdateDto(Guid.NewGuid(), "UpdatedName", "TPP");
        var project = new Project { Id = projectCreateDto.Id, Name = projectCreateDto.Name, Key = projectCreateDto.Key };

        _mapperMock.Setup(m => m.Map<Project>(projectCreateDto)).Returns(project);
        _projectRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Project>())).ReturnsAsync(true);

        var service = CreateService();

        // Act
        await service.UpdateProjectAsync(projectCreateDto);

        // Assert
        _projectRepositoryMock.Verify(r => r.UpdateAsync(It.Is<Project>(p => p.Id == projectCreateDto.Id && p.Key == projectCreateDto.Key.ToUpper())), Times.Once);
    }

    [Fact]
    public async Task UpdateProjectAsync_ShouldThrowOperationFailedException_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var projectCreateDto = new ProjectUpdateDto(Guid.NewGuid(), "UpdatedName", "UK");
        var project = new Project { Id = projectCreateDto.Id, Name = projectCreateDto.Name, Key = projectCreateDto.Key };

        _mapperMock.Setup(m => m.Map<Project>(projectCreateDto)).Returns(project);
        _projectRepositoryMock.Setup(r => r.UpdateAsync(It.IsAny<Project>())).ReturnsAsync(false);

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<OperationFailedException>(() => service.UpdateProjectAsync(projectCreateDto));
    }

    [Fact]
    public async Task GetProjectByIdAsync_ShouldReturnProjectDto_WhenProjectExists()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        var project = new Project { Id = projectId, Name = "P", Key = "K", IsDeleted = false, CreatedAt = DateTimeOffset.UtcNow };
        var projectDto = new ProjectDto(project.Id, project.Name, project.Key, project.IsDeleted, project.CreatedAt);

        _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync(project);
        _mapperMock.Setup(m => m.Map<ProjectDto>(project)).Returns(projectDto);

        var service = CreateService();

        // Act
        var result = await service.GetProjectByIdAsync(projectId);

        // Assert
        Assert.Equal(projectDto, result);
    }

    [Fact]
    public async Task GetProjectByIdAsync_ShouldThrowNotFoundException_WhenProjectIsNull()
    {
        // Arrange
        var projectId = Guid.NewGuid();
        _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId)).ReturnsAsync((Project)null);

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() => service.GetProjectByIdAsync(projectId));
    }

    [Fact]
    public async Task GetAllProjectsAsync_ShouldReturnMappedProjectDtos()
    {
        // Arrange
        var projects = new List<Project>
    {
        new Project { Id = Guid.NewGuid(), Name = "Project1", Key = "ABC" },
        new Project { Id = Guid.NewGuid(), Name = "Project2", Key = "CDE" }
    };

        var projectDtos = new List<ProjectDto>
    {
        new ProjectDto(projects[0].Id, projects[0].Name, projects[0].Key, false, DateTimeOffset.UtcNow),
        new ProjectDto(projects[1].Id, projects[1].Name, projects[1].Key, false, DateTimeOffset.UtcNow)
    };

        _projectRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(projects);
        _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(projects)).Returns(projectDtos);

        var service = CreateService();

        // Act
        var result = await service.GetAllProjectsAsync();

        // Assert
        Assert.Equal(projectDtos, result);
    }

    [Fact]
    public async Task SoftDeleteProjectAsync_ShouldCallSoftDelete_WhenRepositoryReturnsTrue()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        _projectRepositoryMock.Setup(r => r.SoftDeleteByIdAsync(projectId)).ReturnsAsync(true);

        var service = CreateService();

        // Act
        await service.SoftDeleteProjectAsync(projectId);

        // Assert
        _projectRepositoryMock.Verify(r => r.SoftDeleteByIdAsync(projectId), Times.Once);
    }

    [Fact]
    public async Task SoftDeleteProjectAsync_ShouldThrowOperationFailedException_WhenRepositoryReturnsFalse()
    {
        // Arrange
        var projectId = Guid.NewGuid();

        _projectRepositoryMock.Setup(r => r.SoftDeleteByIdAsync(projectId)).ReturnsAsync(false);

        var service = CreateService();

        // Act & Assert
        await Assert.ThrowsAsync<OperationFailedException>(() => service.SoftDeleteProjectAsync(projectId));
    }

    [Fact]
    public async Task GetUserProjectsAsync_ShouldReturnUserProjectsBasedOnClaims()
    {
        // Arrange
        var projectId1 = Guid.NewGuid();
        var projectId2 = Guid.NewGuid();

        var claims = new List<Claim>
    {
        new Claim("ProjectPermission", $"{projectId1}:ReadProject"),
        new Claim("ProjectPermission", $"{projectId2}:ReadProject"),
        new Claim("ProjectPermission", $"{projectId1}:WriteProject"),
        new Claim("OtherClaim", "value")
    };

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(c => c.User.Claims).Returns(claims);

        _httpContextAccessorMock.Setup(a => a.HttpContext).Returns(httpContextMock.Object);

        var projects = new[]
        {
        new Project { Id = projectId1, Name = "P1", Key = "K1" },
        new Project { Id = projectId2, Name = "P2", Key = "K2" }
    };

        _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId1)).ReturnsAsync(projects[0]);
        _projectRepositoryMock.Setup(r => r.GetByIdAsync(projectId2)).ReturnsAsync(projects[1]);

        var projectDtos = projects.Select(p => new ProjectDto(p.Id, p.Name, p.Key, false, DateTimeOffset.UtcNow)).ToList();
        _mapperMock.Setup(m => m.Map<IEnumerable<ProjectDto>>(It.IsAny<IEnumerable<Project>>())).Returns(projectDtos);

        var service = CreateService();

        // Act
        var result = await service.GetUserProjectsAsync();

        // Assert
        Assert.Equal(projectDtos.Count, result.Count());
    }

}