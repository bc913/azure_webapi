using System;

namespace Bcan.Backend.Application.Features.Users.Queries.GetUsers
{
    public class UserLiteDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string NickName { get; set; }
    }
}