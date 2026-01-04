namespace RouteGymManagementDAL.Entities
{
    public class MemberSession : BaseEntity
    {

        // booking date == CreatedAt (BaseEntity) 

        public bool IsAttended { get; set; }

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int SessionId { get; set; }

        public Session Session { get; set; } = null!;


    }
}
