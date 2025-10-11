using Microsoft.Identity.Client.Extensibility;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.PlanViewModels;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Classes;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL.Services.Classes
{
    internal class PlanService : IPlanService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var plans = unitOfWork.GetRepository<Plan>().GetAll();

            if (plans is null || !plans.Any())
            {
                return [];
            }

            return plans.Select(p => new PlanViewModel()
            {
                Id = p.Id,
                Description = p.Description,
                DurationDays = p.DurationDays,
                IsActice = p.IsActive,
                Name = p.Name,
                Price = p.Price
            });
        }

        public PlanViewModel? GetPlanById(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null)
            {
                return null;
            }
            return new PlanViewModel()
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                IsActice = plan.IsActive,
                Price = plan.Price
            };
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int PlanId)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null || plan.IsActive == false || HasActiveMemberShips(PlanId))
            {
                return null;
            }

            return new UpdatePlanViewModel()
            {
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Name = plan.Name,
                Price = plan.Price

            };
        }


        // soft delete
        public bool ToggleStatus(int PlanId)
        {
            var repo = unitOfWork.GetRepository<Plan>();

            var plan = repo.GetById(PlanId);
            if (plan is null || HasActiveMemberShips(PlanId))
            {
                return false;
            }
            if (plan.IsActive == true)
            {
                return plan.IsActive = false;
            }
            else
            {
                plan.IsActive = true;
            }
            plan.UpdatedAt = DateTime.Now;
            try
            {
                repo.Update(plan);

                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
            return false;
            }
        
        }

        public bool UpdatePlan(int PlanId, PlanViewModel Updatedplan)
        {
            var plan = unitOfWork.GetRepository<Plan>().GetById( PlanId);
            if (plan is null || HasActiveMemberShips(PlanId)) return false;

            try
            {
                // Update plan properties
                plan.Description = Updatedplan.Description;
                plan.Price = Updatedplan.Price;
                plan.DurationDays = Updatedplan.DurationDays;
                plan.UpdatedAt = DateTime.Now;

                unitOfWork.GetRepository<Plan>().Update( plan);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        #region Helper

        private bool HasActiveMemberShips(int planId)
        {
            var MemberShips = unitOfWork.GetRepository<Membership>()
                .GetAll(x => x.PlanId == planId && x.Status == "Active");
            return MemberShips.Any();
        }
        #endregion  
    }
    }
