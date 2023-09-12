using LX.TestSpace.Server.DAL.Entities;

namespace LX.TestSpace.Server.BLL.Models
{
    public class TestResultDTO
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public int TestId { get; set; }
        public TestSnapshot TestSnapshot { get; set; }
        public List<QuestionSnapshot> QuestionSnapshots { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime FinishedAt { get; set; }
        public double TestScore { get; set; }

    }
}
