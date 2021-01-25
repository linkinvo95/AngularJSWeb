using System;
using BusinessEntities;

namespace Core.Services.Projects
{
    public interface ICreateProjectService
    {
        Project Create(Guid id, string name);
    }
}