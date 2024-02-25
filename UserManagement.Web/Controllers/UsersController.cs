using System.Linq;
using UserManagement.Data;
using UserManagement.Models;
using UserManagement.Services.Domain.Interfaces;
using UserManagement.Web.Models.Users;

namespace UserManagement.WebMS.Controllers;

[Route("users")]
public class UsersController : Controller
{
    private readonly IUserService _userService;
    // In order to Add, Update, Delete or Edit, we must also add the data context here and update UsersController to also have it.
    private readonly IDataContext _dataContext;

    public UsersController(IUserService userService, IDataContext dataContext)
    {
        _userService = userService;
        _dataContext = dataContext;
    }

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
            var newUser = new User
            {
                Forename = model.Forename,
                Surname = model.Surname,
                Email = model.Email,
                DateofBirth = model.DateofBirth,
                IsActive = model.IsActive
            };

            // Add the new user to the data context
            _dataContext.Create(newUser);

            // Redirect to the user list after successful addition
            return RedirectToAction("List");
        }

        // If ModelState is not valid, return to the Add view with the validation errors
        return View(model);
    }
}

