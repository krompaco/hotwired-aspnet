using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class TurboFrameExampleFormModel
{
    [DisplayName("Enter your e-mail:")]
    [Required(ErrorMessage = "E-mail is a required field.")]
    [EmailAddress(ErrorMessage = "Value entered is not a valid e-mail address.")]
    [StringLength(1000, ErrorMessage = "Attack protection activated, too many characters.")]
    public string? Email { get; set; }

    [Range(typeof(bool), "true", "true", ErrorMessage = "Terms needs to be accepted.")]
    public bool Accept { get; set; }
}
