using AutoMapper;
using RouteGymManagementBLL.BusinessServices.Interfaces;
using RouteGymManagementBLL.View_Models.TrainerVMs;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.UnitOfWorkPattern.Interfaces;

namespace RouteGymManagementBLL.BusinessServices.Implementation
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public TrainerService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateTrainer(CreateTrainerViewModel createTrainer)
        {
            var repo = unitOfWork.GetRepository<Trainer>();

            try
            {
                if (IsEmailExists(createTrainer.Email) || IsPhoneExists(createTrainer.Phone))
                {
                    return false;
                }

                var trainer = mapper.Map<Trainer>(createTrainer);
                repo.Add(trainer);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public IEnumerable<TrainerViewModel> GetAllTrainers()
        {
            var trainers = unitOfWork.GetRepository<Trainer>().GetAll();
            if (trainers is null || !trainers.Any())
            {
                return [];
            }

            return mapper.Map<IEnumerable<TrainerViewModel>>(trainers);
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null)
            {
                return null;
            }

            return mapper.Map<TrainerToUpdateViewModel>(trainer);
        }

        public TrainerViewModel? GetTrainerViewDetails(int TrainerId)
        {
            var trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null)
            {
                return null;
            }

            return mapper.Map<TrainerViewModel>(trainer);
        }

        public bool RemoveTrainer(int TrainerId)
        {
            var repo = unitOfWork.GetRepository<Trainer>();
            var TrainerToRemove = repo.GetById(TrainerId);

            if (TrainerToRemove is null || HasActiveSessions(TrainerId))
            {
                return false;
            }

            repo.Delete(TrainerToRemove);
            return unitOfWork.SaveChanges() > 0;
        }

        public bool UpdateTrainerDetails(int Id, UpdateTrainerViewModel UpdatedTrainer)
        {
            var repo = unitOfWork.GetRepository<Trainer>();
            var trainerToUpdate = repo.GetById(Id);

            if (trainerToUpdate is null || IsEmailExists(UpdatedTrainer.Email) || IsPhoneExists(UpdatedTrainer.Phone))
            {
                return false;
            }

            mapper.Map(UpdatedTrainer, trainerToUpdate);
            trainerToUpdate.UpdatedAt = DateTime.Now;

            repo.Update(trainerToUpdate);
            return unitOfWork.SaveChanges() > 0;
        }

        #region HelperMethods
        private bool IsPhoneExists(string phone)
        {
            return unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone).Any();
        }

        private bool IsEmailExists(string email)
        {
            return unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email).Any();
        }

        private bool HasActiveSessions(int TrainerId)
        {
            var activeSessions = unitOfWork.GetRepository<Session>()
                .GetAll(s => s.TrainerId == TrainerId && s.StartDate > DateTime.Now);
            return activeSessions.Any();
        }
        #endregion
    }
}
