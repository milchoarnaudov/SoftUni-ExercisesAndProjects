using SULS.Data;
using SULS.Models;
using SULS.ViewModels.Problem;
using SULS.ViewModels.Problems;
using SULS.ViewModels.Submissions;
using System.Collections.Generic;
using System.Linq;

namespace SULS.Services
{
    public class ProblemsService : IProblemsService
    {
        private readonly ApplicationDbContext context;

        public ProblemsService(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Create(string name, int points)
        {
            var problem = new Problem { Name = name, Points = points };
            this.context.Problems.Add(problem);
            this.context.SaveChanges();
        }

        public ICollection<ProblemViewModel> GetAll()
        {
            return this.context.Problems.Select(x => new ProblemViewModel { Id = x.Id, Name = x.Name, Count = x.Submissions.Count, }).ToList();
        }

        public string GetNameById(string id)
        {
            return this.context.Problems
                .Where(x => x.Id == id)
                .Select(x => x.Name)
                .FirstOrDefault();
        }

        public ProblemWithSubmissionsViewModel GetById(string id)
        {
            return this.context.Problems.Where(x => x.Id == id).Select(x => new ProblemWithSubmissionsViewModel
            {
                Name = x.Name,
                Submissions = x.Submissions.Select(y => new SubmissionsViewModel
                {
                    Username = y.User.Username,
                    SubmissionId = y.Id,
                    CreatedOn = y.CreatedOn,
                    AchievedResult = y.AchievedResult,
                    MaxPoints = x.Points
                })
            }).FirstOrDefault();
        }
    }
}
