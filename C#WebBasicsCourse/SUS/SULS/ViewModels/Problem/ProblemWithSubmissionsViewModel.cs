using SULS.ViewModels.Submissions;
using System.Collections.Generic;

namespace SULS.ViewModels.Problem
{
    public class ProblemWithSubmissionsViewModel
    {
        public string Name { get; set; }

        public IEnumerable<SubmissionsViewModel> Submissions { get; set; }
    }
}
