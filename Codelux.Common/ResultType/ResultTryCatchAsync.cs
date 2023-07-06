using System;
using System.Threading.Tasks;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static Task<IResult<TOk, TFail>> TryCatchAsync<TOk, TFail, TException>(Action action, TOk ok, TFail fail)
        where TException : Exception
    {
        try
        {
            action();
            return Task.FromResult(Ok(ok, fail));
        }
        catch (TException)
        {
            return Task.FromResult(Fail(ok, fail));
        }
    }

    public static async Task<IResult<TOk, TFail>> TryCatchAsync<TOk, TFail, TException>(Action action,
        Func<Task<TOk>> fnOk, Func<Task<TFail>> fnFail) where TException : Exception
    {
        try
        {
            action();
            return Ok<TOk, TFail>(await fnOk());
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(await fnFail());
        }
    }

    public static async Task<IResult<TOk, TFail>> TryCatchAsync<TOk, TFail, TException>(Func<Task<TOk>> fnOk,
        TFail fail) where TException : Exception
    {
        try
        {
            return Ok(await fnOk(), fail);
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(fail);
        }
    }

    public static async Task<IResult<TOk, TFail>> TryCatchAsync<TOk, TFail, TException>(Func<Task<TOk>> fnOk,
        Func<Task<TFail>> fnFail) where TException : Exception
    {
        try
        {
            return Ok<TOk, TFail>(await fnOk());
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(await fnFail());
        }
    }
}