namespace LX.TestSpace.Server.DAL.Entities
{
    public class TestSnapshot
    {
        public TestSnapshot(string name, string description, TimeSpan duration, DateTime creationDate, string userNameCreate, int? testId)
        {
            Name = name;
            Description = description;
            Duration = duration;
            CreationDate = creationDate;
            UserNameCreate = userNameCreate;
            TestId = testId;
        }

        public int? TestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserNameCreate { get; set; }
    }
}
