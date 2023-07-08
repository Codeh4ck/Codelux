using System;
using System.Linq;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, TFail> Combine<TOk, TFail>(params IResult<TOk, TFail>[] results)
    {
        if (results == null || results.Length == 0)
            throw new ArgumentException(nameof(results));

        foreach (IResult<TOk, TFail> result in results)
        {
            if (result.IsFail)
                return result;
        }

        return results.Last();
    }

    public static IResult<TOk, TFail> Combine<TOk, TFail>(IEnumerable<IResult<TOk, TFail>> results)
    {
        IResult<TOk, TFail>[] resultArray = results?.ToArray();
        return Combine(resultArray);
    }

    public static IResult<TOk, TFail> Combine<TOk, TFail>(Func<TOk, TOk, TOk> combineFn, params IResult<TOk, TFail>[] results)
    {
        if (results == null || results.Length < 2)
            throw new ArgumentException(nameof(results));

        IResult<TOk, TFail> firstResult = results[0];
        
        for (int n = 1; n < results.Length; n++)
        {
            IResult<TOk, TFail> result = results[n];
            
            if (result.IsFail)
                return result;

            firstResult = Ok<TOk, TFail>(combineFn(firstResult.OkValue, result.OkValue));
        }

        return firstResult;
    }

    public static IResult<TOk, TFail> Combine<TOk, TFail>(Func<TOk, TOk, TOk> combineFn, IEnumerable<IResult<TOk, TFail>> results)
    {
        IResult<TOk, TFail>[] resultArray = results.ToArray();
        return Combine(combineFn, resultArray);
    }
}