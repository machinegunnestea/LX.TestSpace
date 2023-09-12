using LX.TestSpace.Client.Entities;

namespace LX.TestSpace.Client.Components.TestsResult.Models;

public class UserPersonalDataViewModel : UserPersonalData
{
    public UserPersonalDataViewModel(UserPersonalData data)
    {
        Id = data.Id;
        Email = data.Email;
        Name = data.Name;
        Surname = data.Surname;
    }

    public bool IsExpanded { get; set; }
}