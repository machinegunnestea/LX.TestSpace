using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Client.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question Question { get; set; }
        [StringLength(250, MinimumLength = 1)]
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}
