namespace TransportManagmentSystemAPI.DBconfig
{
    // Interface - Defines the contract for database connection settings.
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
