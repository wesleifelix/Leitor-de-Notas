namespace LeitorDeNotas.ClearArch.Commons;

public sealed class OperationResult<T>
{
    public bool Success { get; init; }
    public T? Data { get; init; }
    public string? Message { get; init; }

    public static OperationResult<T> Ok(T data, string? message = null) => new() { Success = true, Data = data, Message = message };
    public static OperationResult<T> Fail(string message) => new() { Success = false, Message = message };
}
