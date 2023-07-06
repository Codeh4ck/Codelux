namespace Codelux.Common.ResultType;

public interface IResult<TOk, TFail>
{
    bool IsOk { get; }
    bool IsFail { get; }
    TOk OkValue { get; }
    TFail FailValue { get; }
    bool Equals(IResult<TOk, TFail> other);
}