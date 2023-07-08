using System;
using System.Linq;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, TFail> Pipeline<TOk, TFail>(TOk value, IEnumerable<Func<TOk, IResult<TOk, TFail>>> resultFNs)
    {
        Func<TOk, IResult<TOk, TFail>>[] resultFnArray = resultFNs?.ToArray();
        return Pipeline(value, resultFnArray);
    }

    public static IResult<TOk, TFail> Pipeline<TOk, TFail>(TOk value, params Func<TOk, IResult<TOk, TFail>>[] resultFNs)
    {
        IResult<TOk, TFail> result = Ok<TOk, TFail>(value);
        
        if (resultFNs == null || resultFNs.Length == 0)
            return result;

        TOk currentValue = value;
        
        foreach (Func<TOk, IResult<TOk, TFail>> resultFn in resultFNs)
        {
            result = resultFn(currentValue);
            if (result.IsFail)
                return result;

            currentValue = result.OkValue;
        }

        return result;
    }

    public static Func<TOk, IResult<TOk, TFail>> CreatePipeline<TOk, TFail>(
        params Func<TOk, IResult<TOk, TFail>>[] resultFNs)
        => value => Pipeline(value, resultFNs);

    public static Func<TOk, IResult<TOk, TFail>> CreatePipeline<TOk, TFail>(
        IEnumerable<Func<TOk, IResult<TOk, TFail>>> resultFNs)
        => value => Pipeline(value, resultFNs);
}