using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.DAL.Entities
{
    public class User:IdentityUser
    {
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        [StringLength(20)]
        public string Surname { get; set; } = string.Empty;
        public IEnumerable<TestResult> TestResults { get; set; }
    }
}
