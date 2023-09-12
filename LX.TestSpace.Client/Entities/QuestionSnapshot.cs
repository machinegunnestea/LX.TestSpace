namespace LX.TestSpace.Client.Entities
{
    public class QuestionSnapshot
    {
        public QuestionSnapshot(string text, string imagePath, double questionResult, string codeSnippet)
        {
            Text = text;
            ImagePath = imagePath;
            QuestionResult = questionResult;
            CodeSnippet = codeSnippet;
        }

        public QuestionSnapshot()
        {
        }

        public string Text { get; set; }
        public string? ImagePath { get; set; }
        public string? CodeSnippet { get; set; }
        public double QuestionResult { get; set; }
        public List<UserAnswersSnapshot> UserAnswersSnapshots { get; set; } = new();
    }
}
