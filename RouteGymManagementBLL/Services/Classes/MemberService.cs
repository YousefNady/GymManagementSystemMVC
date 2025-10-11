using Microsoft.IdentityModel.Tokens;
using RouteGymManagementBLL.Services.Interfaces;
using RouteGymManagementBLL.ViewModels.MemberViewModels;
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


    // ASK CLR For Creating Object From Service
    // CLR Will Inject Address Of Object In Constructor

    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel createdMember)
        {
            try
            {
                //// Check If Email Is Exists
                //var emailExists = memberRepository.GetAll( X => X.Email == createdMember.Email).Any();

                //// Check If Phone Is Exists
                //var phoneExists = memberRepository.GetAll( X => X.Phone == createdMember.Phone).Any();

                // If One Of Them Exists Return False
                if (IsEmailExists(createdMember.Email) || IsPhoneExists(createdMember.Phone)) return false;

                // If Not Add Member And Return True if added
                var member = new Member()
                {
                    Name = createdMember.Name,
                    Email = createdMember.Email,
                    Phone = createdMember.Phone,
                    Gender = createdMember.Gender,
                    DateOfBirth = createdMember.DateOfBirth,
                    Address = new Address()
                    {
                        BuildingNumber = createdMember.BuildingNumber,
                        City = createdMember.City,
                        Street = createdMember.Street
                    },
                    HealthRecord = new HealthRecord()
                    {
                        Height = createdMember.HealthRecordViewModel.Height,
                        Weight = createdMember.HealthRecordViewModel.Weight,
                        BloodType = createdMember.HealthRecordViewModel.BloodType, // Fixed typo: BLoodType → BloodType
                        Note = createdMember.HealthRecordViewModel.Note
                    }
                };
                 unitOfWork.GetRepository<Member>().Add(member); // added localy
                return unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var Members = unitOfWork.GetRepository<Member>().GetAll();
            if (Members is null || !Members.Any())
            {
                return [];
            }

            #region Manual Mapping way01
            //var MemberViewModels = new List<MemberViewModel>();
            //foreach (var member in Members)
            //{
            //    var memberViewModels = new MemberViewModel()
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString()
            //    };
            //    MemberViewModels.Add(memberViewModels);
            //}
            //return MemberViewModels; 
            #endregion

            #region Manual Mapping way02

            var MembersViewModels = Members.Select(x => new MemberViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Email = x.Email,
                Phone = x.Phone,
                Photo = x.Photo,
                Gender = x.Gender.ToString()
            });
            return MembersViewModels;


            #endregion
        }

        public HealthRecordViewModel? GetMemberHealthRecordDetails(int MemberId)
        {
            var MemberHealthRecord = unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if (MemberHealthRecord is null)
            {
                return null;
            };


            return new HealthRecordViewModel()
            {
                BloodType = MemberHealthRecord.BloodType,
                Height = MemberHealthRecord.Height,
                Weight = MemberHealthRecord.Weight,
                Note = MemberHealthRecord.Note,
            };
        }

        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {
            var Member = unitOfWork.GetRepository<Member>().GetById(memberId);
            if (Member is null)
            {
                return null;
            }
            return new MemberToUpdateViewModel()
            { 
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Photo = Member.Photo,
                BuildingNumber = Member.Address.BuildingNumber,
                City = Member.Address.City,
                Street = Member.Address.Street
            };
        }

        public MemberViewModel? GetMemberViewDetails(int MemberId)
        {
            var Member = unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (Member is null)
            {
                return null;
            }

            var ViewModel = new MemberViewModel()
            {
                Name = Member.Name,
                Email = Member.Email,
                Phone = Member.Phone,
                Gender = Member.Gender.ToString(),
                DateOfBirth = Member.DateOfBirth.ToShortDateString(),
                Address = $"{Member.Address.BuildingNumber} - {Member.Address.City} - {Member.Address.Street}",
                Photo = Member.Photo,
            };

            var ActiveMemberShip = unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == MemberId && x.Status == "Active").FirstOrDefault();

            if (ActiveMemberShip is not null)
            {
                ViewModel.MembershipStartDate = ActiveMemberShip.CreatedAt.ToShortDateString();
                ViewModel.MembershipEndDate = ActiveMemberShip.EndDate.ToShortDateString();
                var plan = unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId);
                ViewModel.PlanName = plan?.Name;
            }
            return ViewModel;
        }

        public bool UpdateMemberDetails(int Id, MemberToUpdateViewModel UpdatedMember)
        {

            try 
            {
                var memberRepo = unitOfWork.GetRepository<Member>();

                if (IsEmailExists( UpdatedMember.Email) || IsPhoneExists(UpdatedMember.Phone)) return false;

                var member = memberRepo.GetById(Id);
                if (member is null) return false;

                member.Email = UpdatedMember.Email;
                member.Phone = UpdatedMember.Phone;

                // Check if Address exists before updating its properties
                if (member.Address != null)
                {
                    member.Address.BuildingNumber = UpdatedMember.BuildingNumber;
                    member.Address.City = UpdatedMember.City;
                    member.Address.Street = UpdatedMember.Street;
                }

                member.UpdatedAt = DateTime.Now;
                 memberRepo.Update(entity: member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }



        }

        public bool RemoveMember(int MemberId)
        {
            var memberRepo = unitOfWork.GetRepository<Member>();

            var Member = memberRepo.GetById(MemberId);
            if (Member is null) return false;

            var HasActiveMemberSessions = unitOfWork.GetRepository<MemberSession>().GetAll(x => x.MemberId == MemberId && x.Session.StartDate > DateTime.Now).Any();

            if (HasActiveMemberSessions) { return false; }


            var membershipRepo = unitOfWork.GetRepository<Membership>();

            var memberships = membershipRepo.GetAll(x => x.MemberId == MemberId);
            try
            {
                if (memberships.Any())
                {
                    foreach (var membership in memberships)
                    {
                        membershipRepo.Delete(membership);
                    }
                }
                memberRepo.Delete(Member);
                return unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }


        #region HelperMethods
        private bool IsPhoneExists(string phone)
        {
            return unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        }

        private bool IsEmailExists(string email)
        {
            return unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }
        #endregion




    }
}
