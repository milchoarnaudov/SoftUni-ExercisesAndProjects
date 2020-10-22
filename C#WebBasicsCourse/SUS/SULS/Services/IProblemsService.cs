using SULS.ViewModels.Problem;
using SULS.ViewModels.Problems;
using System.Collections.Generic;

namespace SULS.Services
{
    public interface IProblemsService
    {
        ICollection<ProblemViewModel> GetAll();

        void Create(string name, int points);

        string GetNameById(string id);

        ProblemWithSubmissionsViewModel GetById(string id);
    }
}
