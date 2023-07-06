using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static async Task<IResult<TOk, TFail>> CombineAsync<TOk, TFail>(params Task<IResult<TOk, TFail>>[] results)
    {
        if (results == null || results.Length == 0)
            return Fail<TOk, TFail>(default);

        IResult<TOk, TFail> result = null;
        
        foreach (Task<IResult<TOk, TFail>> resultTask in results)
        {
            result = await resultTask;
            
            if (result.IsFail)
                return result;
        }

        return result;
    }

    public static Task<IResult<TOk, TFail>> CombineAsync<TOk, TFail>(IEnumerable<Task<IResult<TOk, TFail>>> results)
    {
        Task<IResult<TOk, TFail>>[] resultArray = results?.ToArray();
        return CombineAsync(resultArray);
    }

    public static async Task<IResult<TOk, TFail>> CombineAsync<TOk, TFail>(Func<TOk, TOk, Task<TOk>> combineFn,
        params IResult<TOk, TFail>[] results)
    {
        if (results == null || results.Length == 0)
            return Fail<TOk, TFail>(default);

        IResult<TOk, TFail> firstResult = results[0];
        
        if (results.Length == 1)
            return firstResult;

        for (int n = 1; n < results.Length; n++)
        {
            IResult<TOk, TFail> result = results[n];
            if (result.IsFail)
                return result;

            firstResult = Ok<TOk, TFail>(await combineFn(firstResult.OkValue, result.OkValue));
        }

        return firstResult;
    }

    public static Task<IResult<TOk, TFail>> CombineAsync<TOk, TFail>(Func<TOk, TOk, Task<TOk>> combineFn,
        IEnumerable<IResult<TOk, TFail>> results)
    {
        IResult<TOk, TFail>[] resultArray = results?.ToArray();
        return CombineAsync(combineFn, resultArray);
    }
}