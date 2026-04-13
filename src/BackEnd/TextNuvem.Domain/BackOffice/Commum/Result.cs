namespace TextNuvem.Domain.BackOffice.Commum;

public record Result<T>
{
    private Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }
    private Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public bool IsSuccess { get; private init; }
    public T Value { get; private init; }
    public Error Error { get; private init; }

    public static Result<T> Failure(Error error) => new(error);
    public static Result<T> Success(T value) => new(value);

    public static implicit operator Result<T>(Error error)
        => Failure(error);
    
    public static implicit operator Result<T>(T value)
        => Success(value);
}