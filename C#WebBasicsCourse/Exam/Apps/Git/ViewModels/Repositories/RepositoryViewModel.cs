using System;

namespace Git.ViewModels.Repositories
{
    public class RepositoryViewModel
    {
        public string Id { get; set; }

        public string RepositoryName { get; set; }

        public string OwnerName { get; set; }

        public DateTime CreatedOn { get; set; }

        public int CommitsCount { get; set; }
    }
}
