using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class GlobalMessageFormModel
{
    [DisplayName("Enter a global message:")]
    [Required(ErrorMessage = "Global message is a required field.")]
    [StringLength(1000, ErrorMessage = "Attack protection activated, too many characters.")]
    public string? Message { get; set; }
}
