using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitsService
    {
        void Create(string description, string repoId, string userId);

        IEnumerable<CommitsViewModel> GetAll(string ownerId);

        public bool Delete(string id);
    }
}
