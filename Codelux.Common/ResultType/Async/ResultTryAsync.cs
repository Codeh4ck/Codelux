using System;
using System.Threading.Tasks;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static Task<IResult<TOk, Exception>> TryAsync<TOk>(Action action, TOk ok)
    {
        try
        {
            action();
            return Task.FromResult(Ok<TOk, Exception>(ok));
        }
        catch (Exception exn)
        {
            return Task.FromResult(Fail(ok, exn));
        }
    }

    public static async Task<IResult<TOk, TFail>> TryAsync<TOk, TFail>(Func<Task<TOk>> fnOk, TFail fail)
    {
        try
        {
            return Ok(await fnOk(), fail);
        }
        catch
        {
            return Fail<TOk, TFail>(fail);
        }
    }

    public static async Task<IResult<TOk, TFail>> TryAsync<TOk, TFail>(Func<Task<TOk>> fnOk, Func<Task<TFail>> fnFail)
    {
        try
        {
            return Ok<TOk, TFail>(await fnOk());
        }
        catch
        {
            return Fail<TOk, TFail>(await fnFail());
        }
    }

    public static Task<IResult<TOk, TFail>> TryAsync<TOk, TFail>(Action action, TOk ok, TFail fail)
    {
        try
        {
            action();
            return Task.FromResult(Ok(ok, fail));
        }
        catch
        {
            return Task.FromResult(Fail(ok, fail));
        }
    }

    public static async Task<IResult<TOk, TFail>> TryAsync<TOk, TFail>(Action action, Func<Task<TOk>> fnOk,
        Func<Task<TFail>> fnFail)
    {
        try
        {
            action();
            return Ok<TOk, TFail>(await fnOk());
        }
        catch
        {
            return Fail<TOk, TFail>(await fnFail());
        }
    }
}