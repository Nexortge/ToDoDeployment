namespace Todo.entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public ICollection<ToDo> ToDos { get; set; } = new List<ToDo>();
    }
}