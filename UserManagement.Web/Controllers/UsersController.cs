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

    [Route("view")]

    [HttpGet]
    public IActionResult View(long id)
    {
        // Retrieve the user by id from your UserService
        var user = _userService.GetUserById(id);

        if (user == null)
        {
            // Handle user not found, for example, redirect to the user list
            return RedirectToAction("List");
        }

        var model = new ViewUserViewModel
        {
            // Populate the ViewModel properties with the user information
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateofBirth = user.DateofBirth
        };

        return View(model);
    }


    [Route("edit")]
    public IActionResult Edit(long id)
    {
        // Retrieve the user by id from your UserService
        var user = _userService.GetUserById(id);

        if (user == null)
        {
            // Handle user not found, for example, redirect to the user list
            return RedirectToAction("List");
        }

        var model = new EditUserViewModel
        {
            // Populate the ViewModel properties with the user information
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            IsActive = user.IsActive,
            DateofBirth = user.DateofBirth
        };

        return View(model);
    }

    [HttpPost]
    public IActionResult Edit(EditUserViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Perform the logic to update the user in the database using your UserService
            var user = _userService.GetUserById(model.Id);

            if (user != null)
            {
                user.Forename = model.Forename;
                user.Surname = model.Surname;
                user.Email = model.Email;
                user.DateofBirth = model.DateofBirth;
                user.IsActive = model.IsActive;

                _userService.UpdateUser(user);

                // Redirect to the user list after successful edit
                return RedirectToAction("List");
            }

            // Handle the case where the user is not found
            return NotFound();
        }

        // If ModelState is not valid, return to the Edit view with the validation errors
        return View(model);
    }
    [Route("delete")]
    [HttpGet]
    public IActionResult Delete(long id)
    {
        // Retrieve user details for confirmation
        var user = _userService.GetUserById(id);

        if (user == null)
        {
            // Handle case where user is not found
            return NotFound();
        }

        var deleteViewModel = new DeleteUserViewModel
        {
            Id = user.Id,
            Forename = user.Forename,
            Surname = user.Surname,
            Email = user.Email,
            DateofBirth = user.DateofBirth,
            IsActive = user.IsActive
        };

        return View(deleteViewModel);
    }
    [Route("deleteconfirmed")]
    [HttpPost]
    [ActionName("DeleteConfirmed")]
    public IActionResult DeleteConfirmed(DeleteUserViewModel model)
    {
        if (ModelState.IsValid && model.Confirmation == "DELETE")
        {
            // Perform the logic to delete the user in the database using your UserService
            _userService.DeleteUser(model.Id);

            // Redirect to the user list after successful deletion
            return RedirectToAction("List");
        }

        // If ModelState is not valid or confirmation is not correct, return to the Delete view with errors
        return View(model);
    }

}
