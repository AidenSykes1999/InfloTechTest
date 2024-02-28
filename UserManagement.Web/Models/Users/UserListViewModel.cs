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

    // Validate these values aren't empty.
    [Required(ErrorMessage = "Forename is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Forename must be between 1 and 100 characters")]
    public string Forename { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Surname must be between 1 and 100 characters")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateTime DateofBirth { get; set; }

    [Required(ErrorMessage = "Active status is required")]
    public bool IsActive { get; set; }
}

public class ViewUserViewModel
{
    public long Id { get; set; }

    [Required(ErrorMessage = "Forename is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Forename must be between 1 and 100 characters")]
    public string Forename { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Surname must be between 1 and 100 characters")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public DateTime DateofBirth { get; set; }
}

public class EditUserViewModel
{
    [Required(ErrorMessage = "ID is required")]
    public long Id { get; set; }

    [Required(ErrorMessage = "Forename is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Forename must be between 1 and 100 characters")]
    public string Forename { get; set; } = string.Empty;

    [Required(ErrorMessage = "Surname is required")]
    [StringLength(100, MinimumLength = 1, ErrorMessage = "Surname must be between 1 and 100 characters")]
    public string Surname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Date of Birth is required")]
    [DataType(DataType.Date, ErrorMessage = "Invalid date format")]
    public DateTime DateofBirth { get; set; }

    [Required(ErrorMessage = "Active status is required")]
    public bool IsActive { get; set; }
}
