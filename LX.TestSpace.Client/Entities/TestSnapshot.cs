namespace LX.TestSpace.Client.Entities
{
    public class TestSnapshot
    {
        public TestSnapshot(string name, string description, TimeSpan duration, DateTime creationDate, string userNameCreate, int testId)
        {
            Name = name;
            Description = description;
            Duration = duration;
            CreationDate = creationDate;
            UserNameCreate = userNameCreate;
            TestId = testId;
        }

        public TestSnapshot()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan Duration { get; set; }
        public DateTime CreationDate { get; set; }
        public string UserNameCreate { get; set; }
        public int? TestId { get; set; }
    }
}
