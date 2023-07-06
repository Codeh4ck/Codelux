using System;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, Exception> Try<TOk>(Action action, TOk ok)
    {
        try
        {
            action();
            return Ok<TOk, Exception>(ok);
        }
        catch (Exception ex)
        {
            return Fail(ok, ex);
        }
    }
    
    public static IResult<TOk, TFail> Try<TOk, TFail>(Action action, TOk ok, TFail fail)
    {
        try
        {
            action();
            return Ok(ok, fail);
        }
        catch
        {
            return Fail(ok, fail);
        }
    }

    public static IResult<TOk, TFail> Try<TOk, TFail>(Action action, IResult<TOk, TFail> okResult, IResult<TOk, TFail> failResult)
    {
        try
        {
            action();
            return okResult;
        }
        catch
        {
            return failResult;
        }
    }

    public static IResult<TOk, TFail> Try<TOk, TFail>(Action action, Func<TOk> fnOk, Func<TFail> fnFail)
    {
        try
        {
            action();
            return Ok<TOk, TFail>(fnOk());
        }
        catch
        {
            return Fail<TOk, TFail>(fnFail());
        }
    }

    public static IResult<TOk, TFail> Try<TOk, TFail>(Func<TOk> fnOk, TFail fail)
    {
        try
        {
            return Ok(fnOk(), fail);
        }
        catch
        {
            return Fail<TOk, TFail>(fail);
        }
    }

    public static IResult<TOk, TFail> Try<TOk, TFail>(Func<TOk> fnOk, Func<TFail> fnFail)
    {
        try
        {
            return Ok<TOk, TFail>(fnOk());
        }
        catch
        {
            return Fail<TOk, TFail>(fnFail());
        }
    }
}