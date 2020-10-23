using System;

namespace SULS.ViewModels.Submissions
{
    public class SubmissionsViewModel
    {
        public string Username { get; set; }

        public string SubmissionId { get; set; }

        public int AchievedResult { get; set; }

        public int MaxPoints { get; set; }

        public int Percentage => (int)Math.Round(this.AchievedResult * 100.0M / this.MaxPoints);

        public DateTime CreatedOn { get; set; }
    }
}
