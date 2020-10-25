using Git.ViewModels.Repositories;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IRepositoryService
    {
        IEnumerable<RepositoryViewModel> GetAll();

        void Create(string name, string repositoryType, string userId);

        string GetNameById(string id);
    }
}
