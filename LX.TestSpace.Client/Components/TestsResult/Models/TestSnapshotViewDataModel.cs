using LX.TestSpace.Client.Entities;

namespace LX.TestSpace.Client.Components.TestsResult.Models;

public class TestSnapshotViewDataModel : Test
{
    public TestSnapshotViewDataModel(Test test)
    {
        Id = test.Id;
        Name = test.Name;
        Description = test.Description;
        Description = test.Description;
        Duration = test.Duration;
        Questions = test.Questions;
    }
    
    public bool IsExpanded { get; set; }
}