using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectManager_01.WebAPI.Application.DTOs;

namespace ProjectManager_01.WebAPI.Application.Contracts;
public interface IProjectsRepository
{
    ProjectDTO GetProjectDTO(ProjectDTO projectDTO);
}
