using LX.TestSpace.Server.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.BLL.Models
{
    public class QuestionDTO
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public string? CodeSnippet { get; set; }
        public bool IsMultipleAnswers { get; set; }
        public IEnumerable<AnswerDTO> Answers { get; set; }
    }
}
