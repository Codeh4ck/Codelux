namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, TFail> Ok<TOk, TFail>(TOk ok) => new ResultOk<TOk, TFail>(ok);
    public static IResult<TOk, TFail> Ok<TOk, TFail>(TOk ok, TFail _) => new ResultOk<TOk, TFail>(ok);
    public static IResult<TOk, TFail> Fail<TOk, TFail>(TFail fail) => new ResultFail<TOk, TFail>(fail);
    public static IResult<TOk, TFail> Fail<TOk, TFail>(TOk _, TFail fail) => new ResultFail<TOk, TFail>(fail);

    public static IResult<TOk, TFail> Clone<TOk, TFail>(IResult<TOk, TFail> result) =>
        result.IsOk ? Ok<TOk, TFail>(result.OkValue) : Fail<TOk, TFail>(result.FailValue);
}