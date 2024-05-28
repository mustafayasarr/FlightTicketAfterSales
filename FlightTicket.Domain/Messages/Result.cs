namespace FlightTicket.Domain.Messages;

public class Result
{
    public bool Success { get; set; }
    public string Error { get; set; }
    public string? Code { get; set; }
    public bool IsFailure => !Success;

    protected Result(bool success, string error, string? code = null)
    {
        if (success && error != string.Empty)
        {
            throw new InvalidOperationException();
        }
        if (!success && error == string.Empty)
        {
            throw new InvalidOperationException();
        }
        Success = success;
        Error = error;
        Code = code;
    }
    public static Result Fail(string message, string? code = null)
    {
        return new Result(false, message, code);
    }
    public static Result<T> Fail<T>(string message, string? code = null)
    {
        return new Result<T>(default, false, message, code);
    }
    public static Result Ok()
    {
        return new Result(true, string.Empty, string.Empty);
    }
    public static Result<T> Ok<T>(T value)
    {
        return new Result<T>(value, true, string.Empty, string.Empty);
    }
}

public class Result<T> : Result
{
    public T Value { get; set; }
    protected internal Result(T value, bool success, string error, string? code = null) : base(success, error, code)
    {
        Value = value;
    }
}
