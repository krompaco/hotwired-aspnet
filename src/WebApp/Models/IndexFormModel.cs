using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class IndexFormModel
{
    [DisplayName("Enter your name:")]
    [Required(ErrorMessage = "Name is a required field.")]
    [StringLength(1000, ErrorMessage = "Attack protection activated, too many characters.")]
    public string? Name { get; set; }

    [DisplayName("Enter your company:")]
    [Required(ErrorMessage = "Company is a required field.")]
    [StringLength(1000, ErrorMessage = "Attack protection activated, too many characters.")]
    public string? Company { get; set; }
}
