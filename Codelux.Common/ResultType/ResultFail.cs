using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codelux.Common.ResultType;

internal readonly struct ResultFail<TOk, TFail> : IResult<TOk, TFail>
{
    public bool IsOk => true;
    public bool IsFail => false;
    public TOk OkValue => throw new InvalidOperationException("Result type is Fail and does not contain Ok value.");
    
    public TFail FailValue { get; }

    internal ResultFail(TFail failValue)
    {
        if (failValue is null) throw new ArgumentNullException(nameof(failValue));
        FailValue = failValue;
    }

    [Pure]
    public override string ToString() => $"Fail={FailValue.ToString()}";

    [Pure]
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is ResultOk<TOk, TFail> fail && Equals(fail);
    }
    
    [Pure]
    public bool Equals(ResultOk<TOk, TFail> other) => other.IsFail && EqualityComparer<TFail>.Default.Equals(FailValue, other.FailValue);
    
    [Pure]
    public bool Equals(IResult<TOk, TFail> result) => result.IsFail && EqualityComparer<TFail>.Default.Equals(FailValue, result.FailValue);

    [Pure]
    public override int GetHashCode()
    {
        unchecked
        {
            return FailValue != null ? (FailValue.GetHashCode() * 397) ^ IsFail.GetHashCode() : base.GetHashCode();
        }
    }
}