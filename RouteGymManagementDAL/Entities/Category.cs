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
