using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoryService repositoryService;

        public RepositoriesController(IRepositoryService repositoryService)
        {
            this.repositoryService = repositoryService;
        }

        public HttpResponse All()
        {
            var allRepositories = repositoryService.GetAll().ToList();
            return this.View(allRepositories);
        }

        public HttpResponse Create()
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(string name, string repositoryType)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (name.Length < 3 || name.Length > 10 || string.IsNullOrEmpty(name))
            {
                return this.Error("Repository name length must be between 3 and 10 characters");
            }

            if (repositoryType.ToLower() != "public" && repositoryType.ToLower() != "private")
            {
                return this.Error("Invalid repository type");
            }

            this.repositoryService.Create(name, repositoryType, this.GetUserId());
            return this.Redirect("/Repositories/All");
        }
    }
}
