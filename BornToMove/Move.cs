namespace BornToMove
{
    public class Move
    {
        public int Id { set; get; }  
        public String name { set; get; }
        public String description { set; get; }
        public int sweatrate { set; get; }

        public Move(int Id, String name, String description, int sweatrate)
        {
            this.Id = Id;
            this.name = name;
            this.description = description;
            this.sweatrate = sweatrate;
        }
    }
}