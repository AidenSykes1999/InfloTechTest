using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Web.Models.Users;

public class UserListViewModel
{
    public List<UserListItemViewModel> Items { get; set; } = new();
}

public class UserListItemViewModel
{
    public long Id { get; set; }
    public string? Forename { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
    public DateTime DateofBirth { get; internal set; }
}

public class AddUserViewModel
{
    [Required(ErrorMessage = "Forename is required")]
    public string? Forename { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    public string? Surname { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateTime DateOfBirth { get; set; }

    [Required(ErrorMessage = "Active status is required")]
    public bool IsActive { get; set; }
}
