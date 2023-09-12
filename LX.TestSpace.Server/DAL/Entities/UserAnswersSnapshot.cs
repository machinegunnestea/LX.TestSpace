namespace LX.TestSpace.Server.DAL.Entities
{
    public class UserAnswersSnapshot
    {
        public UserAnswersSnapshot(int answerId, string text, bool? isCorrect, bool isSelected)
        {
            AnswerId = answerId;
            Text = text;
            IsCorrect = isCorrect;
            IsSelected = isSelected;
        }
        public int AnswerId { get; set; }
        public string Text { get; set; }
        public bool? IsCorrect { get; set; }  
        public bool IsSelected { get; set; }
        
    }
}
