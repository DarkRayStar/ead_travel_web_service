﻿namespace TransportManagmentSystemAPI.DBconfig
{
    public interface IDatabaseSettings
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
