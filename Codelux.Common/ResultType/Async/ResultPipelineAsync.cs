using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static Task<IResult<TOk, TFail>> PipelineAsync<TOk, TFail>(TOk value, IEnumerable<Func<TOk, Task<IResult<TOk, TFail>>>> resultFns)
    {
        Func<TOk, Task<IResult<TOk, TFail>>>[] resultFnArray = resultFns?.ToArray();
        return PipelineAsync(value, resultFnArray);
    }

    public static async Task<IResult<TOk, TFail>> PipelineAsync<TOk, TFail>(TOk value, params Func<TOk, Task<IResult<TOk, TFail>>>[] resultFns)
    {
        IResult<TOk, TFail> result = Ok<TOk, TFail>(value);
        if (resultFns == null || resultFns.Length == 0)
            return result;

        TOk currentValue = value;
        
        foreach (Func<TOk, Task<IResult<TOk, TFail>>> resultFn in resultFns)
        {
            result = await resultFn(currentValue);
            
            if (result.IsFail)
                return result;

            currentValue = result.OkValue;
        }

        return result;
    }

    public static Func<TOk, Task<IResult<TOk, TFail>>> CreateAsyncPipeline<TOk, TFail>(params Func<TOk, Task<IResult<TOk, TFail>>>[] resultFNs) 
        => value => PipelineAsync(value, resultFNs);

    public static Func<TOk, Task<IResult<TOk, TFail>>> CreateAsyncPipeline<TOk, TFail>(IEnumerable<Func<TOk, Task<IResult<TOk, TFail>>>> resultFNs)
        => value => PipelineAsync(value, resultFNs);
}