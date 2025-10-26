using AutoMapper;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.SessionViewModels;
using RouteGymManagementBLL.ViewModels.TrainerViewModels;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public bool RemoveSession(int sessionId)
        {
            try
            {
                var session = unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (!IsSessionAvailableForRemoving(session!))
                {
                    return false;
                }

                unitOfWork.GetRepository<Session>().Delete(session!);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Update Session Falid: {ex}");
                return false;
            }
        }
        public bool CreateSession(CreateSessionViewModel createdSession)
        {
            try
            {
                // check if trainer exists
                if (!IsTrainerExists(createdSession.TrainerId))
                {
                    return false;
                }
                // check if category exists
                if (!IsCategoryExists(createdSession.CategoryId))
                {
                    return false;
                }
                // check if StartDate Befrore EndDate
                if (!IsDateTimeValid(createdSession.StartDate, createdSession.EndDate))
                {
                    return false;
                }

                if (createdSession.Capacity > 25 || createdSession.Capacity < 0)
                {
                    return false;
                }

                var sessionEntity = mapper.Map<Session>(createdSession);
                unitOfWork.GetRepository<Session>().Add(sessionEntity);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Create Session Faild: {ex}");

                return false;
            }
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessions = unitOfWork.sessionRepository.GetAllSessionWithTrainerAndCategory();
            if (!sessions.Any())
            {
                return [];
            }

            //return sessions.Select(s => new SessionViewModel
            //{
            //    Id = s.Id,
            //    CategoryName = s.SessionCategore.CategoryName, // related Data
            //    Description = s.Description,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    TrainerName = s.SessionTrainer.Name,// related Data
            //    Capacity = s.Capacity,
            //    AvailableSlots = s.Capacity - unitOfWork.sessionRepository.GetCountOfBookedSlots(s.Id)// related Data
            //});

            var mappedSessions = mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            };
            return mappedSessions;
        }
        public SessionViewModel? GetSessionById(int sessionId)
        {
            var session = unitOfWork.sessionRepository.GetSessionSessionWithTrainerAndCategory(sessionId);
            if (session is null)
            {
                return null;
            }

            var mappedSession = mapper.Map<Session, SessionViewModel>(session);
            mappedSession.AvailableSlots = session.Capacity - unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id);
            return mappedSession;
        }
        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (!IsSessionAvailableForUpdating(session!))
            {
                return null;    
            }

            return mapper.Map<UpdateSessionViewModel>(session);
        }
        public bool UpdateSession(UpdateSessionViewModel updateSession, int sessionId)
        {
            try
            {

                var session = unitOfWork.GetRepository<Session>().GetById(sessionId);
                if (!IsSessionAvailableForUpdating(session!))
                {
                    return false;
                }
                if (!IsTrainerExists(updateSession.TrainerId))
                {
                    return false;
                }

                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate))
                {
                    return false;
                }

                mapper.Map(updateSession, session);
                session!.UpdatedAt = DateTime.Now;
                unitOfWork.sessionRepository.Update(session);
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Session Falid: {ex}");
                return false;
            }
        }

        public IEnumerable<TrainerSelectViewModel> GetAllTrainersForDropDown()
        {
            var Trainers = unitOfWork.GetRepository<Trainer>().GetAll();
            return mapper.Map<IEnumerable<TrainerSelectViewModel>>(Trainers);
        }

        public IEnumerable<CategorySelectViewModel> GetAllCategoryForDropDown()
        {
            var Categories = unitOfWork.GetRepository<Category>().GetAll();
            return mapper.Map<IEnumerable<CategorySelectViewModel>>(Categories);
        }

        #region Helper
        private bool IsSessionAvailableForUpdating(Session session)
        {
            return session.StartDate > DateTime.Now && 
                unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) == 0;
        }
        private bool IsSessionAvailableForRemoving(Session session)
        {
            if (session is null) return false;
            // If Session Started - No Delete Allowed
            if (session.StartDate <= DateTime.Now && session.EndDate > DateTime.Now) return false;
            // If Session Has Active Bookings - No Delete Allowed
            var hasActiveBooking = unitOfWork.sessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBooking) return false;

            return true;
        }
        private bool IsTrainerExists(int trainerId)
        {
            return unitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        }

        private bool IsCategoryExists(int categoryId)
        {
            return unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime startDate, DateTime EndDate)
        {
            return EndDate > startDate && DateTime.Now < startDate  ;

        }
        #endregion
    }

}
