namespace Codelux.Common.ResultType;

public interface IResult
{
    bool IsOk { get; }
    bool IsFail { get; }
}

public interface IResult<TOk, TFail> : IResult
{
    TOk OkValue { get; }
    TFail FailValue { get; }
    bool Equals(IResult<TOk, TFail> other);
}