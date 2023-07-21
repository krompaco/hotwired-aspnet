using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public class PlayerFormModel
{
    public PlayerFormModel()
    {
        this.Id = Guid.Empty.ToString("D");
        this.Name = string.Empty;
        this.Ranking = 500;
    }

    public string Id { get; set; }

    [DisplayName("Enter name:")]
    [Required(ErrorMessage = "Name is a required field.")]
    [StringLength(1000, ErrorMessage = "Attack protection activated, too many characters.")]
    public string Name { get; set; }

    [DisplayName("Enter ranking:")]
    [Range(1, 500, ErrorMessage = "Set ranking between 1 and 500.")]
    public int Ranking { get; set; }
}
