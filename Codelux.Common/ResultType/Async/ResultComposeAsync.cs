using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static Task<IResult<TOk, TFail>> ComposeAsync<TOk, TFail>(TOk value, params Func<TOk, Task<IResult<TOk, TFail>>>[] resultFns)
    {
        Array.Reverse(resultFns);
        return PipelineAsync(value, resultFns);
    }

    public static Task<IResult<TOk, TFail>> ComposeAsync<TOk, TFail>(TOk value, IEnumerable<Func<TOk, Task<IResult<TOk, TFail>>>> resultFns)
        => PipelineAsync(value, resultFns.Reverse());

    public static Func<TOk, Task<IResult<TOk, TFail>>> CreateAsyncComposition<TOk, TFail>(params Func<TOk, Task<IResult<TOk, TFail>>>[] resultFns)
    {
        Array.Reverse(resultFns);
        return value => PipelineAsync(value, resultFns);
    }

    public static Func<TOk, Task<IResult<TOk, TFail>>> CreateAsyncComposition<TOk, TFail>(
        IEnumerable<Func<TOk, Task<IResult<TOk, TFail>>>> resultFns) =>
        value => PipelineAsync(value, resultFns.Reverse());
}