using Core.Entities.Abstract;

namespace Entities.DTOs
{
    public class UpdatePasswordDTO : IDto
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }
    }
}
