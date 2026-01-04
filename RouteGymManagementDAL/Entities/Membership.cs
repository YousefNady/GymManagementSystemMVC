namespace RouteGymManagementDAL.Entities
{
    public class Membership : BaseEntity
    {

        // StartDate == CreatedAt (BaseEntity) 

        public DateTime EndDate { get; set; }

        // Business Rule: Membership status is computed: "Active"
        // if EndDate > Now , else "Expired"
        // readOnly Property
        public string Status
        {
            get
            {
                if (EndDate <= DateTime.Now)
                {
                    return "Expired";
                }
                else
                {
                    return "Active";
                }
            }
        }


        public int MemberId { get; set; }

        public Member Member { get; set; } = null!;

        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

    }
}
