using System;
using System.Linq;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, TFail> Compose<TOk, TFail>(TOk value, params Func<TOk, IResult<TOk, TFail>>[] resultFNs)
    {
        Array.Reverse(resultFNs);
        return Pipeline(value, resultFNs);
    }

    public static IResult<TOk, TFail> Compose<TOk, TFail>(TOk value, IEnumerable<Func<TOk, IResult<TOk, TFail>>> resultFNs) 
        => Pipeline(value, resultFNs.Reverse());

    public static Func<TOk, IResult<TOk, TFail>> CreateComposition<TOk, TFail>(params Func<TOk, IResult<TOk, TFail>>[] resultFNs)
    {
        Array.Reverse(resultFNs);
        return value => Pipeline(value, resultFNs);
    }

    public static Func<TOk, IResult<TOk, TFail>> CreateComposition<TOk, TFail>(IEnumerable<Func<TOk, IResult<TOk, TFail>>> resultFNs) 
        => value => Pipeline(value, resultFNs.Reverse());
}