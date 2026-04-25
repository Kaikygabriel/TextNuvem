namespace TextNuvem.Commum;

public class Result
{
    private Result()
    {
        IsSuccess = true;
    }
    private Result(string error)
    {
        Error = error;
        IsSuccess = false;
    }

    public string Error { get; private init;}
    public bool IsSuccess { get; private init; }

    public static Result Success() => new ();
    public static Result Failure(string messageError) => new (messageError);
}