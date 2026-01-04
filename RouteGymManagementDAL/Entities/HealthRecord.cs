namespace RouteGymManagementDAL.Entities
{

    // 1 - 1 relationship with Member [Shared PK]
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; } = null!;
        public string? Note { get; set; }


        #region Relationships


        #region Member - HealthRecord

        public Member Member { get; set; } = null!;


        #endregion


        #endregion

    }
}
