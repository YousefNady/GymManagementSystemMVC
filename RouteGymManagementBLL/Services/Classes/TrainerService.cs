using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.TrainerViewModels;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Classes
{
    internal class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork unitOfWork;

        public TrainerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
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

                var trainer = new Trainer()
                {

                    Name = createTrainer.Name,
                    Email = createTrainer.Email,
                    Phone = createTrainer.Phone,
                    DateOfBirth = createTrainer.DateOfBirth,
                    Gender = createTrainer.Gender,
                    Specialties = createTrainer.Specialties,
                    Address = new Address()
                    {
                        BuildingNumber = createTrainer.BuildingNumber,
                        City = createTrainer.City,
                        Street = createTrainer.Street
                    }

                };
                repo.Add(trainer); // added localy
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

            return trainers.Select(t => new TrainerViewModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    Specialties = t.Specialties.ToString(),
                    Email = t.Email,
                    Phone = t.Phone


                });
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int TrainerId)
        {
            var trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null)
            {
                return null;
            }

            return new TrainerToUpdateViewModel()
            {
                Name = trainer.Name,
                Email = trainer.Email,
                BulidingNumber = trainer.Address.BuildingNumber,
                City = trainer.Address.City,
                Street = trainer.Address.Street,
                Phone = trainer.Phone,
                Specialties = trainer.Specialties.ToString()
            };

        }

        public TrainerViewModel? GetTrainerViewDetails(int TrainerId)
        {
            var trainer = unitOfWork.GetRepository<Trainer>().GetById(TrainerId);
            if (trainer is null)
            {
                return null;
            }

            return new TrainerViewModel
            {
                Email = trainer.Email,
                Id = trainer.Id,
                Name = trainer.Name,
                Specialties = trainer.Specialties.ToString(),
                Phone = trainer.Phone
            };
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

            if (trainerToUpdate is null || IsEmailExists(UpdatedTrainer.Email)|| IsPhoneExists(UpdatedTrainer.Phone)) 
            {
                return false;
            }
            trainerToUpdate.Email = UpdatedTrainer.Email;
            trainerToUpdate.Phone = UpdatedTrainer.Phone;
            trainerToUpdate.Address.BuildingNumber = UpdatedTrainer.BuildingNumber;
            trainerToUpdate.Address.City = UpdatedTrainer.City;
            trainerToUpdate.Address.Street = UpdatedTrainer.Street;
            trainerToUpdate.Specialties = UpdatedTrainer.Specialties;
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
                .GetAll( s => s.TrainerId == TrainerId && s.StartDate > DateTime.Now);
            return activeSessions.Any(); 
        }

        #endregion

    }
}
