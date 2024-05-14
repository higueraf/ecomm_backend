using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecomm.Application.Dtos.User.Response
{
    public class TokenResponseDto
    {
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? Email { get; set; }
    }
}
