namespace ScalpLedger.Application.Common.Models;

public class BaseResult
{
    public bool IsSuccess { get; private set; }
    public string? Message { get; }
    public List<Error>? Errors { get; private set; }

    public BaseResult(string? message)
    {
        IsSuccess = true;
        Message = message;
    }

    public BaseResult(string code, string message)
    {
        IsSuccess = false;
        Message = message;
        Errors = [new(code, message)];
    }

    public void AddError(string code, string message)
    {
        if (IsSuccess)
            IsSuccess = false;

        Errors ??= [];

        Errors.Add(new Error(code, message));
    }
}

public class BaseResult<T> : BaseResult
{
    public T? Data { get; }

    public BaseResult(T data, string? message = null) : base(message)
    {
        Data = data;
    }

    public BaseResult(string code, string message) : base(code, message)
    {
        Data = default;
    }

    public static implicit operator BaseResult<T>(T result)
    {
        return new(result);
    }
}

public class Error
{
    public string Code { get; }
    public string Message { get; }
    public string? Field { get; }

    public Error(string code, string message, string? field = null)
    {
        Code = code;
        Message = message;
        Field = field;
    }
}