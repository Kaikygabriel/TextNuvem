using TextNuvem.Domain.BackOffice.Commum;

namespace TextNuvem.Domain.BackOffice.Abstraction;

public sealed record Result
{
    private Result()
    {
        IsSuccess = true;
    }
    private Result(Error error)
    {
        Error = error;
        IsSuccess = false;
    }

    public bool IsSuccess { get; private init; }
    public Error Error { get; private init; }

    public static Result Failure(Error error) => new(error);
    public static Result Success() => new();

    public static implicit operator Result(Error error)
        => Failure(error);
}