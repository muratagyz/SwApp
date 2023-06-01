using System.ComponentModel.DataAnnotations;

namespace SwAppViewModel.UI;

public class ContactMessage
{
    [Required] public string Fullname { get; set; }

    [Required] public string Email { get; set; }

    [Required] public string Number { get; set; }

    [Required] public string Message { get; set; }
}