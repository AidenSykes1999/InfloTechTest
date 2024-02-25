using System.ComponentModel;
using System.Linq;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    public UsersController(IUserService userService) => _userService = userService;

    [HttpGet]
    public ViewResult List(bool? active)

    //Since we want either to filter by active, inactive or 'all', the best way to do this is to check if the HTTP has 'active' as a value and if it is true or false. If not, it shall show 'all' instead.

    {
        IEnumerable<UserListItemViewModel> items;

        if (active.HasValue)
        {
            items = _userService.FilterByActive(active.Value)
                .Select(p => new UserListItemViewModel
                {
                    Id = p.Id,
                    Forename = p.Forename,
                    Surname = p.Surname,
                    Email = p.Email,
                    DateofBirth = p.DateofBirth,
                    IsActive = p.IsActive
                });
        }
        else
        {
            items = _userService.GetAll()
                .Select(p => new UserListItemViewModel
                {
                    Id = p.Id,
                    Forename = p.Forename,
                    Surname = p.Surname,
                    Email = p.Email,
                    DateofBirth = p.DateofBirth,
                    IsActive = p.IsActive
                });
        }

        var model = new UserListViewModel
        {
            Items = items.ToList()
        };

        return View(model);
    }

    //New route is needed to ensure no AmbiguousMatchException

    [Route("add")]
    // Add User controller requires both Posting and Getting as we need to create ViewModel

    [HttpGet]
    public IActionResult Add()
    {
        var model = new AddUserViewModel(); // You need to create this ViewModel
        return View(model);
    }


    [HttpPost]
    public IActionResult Add(AddUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Perform the logic to add the user to the database using your UserService
            // Example: _userService.AddUser(model.ToUser()); // You need to create this method

            // Redirect to the user list after successful addition
            return RedirectToAction("List");
        }

        // If ModelState is not valid, return to the Add view with the validation errors
        return View(model);
    }
}

