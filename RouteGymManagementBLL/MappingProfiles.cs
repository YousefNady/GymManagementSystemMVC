using AutoMapper;
using GymManagementSystemBLL.ViewModels.SessionViewModels;
using RouteGymManagementBLL.ViewModels.MemberViewModels;
using RouteGymManagementBLL.ViewModels.PlanViewModels;
using RouteGymManagementBLL.ViewModels.SessionViewModels;
using RouteGymManagementBLL.ViewModels.TrainerViewModels;
using RouteGymManagementDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementBLL
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            #region Session 
            CreateMap<Session, SessionViewModel>()
                .ForMember(destination => destination.CategoryName, option => option.MapFrom(src => src.SessionCategore.CategoryName))
                .ForMember(destination => destination.TrainerName, option => option.MapFrom(src => src.SessionTrainer.Name))
                .ForMember(destination => destination.AvailableSlots, option => option.Ignore());

            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
            #endregion

            #region Member
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(destination => destination.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }))
                .ForMember(destination => destination.HealthRecord, opt => opt.MapFrom(src => new HealthRecord
                {
                    Weight = src.HealthRecordViewModel.Weight,
                    Height = src.HealthRecordViewModel.Height,
                    BloodType = src.HealthRecordViewModel.BloodType,
                    Note = src.HealthRecordViewModel.Note
                }));

            CreateMap<Member, MemberViewModel>()
                .ForMember(destination => destination.Gender, opt => opt.MapFrom(src => src.Gender.ToString()))
                .ForMember(destination => destination.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToString()))
                .ForMember(destination => destination.Address, opt => opt.MapFrom(src => src.Address != null ? $"{src.Address.BuildingNumber}, {src.Address.Street}, {src.Address.City}" : null))
                .ForMember(destination => destination.PlanName, opt => opt.Ignore())
                .ForMember(destination => destination.MembershipStartDate, opt => opt.Ignore())
                .ForMember(destination => destination.MembershipEndDate, opt => opt.Ignore());

            CreateMap<HealthRecord, HealthRecordViewModel>();

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(destination => destination.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(destination => destination.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(destination => destination.City, opt => opt.MapFrom(src => src.Address.City));
            #endregion

            #region Plan
            CreateMap<Plan, PlanViewModel>();
            CreateMap<Plan, UpdatePlanViewModel>()
                .ForMember(destination => destination.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Trainer
            // CreateTrainerViewModel -> Trainer
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(destination => destination.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(destination => destination.Specialties, opt => opt.MapFrom(src => src.Specialties.ToString()));

            CreateMap<UpdateTrainerViewModel, Trainer>()
                .ForMember(destination => destination.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, UpdateTrainerViewModel>()
                .ForMember(destination => destination.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(destination => destination.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(destination => destination.City, opt => opt.MapFrom(src => src.Address.City));
            #endregion
        }

    }
    
}
