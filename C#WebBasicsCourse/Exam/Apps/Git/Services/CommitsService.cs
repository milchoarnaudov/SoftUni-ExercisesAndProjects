using Git.Data;
using Git.ViewModels.Commits;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitsService : ICommitsService
    {
        private readonly ApplicationDbContext dbContext;

        public CommitsService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string description, string repoId, string creatorId)
        {
            var commit = new Commit
            {
                Description = description,
                RepositoryId = repoId,
                CreatorId = creatorId,
                CreatedOn = DateTime.UtcNow
            };

            this.dbContext.Commits.Add(commit);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<CommitsViewModel> GetAll(string ownerId)
        {
            return this.dbContext.Commits.Select(x => new CommitsViewModel
            {
                Id = x.Id,
                Description = x.Description,
                CreatedOn = x.CreatedOn,
                RepositoryName = x.Repository.Name,
                IsRepoPublic = x.Repository.IsPublic,
                OwnerId = x.CreatorId
            }).Where(c => c.IsRepoPublic && c.OwnerId == ownerId).AsEnumerable();
        }

        public bool Delete(string id)
        {
            var commit = this.dbContext.Commits.Find(id);

            if (commit == null)
            {
                return false;
            }

            this.dbContext.Commits.Remove(commit);
            this.dbContext.SaveChanges();
            return true;
        }
    }
}
