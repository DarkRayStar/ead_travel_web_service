using System;
using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

// This interface defines methods for managing traveler accounts.
namespace TransportManagmentSystemAPI.Services
{
    interface ITravallerManagementService
    {
        // Method to create or update a traveler account.
        TravallerManagement CreateAndUpdateTravellerAccount(TravallerManagement travallerProfile);
        // Method to display all active traveler accounts.
        List<TravallerManagement> DisplayAllActiveAccounts(bool isActive);
        // Method to delete a traveler account.
        String DeletedTravellerAccount(String _Nic);
        // Method to manage activation details of a traveler account.
        TravallerManagement ManageActivationTravellerAccountDetails(string nic ,TravallerManagement travallerProfile);
        // Method to get traveler account details by NIC.
        TravallerManagement GetTravallerAccountDetailsByNic(string _Nic);
    }
}
