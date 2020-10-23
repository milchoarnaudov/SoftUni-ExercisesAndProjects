using SULS.Data;
using SULS.Models;
using System;
using System.Linq;

namespace SULS.Services
{
    public class SubmissionsService : ISubmissionsService
    {
        private readonly ApplicationDbContext context;
        private readonly Random random;

        public SubmissionsService(ApplicationDbContext context, Random random)
        {
            this.context = context;
            this.random = random;
        }

        public void Create(string problemId, string userId, string code)
        {
            var problemMaxPoints = this.context.Problems
                .Where(x => x.Id == problemId)
                .Select(x => x.Points).FirstOrDefault();

            var submission = new Submission
            {
                Code = code,
                ProblemId = problemId,
                UserId = userId,
                AchievedResult = this.random.Next(0, problemMaxPoints + 1),
                CreatedOn = DateTime.UtcNow,
            };

            this.context.Submissions.Add(submission);
            this.context.SaveChanges();
        }

        public void Delete(string id)
        {
            var submission = this.context.Submissions.Find(id);
            this.context.Submissions.Remove(submission);
            this.context.SaveChanges();
        }
    }
}