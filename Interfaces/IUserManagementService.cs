using TransportManagmentSystemAPI.Models;

namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IUserManagementService
    {
        UserManagement UserLoginMangement(UserManagement user);
    }
}
