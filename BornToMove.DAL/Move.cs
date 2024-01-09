namespace BornToMove
{
    public class Move
    {
        public int Id { get; set; } = 0;
        public String name { set; get; }
        public String description { set; get; }
        public int sweatrate { set; get; }

        virtual public ICollection<MoveRating> Ratings { get; set; }

        public Move(String name, String description, int sweatrate)
        {
            this.name = name;
            this.description = description;
            this.sweatrate = sweatrate;
            this.Ratings = new List<MoveRating>();
        }
    }
}