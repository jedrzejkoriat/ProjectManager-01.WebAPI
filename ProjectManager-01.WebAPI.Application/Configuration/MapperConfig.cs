using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProjectManager_01.WebAPI.Application.DTOs;
using ProjectManager_01.WebAPI.Domain.Models;

namespace ProjectManager_01.WebAPI.Application.Configuration;
internal class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<Comment, CommentDTO>().ReverseMap();
        CreateMap<Permission, PermissionDTO>().ReverseMap();
        CreateMap<Priority, PriorityDTO>().ReverseMap();
        CreateMap<Project, ProjectDTO>().ReverseMap();
        CreateMap<ProjectRole, ProjectRoleDTO>().ReverseMap();
        CreateMap<ProjectRolePermission, ProjectRolePermissionDTO>().ReverseMap();
        CreateMap<ProjectUserRole, ProjectUserRoleDTO>().ReverseMap();
        CreateMap<Role, RoleDTO>().ReverseMap();
        CreateMap<Tag, TagDTO>().ReverseMap();
        CreateMap<Ticket, TicketDTO>().ReverseMap();
        CreateMap<TicketRelation, TicketRelationDTO>().ReverseMap();
        CreateMap<TicketTag, TicketTagDTO>().ReverseMap();
        CreateMap<User, UserDTO>().ReverseMap();
        CreateMap<UserRole, UserRoleDTO>().ReverseMap();
    }
}
