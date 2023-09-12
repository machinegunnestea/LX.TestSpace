using LX.TestSpace.Server.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace LX.TestSpace.Server.BLL.Models
{
    public class TestDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserNameCreate { get; set; }
        public bool IsPublished { get; set; }
        public IEnumerable<QuestionDTO> Questions { get; set; }
    }
}
