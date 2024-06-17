using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaWebMaster.Models.ViewModel
{
    public class LoginVM
    {
        [Required]
        public string? user { get; set; }

        [Required]
        public string? password { get; set; }
    }
}
