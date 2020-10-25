using Git.Services;
using Git.ViewModels.Commits;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitsService commitsService;
        private readonly IRepositoryService repositoryService;

        public CommitsController(ICommitsService commitsService, IRepositoryService repositoryService)
        {
            this.commitsService = commitsService;
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var allCommits = commitsService.GetAll(this.GetUserId()).ToList();
            return this.View(allCommits);
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var repositoryName = this.repositoryService.GetNameById(id);

            if (repositoryService == null)
            {
                return this.Error("Repository not found");
            }

            var createCommitview = new CreateCommitViewModel 
            {
                Id = id, Name = repositoryName 
            };

            return this.View(createCommitview);
        }

        [HttpPost]
        public HttpResponse Create(CreateCommitInputModel input)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Description.Length < 5 || string.IsNullOrEmpty(input.Description))
            {
                return this.Error("Description length must be longer than 5 ");
            }

            this.commitsService.Create(input.Description, input.Id, this.GetUserId());
            return this.Redirect("/Repositories/All");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (!this.commitsService.Delete(id))
            {
                return this.Error("Unsuccessful deletion. The commit is not found");
            }

            return this.Redirect("/Commits/All");
        }
    }
}
