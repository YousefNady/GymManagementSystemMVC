using RouteGymManagementBLL.ViewModels.MemberViewModels;
using RouteGymManagementBLL.ViewModels.TrainerViewModels;
using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel? GetTrainerViewDetails(int TrainerId);


        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainerDetails(int Id, TrainerToUpdateViewModel UpdatedTrainer);
        bool RemoveTrainer(int TrainerId);


    }
}
