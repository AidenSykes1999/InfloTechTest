using System.Collections.Generic;
using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;

namespace UserManagement.Services.Domain.Implementations;

public class UserService : IUserService
{
    private readonly IDataContext _dataAccess;
    public UserService(IDataContext dataAccess) => _dataAccess = dataAccess;

    /// <summary>
    /// Return users by active state
    /// </summary>
    /// <param name="isActive"></param>
    /// <returns></returns>
    ///

    //public IEnumerable<User> FilterByActive(bool isActive)
    //{
    //    //return _dataAccess.GetAll<User>().Where(user => user.IsActive == isActive);

    //    throw new NotImplementedException();
    //}

    // Using the same structure as the GetAll feature in order for the code to stay cohesive. FilterbyInactive would be achieved just be != rather than ==
    public IEnumerable<User> FilterByActive(bool isActive) => _dataAccess.GetAll<User>().Where(user => user.IsActive == isActive);

    public IEnumerable<User> GetAll() => _dataAccess.GetAll<User>();


    // Implemented GetUserById for view functionality
    public User? GetUserById(long id)
    {
        return _dataAccess.GetById<User>(id);
    }

    // Implemented Update User for updating user functionality
    public void UpdateUser(User user)
    {
        _dataAccess.Update(user);
    }

    public void DeleteUser(long id)
    {
        var user = _dataAccess.GetById<User>(id);
        if (user != null)
        {
            _dataAccess.Delete(user);
        }
    }
}
