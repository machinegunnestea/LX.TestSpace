using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Client.Entities
{
    public class User
    {
        public string Id { get; set; }
        [StringLength(20)]
        public string Name { get; set; } = string.Empty;
        [StringLength(20)]
        public string Surname { get; set; } = string.Empty;
        [StringLength(40)]
        public string Email { get; set; } = string.Empty;
        public IList<string> UserRoles { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
