using AutoMapper;
using ProjectManager_01.Application.DTOs.Comments;
using ProjectManager_01.Application.DTOs.Permissions;
using ProjectManager_01.Application.DTOs.Priorities;
using ProjectManager_01.Application.DTOs.ProjectRolePermissions;
using ProjectManager_01.Application.DTOs.ProjectRoles;
using ProjectManager_01.Application.DTOs.Projects;
using ProjectManager_01.Application.DTOs.ProjectUserRoles;
using ProjectManager_01.Application.DTOs.Roles;
using ProjectManager_01.Application.DTOs.Tags;
using ProjectManager_01.Application.DTOs.TicketRelations;
using ProjectManager_01.Application.DTOs.Tickets;
using ProjectManager_01.Application.DTOs.TicketTags;
using ProjectManager_01.Application.DTOs.UserRoles;
using ProjectManager_01.Application.DTOs.Users;
using ProjectManager_01.Domain.Models;

namespace ProjectManager_01.Application.Configuration;

public sealed class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<CommentCreateDto, Comment>().ReverseMap();
        CreateMap<CommentDto, Comment>().ReverseMap();
        CreateMap<CommentUpdateDto, Comment>().ReverseMap();

        CreateMap<PermissionCreateDto, Permission>().ReverseMap();
        CreateMap<PermissionDto, Permission>().ReverseMap();
        CreateMap<PermissionUpdateDto, Permission>().ReverseMap();

        CreateMap<PriorityCreateDto, Priority>().ReverseMap();
        CreateMap<PriorityDto, Priority>().ReverseMap();
        CreateMap<PriorityUpdateDto, Priority>().ReverseMap();

        CreateMap<ProjectRolePermissionCreateDto, ProjectRolePermission>().ReverseMap();
        CreateMap<ProjectRolePermissionDto, ProjectRolePermission>().ReverseMap();

        CreateMap<ProjectRoleCreateDto, ProjectRole>().ReverseMap();
        CreateMap<ProjectRoleDto, ProjectRole>().ReverseMap();
        CreateMap<ProjectRoleUpdateDto, ProjectRole>().ReverseMap();

        CreateMap<ProjectCreateDto, Project>().ReverseMap();
        CreateMap<ProjectDto, Project>().ReverseMap();
        CreateMap<ProjectUpdateDto, Project>().ReverseMap();

        CreateMap<ProjectUserRoleCreateDto, ProjectUserRole>().ReverseMap();
        CreateMap<ProjectUserRoleDto, ProjectUserRole>().ReverseMap();
        CreateMap<ProjectUserRoleUpdateDto, ProjectUserRole>().ReverseMap();

        CreateMap<RoleCreateDto, Role>().ReverseMap();
        CreateMap<RoleDto, Role>().ReverseMap();
        CreateMap<RoleUpdateDto, Role>().ReverseMap();

        CreateMap<TagCreateDto, Tag>().ReverseMap();
        CreateMap<TagDto, Tag>().ReverseMap();
        CreateMap<TagUpdateDto, Tag>().ReverseMap();

        CreateMap<TicketRelationCreateDto, TicketRelation>().ReverseMap();
        CreateMap<TicketRelationDto, TicketRelation>().ReverseMap();
        CreateMap<TicketRelationUpdateDto, TicketRelation>().ReverseMap();

        CreateMap<TicketCreateDto, Ticket>().ReverseMap();
        CreateMap<Ticket, TicketDto>()
            .ForMember(dest => dest.Assignee, opt => opt.MapFrom(src => src.Assignee));
        CreateMap<TicketOverviewDto, Ticket>().ReverseMap();
        CreateMap<TicketUpdateDto, Ticket>().ReverseMap();

        CreateMap<TicketTagCreateDto, TicketTag>().ReverseMap();
        CreateMap<TicketTagDto, TicketTag>().ReverseMap();

        CreateMap<UserRoleCreateDto, UserRole>().ReverseMap();
        CreateMap<UserRoleDto, UserRole>().ReverseMap();
        CreateMap<UserRoleUpdateDto, UserRole>().ReverseMap();

        CreateMap<UserCreateDto, User>().ReverseMap();
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<UserUpdateDto, User>().ReverseMap();
    }
}