# DotNetCore.Mediator

The smallest, simplest and fastest implementation of the mediator pattern.

## Tests

 ```csharp
[TestClass]
public class MediatorTests
{
    private readonly IMediator _mediator;

    public MediatorTests()
    {
        var services = new ServiceCollection();

        services.AddMediator(typeof(MediatorTests).Assembly);

        _mediator = services.BuildServiceProvider().GetRequiredService<IMediator>();
    }

    [TestMethod]
    public void CategoryByIdQuery()
    {
        var query = new CategoryByIdQuery { Id = 1 };

        var response = _mediator.HandleAsync<CategoryByIdQuery, Category>(query).Result;

        Assert.IsTrue(response.Succeeded && response.Data != default);
    }

    [TestMethod]
    [ExpectedException(typeof(AggregateException))]
    public void DeleteCategoryCommand_Should_ThrowException()
    {
        var command = new DeleteCategoryCommand { Id = 1 };

        _mediator.HandleAsync(command).Wait();
    }

    [TestMethod]
    public void InsertCategoryCommand()
    {
        var command = new InsertCategoryCommand { Name = "Name" };

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Succeeded && response.Data > 0);
    }

    [TestMethod]
    public void InsertCategoryCommand_Should_ValidationFail()
    {
        var command = new InsertCategoryCommand();

        var response = _mediator.HandleAsync<InsertCategoryCommand, long>(command).Result;

        Assert.IsTrue(response.Failed);
    }

    [TestMethod]
    public void UpdateCategoryCommand()
    {
        var command = new UpdateCategoryCommand { Id = 1, Name = "Name Updated" };

        var response = _mediator.HandleAsync(command).Result;

        Assert.IsTrue(response.Succeeded);
    }
}
 ```

## Query

```cs
public class CategoryByIdQuery
{
    public long Id { get; set; }
}
```

## QueryHandler

```cs
public sealed class CategoryByIdQueryHandler : IHandler<CategoryByIdQuery, Category>
{
    public Task<IResult<Category>> HandleAsync(CategoryByIdQuery request)
    {
        var category = new Category(request.Id, nameof(Category));

        return Result<Category>.SuccessAsync(category);
    }
}
```

## Command

```cs
public class InsertCategoryCommand
{
    public string Name { get; set; }
}
```

## CommandHandler

```cs
public sealed class InsertCategoryCommandHandler : IHandler<InsertCategoryCommand, long>
{
    public Task<IResult<long>> HandleAsync(InsertCategoryCommand command)
    {
        return Result<long>.SuccessAsync(1);
    }
}
```

## CommandValidator

```cs
public class InsertCategoryCommandValidator : AbstractValidator<InsertCategoryCommand>
{
    public InsertCategoryCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty();
    }
}
```

## Command

```cs
public class UpdateCategoryCommand
{
    public long Id { get; set; }

    public string Name { get; set; }
}
```

## CommandHandler

```cs
public sealed class UpdateCategoryCommandHandler : IHandler<UpdateCategoryCommand>
{
    private readonly IMediator _mediator;

    public UpdateCategoryCommandHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IResult> HandleAsync(UpdateCategoryCommand request)
    {
        var category = new Category(request.Id, request.Name);

        var categoryUpdatedEvent = new CategoryUpdatedEvent(category);

        await _mediator.HandleAsync(categoryUpdatedEvent);

        return await Result.SuccessAsync();
    }
}
```

## Event

```cs
public class CategoryUpdatedEvent
{
    public CategoryUpdatedEvent(Category category)
    {
        Category = category;
    }

    public Category Category { get; }
}
```

## EventHandler

```cs
public sealed class CategoryUpdatedEventHandler : IHandler<CategoryUpdatedEvent>
{
    public Task<IResult> HandleAsync(CategoryUpdatedEvent @event)
    {
        return Result.SuccessAsync();
    }
}
```

## Mediator

```cs
public interface IMediator
{
    Task HandleAsync<TRequest>(TRequest request);

    Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request);
}
```

```cs
public sealed class Mediator : IMediator
{
    public Task HandleAsync<TRequest>(TRequest request) { }

    public Task<TResponse> HandleAsync<TRequest, TResponse>(TRequest request) { }
}
```

```cs
public interface IHandler<TRequest>
{
    Task HandleAsync(TRequest request);
}

public interface IHandler<TRequest, TResponse>
{
    Task<TResponse> HandleAsync(TRequest request);
}
```
