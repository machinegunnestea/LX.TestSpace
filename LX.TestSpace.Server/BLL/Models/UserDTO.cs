﻿using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.BLL.Models
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? UserName { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> UserRoles { get; set; }
    }
}
