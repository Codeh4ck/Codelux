using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static async Task<IResult<IEnumerable<TOk>, IEnumerable<TFail>>> ChainAsync<TOk, TFail>(
        params Func<Task<IResult<TOk, TFail>>>[] resultFns)
    {
        if (resultFns == null || resultFns.Length == 0)
            throw new ArgumentException(nameof(resultFns));

        List<TOk> listOk = new List<TOk>();
        List<TFail> listFail = new List<TFail>();

        List<Task<IResult<TOk, TFail>>> listTask = resultFns.Select(resultFn => resultFn()).ToList();

        await Task.WhenAll(listTask);
        foreach (Task<IResult<TOk, TFail>> resultTask in listTask)
        {
            IResult<TOk, TFail> result = resultTask.Result;
            if (result.IsOk)
            {
                if (result.OkValue != null)
                    listOk.Add(result.OkValue);
            }
            else
            {
                if (result.FailValue != null)
                    listFail.Add(result.FailValue);
            }
        }

        return listFail.Any()
            ? Fail<IEnumerable<TOk>, IEnumerable<TFail>>(listFail)
            : Ok<IEnumerable<TOk>, IEnumerable<TFail>>(listOk);
    }

    public static Task<IResult<IEnumerable<TOk>, IEnumerable<TFail>>> ChainAsync<TOk, TFail>(IEnumerable<Func<Task<IResult<TOk, TFail>>>> resultFNs)
    {
        Func<Task<IResult<TOk, TFail>>>[] resultArray = resultFNs?.ToArray();
        return ChainAsync(resultArray);
    }
}