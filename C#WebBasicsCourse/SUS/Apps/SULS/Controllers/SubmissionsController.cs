using SULS.Services;
using SULS.ViewModels.Submissions;
using SUS.HTTP;
using SUS.MvcFramework;

namespace SULS.Controllers
{
    public class SubmissionsController : Controller
    {
        private readonly ISubmissionsService submissionsService;
        private readonly IProblemsService problemsService;

        public SubmissionsController(ISubmissionsService submissionsService, IProblemsService problemsService)
        {
            this.submissionsService = submissionsService;
            this.problemsService = problemsService;
        }

        public HttpResponse Create(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateSubmissionViewModel
            {
                ProblemId = id,
                Name = this.problemsService.GetNameById(id),
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(string problemId, string code)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (string.IsNullOrEmpty(code) || code.Length < 30 || code.Length > 800)
            {
                return this.Error("Code should be between 30 and 800 characters long.");
            }

            var userId = this.GetUserId();
            this.submissionsService.Create(problemId, userId, code);
            return this.Redirect("/");
        }

        public HttpResponse Delete(string id)
        {
            if (!this.IsUserSignedIn())
            {
                return this.Redirect("/Users/Login");
            }

            this.submissionsService.Delete(id);
            return this.Redirect("/");
        }
    }
}
