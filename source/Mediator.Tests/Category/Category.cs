namespace DotNetCore.Mediator.Tests
{
    public class Category
    {
        public Category(long id, string name)
        {
            Id = id;
            Name = name;
        }

        public long Id { get; }

        public string Name { get; }
    }
}
