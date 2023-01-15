using Core.Entities;
using Core.Entities.Abstract;

namespace Entities.Concrete.Dtos
{
    public class UserLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
