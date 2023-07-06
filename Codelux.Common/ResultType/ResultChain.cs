using System;
using System.Linq;
using System.Collections.Generic;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<IEnumerable<TOk>, IEnumerable<TFail>> Chain<TOk, TFail>(params Func<IResult<TOk, TFail>>[] resultFns)
    {
        if (resultFns == null || resultFns.Length == 0)
            throw new ArgumentException(nameof(resultFns));

        List<TOk> listOk = new();
        List<TFail> listFail = new();

        foreach (Func<IResult<TOk, TFail>> resultFn in resultFns)
        {
            IResult<TOk, TFail> result = resultFn();

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

    public static IResult<IEnumerable<TOk>, IEnumerable<TFail>> Chain<TOk, TFail>(
        IEnumerable<Func<IResult<TOk, TFail>>> resultFNs)
    {
        Func<IResult<TOk, TFail>>[] resultArray = resultFNs?.ToArray();
        return Chain(resultArray);
    }
}