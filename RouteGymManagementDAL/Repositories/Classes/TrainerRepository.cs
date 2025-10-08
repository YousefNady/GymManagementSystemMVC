using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Data.Contexts;
using RouteGymManagementDAL.Entities;
using RouteGymManagementDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Repositories.Classes
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext dbContext;

        public TrainerRepository(GymDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Trainer trainer)
        {
            dbContext.Trainers.Add(trainer);
            return dbContext.SaveChanges();
        }

        public int Delete(Trainer trainer)
        {
            dbContext.Trainers.Remove(trainer);
            return dbContext.SaveChanges();
        }

        public IEnumerable<Trainer> GetAll() => dbContext.Trainers.ToList();


        public Trainer? GetById(int Id) => dbContext.Trainers.Find(Id);

        public int Update(Trainer trainer)
        {
            dbContext.Trainers.Update(trainer);
            return dbContext.SaveChanges();
        }
    }
}
