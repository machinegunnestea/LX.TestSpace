namespace LX.TestSpace.Client.Entities
{
    public class UserAnswersSnapshot
    {
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }
        public bool IsSelected { get; set; }

    }
}
