using RouteGymManagementBLL.View_Models.TrainerVMs;

namespace RouteGymManagementBLL.BusinessServices.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel> GetAllTrainers();
        bool CreateTrainer(CreateTrainerViewModel createTrainer);
        TrainerViewModel? GetTrainerViewDetails(int TrainerId);


        TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId);
        bool UpdateTrainerDetails(int Id, UpdateTrainerViewModel UpdatedTrainer);
        bool RemoveTrainer(int TrainerId);


    }
}
