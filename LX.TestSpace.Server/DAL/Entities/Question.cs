using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.DAL.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        [StringLength(1000, MinimumLength = 5)]
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public string? CodeSnippet { get; set; }
        public IEnumerable<Answer> Answers { get; set; }

    }
}
