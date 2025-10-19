using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace RouteGymManagementDAL.Entities
{
    public class Category : BaseEntity
    {
        public string CategoryName { get; set; } = null!;

        #region Relationships
        #region Category - Session
        public ICollection<Session> Sessions { get; set; } = null!;
        #endregion
        #endregion

    }
}
