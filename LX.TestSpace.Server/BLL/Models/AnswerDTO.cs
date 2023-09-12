using LX.TestSpace.Server.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.BLL.Models
{
    public class AnswerDTO
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }
    }
}
