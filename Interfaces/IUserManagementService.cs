using TransportManagmentSystemAPI.Models;

// This interface defines a method for user login management.
namespace TransportManagmentSystemAPI.Interfaces
{
    public interface IUserManagementService
    {
        // Method for user login management.
        UserManagement UserLoginMangement(UserManagement user);
    }
}
