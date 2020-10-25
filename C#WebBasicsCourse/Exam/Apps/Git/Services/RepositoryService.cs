using Git.Data;
using Git.ViewModels.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class RepositoryService : IRepositoryService
    {
        private readonly ApplicationDbContext dbContext;

        public RepositoryService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(string name, string repositoryType, string userId)
        {
            var repository = new Repository
            {
                Name = name, 
                IsPublic = repositoryType.ToLower() == "public",
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow
            };

            this.dbContext.Repositories.Add(repository);
            this.dbContext.SaveChanges();
        }

        public IEnumerable<RepositoryViewModel> GetAll()
        {
            return this.dbContext.Repositories.Select(x => new RepositoryViewModel
            {
                Id = x.Id,
                CommitsCount = x.Commits.Count(),
                CreatedOn = x.CreatedOn,
                OwnerName = x.Owner.Username,
                RepositoryName = x.Name
            }).AsEnumerable();
        }

        public string GetNameById(string id)
        {
            return this.dbContext.Repositories.Find(id)?.Name;
        }
    }
}
