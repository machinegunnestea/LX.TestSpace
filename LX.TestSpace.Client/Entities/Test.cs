using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Client.Entities
{
    public class Test
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        [StringLength(500, MinimumLength = 1)]
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserNameCreate { get; set; }
        public bool IsPublished { get; set; }
        public List<Question> Questions { get; set; }
        public List<TestResult> TestResults { get; set; }
    }
}
