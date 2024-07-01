namespace Ratting.Domain
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }
        
        public int BestResult { get; set; }
    }
}
