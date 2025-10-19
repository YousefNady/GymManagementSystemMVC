using Microsoft.EntityFrameworkCore;
using RouteGymManagementDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RouteGymManagementDAL.Entities
{
    public abstract class GymUser : BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        // Navigation Property of Composite Attribute 
        public Address Address { get; set; } = null!;

    }

    [Owned]
    public class Address // Composite Attribute 
    {
        public int BuildingNumber { get; set; }

        public string Street { get; set; } = null!;
        public string City { get; set; } = null!;
    }
}
