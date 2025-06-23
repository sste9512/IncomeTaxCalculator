namespace IncomeTaxCalculator.Server.Application.Common.Models;

public sealed class Result<T>
{
    public bool IsSuccess { get; init; }
    public bool IsFailure => !IsSuccess;
    public T? Value { get; init; }
    public string? Error { get; init; }

    private Result(bool isSuccess, T? value, string? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T value) => new(true, value, null);
    public static Result<T> Failure(string error) => new(false, default, error);

    public TResult Match<TResult>(
        Func<T, TResult> onSuccess,
        Func<string, TResult> onFailure)
    {
        return IsSuccess && Value is not null 
            ? onSuccess(Value) 
            : onFailure(Error ?? "Unknown error");
    }
} 