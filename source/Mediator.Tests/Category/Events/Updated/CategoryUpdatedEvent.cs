namespace DotNetCore.Mediator.Tests
{
    public class CategoryUpdatedEvent
    {
        public CategoryUpdatedEvent(Category category)
        {
            Category = category;
        }

        public Category Category { get; }
    }
}
