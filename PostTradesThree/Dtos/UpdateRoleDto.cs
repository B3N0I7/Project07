using System.ComponentModel.DataAnnotations;

namespace PostTradesThree.Dtos
{
    public class UpdateRoleDto
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
    }
}
