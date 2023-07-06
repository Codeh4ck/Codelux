using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Codelux.Common.ResultType;

internal readonly struct ResultOk<TOk, TFail> : IResult<TOk, TFail>
{
    public bool IsOk => true;
    public bool IsFail => false;
    public TFail FailValue => throw new InvalidOperationException("Result type is Ok and does not contain Fail value.");

    public TOk OkValue { get; }
    
    internal ResultOk(TOk okValue)
    {
        if (okValue is null) throw new ArgumentNullException(nameof(okValue));
        OkValue = okValue;
    }

    [Pure]
    public override string ToString() => $"Ok={OkValue.ToString()}";

    [Pure]
    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        return obj is ResultOk<TOk, TFail> ok && Equals(ok);
    }
    
    [Pure]
    public bool Equals(ResultOk<TOk, TFail> other) => other.IsOk && EqualityComparer<TOk>.Default.Equals(OkValue, other.OkValue);
    
    [Pure]
    public bool Equals(IResult<TOk, TFail> result) => result.IsOk && EqualityComparer<TOk>.Default.Equals(OkValue, result.OkValue);

    [Pure]
    public override int GetHashCode()
    {
        unchecked
        {
            return OkValue != null ? (OkValue.GetHashCode() * 397) ^ IsOk.GetHashCode() : base.GetHashCode();
        }
    }
}