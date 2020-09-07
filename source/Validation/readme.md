# DotNetCore.Validation

## Extensions

```cs
public static class Extensions
{
    public static async Task<IResult> ValidationAsync<T>(this IValidator<T> validator, T instance) { }
}
```
