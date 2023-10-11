using System;
using System.Collections.Generic;
using TransportManagmentSystemAPI.Models;

namespace TransportManagmentSystemAPI.Services
{
    interface ITravallerManagementService
    {
        TravallerManagement CreateAndUpdateTravellerAccount(TravallerManagement travallerProfile);
        List<TravallerManagement> DisplayAllActiveAccounts(bool isActive);
        String DeletedTravellerAccount(String _Nic);
        TravallerManagement ManageActivationTravellerAccountDetails(string nic ,TravallerManagement travallerProfile);
        TravallerManagement GetTravallerAccountDetailsByNic(string _Nic);
    }
}
