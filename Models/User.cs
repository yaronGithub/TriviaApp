namespace TriviaAppClean.Models
{

    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Score { get; set; }
        public int Rank { get; set; }
        public List<AmericanQuestion> Questions { get; set; }
    }

}
