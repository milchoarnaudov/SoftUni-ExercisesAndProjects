using System;

namespace Git.ViewModels.Commits
{
    public class CommitsViewModel
    {
        public string Id { get; set; }

        public string RepositoryName { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsRepoPublic { get; set; }

        public string OwnerId { get; set; }
    }
}
