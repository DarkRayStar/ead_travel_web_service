namespace TransportManagmentSystemAPI.DBconfig
{
    // Defines the contract for schema names used in the database.
    public interface IScheam
    {
        public string UsersScheama { get; set; }
        public string TravellerScheama { get; set; }
        public string TrainScheam { get; set; }
        public string ReservationScheam { get; set; }
        public string ScheduleScheam { get; set; }


    }
}
