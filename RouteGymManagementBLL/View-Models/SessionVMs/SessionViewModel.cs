namespace RouteGymManagementBLL.View_Models.SessionVMs
{
    public class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public int AvailableSlots { get; set; }
        public int BookedSlots { get; set; }
        public int Capacity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


        #region Computed MyProperties
        public string DateForDisplay
        {
            get
            {
                return $"{StartDate:MM:dd:yyyy}";
            }
        }
        public string TimeRangeForDisplay
        {
            get
            {
                return $"{StartDate:hh:mm tt} - {EndDate:hh:mm tt}";
            }
        }
        public TimeSpan Duration => EndDate - StartDate;
        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now)
                {
                    return "Upcoming";
                }
                else if (StartDate < DateTime.Now && EndDate > DateTime.Now)
                {
                    return "Ongoing";
                }
                else
                {
                    return "Completed";
                }
            }
        }

        #endregion


    }
}
