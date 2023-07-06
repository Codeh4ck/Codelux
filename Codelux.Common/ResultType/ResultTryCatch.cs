using System;

namespace Codelux.Common.ResultType;

public static partial class Result
{
    public static IResult<TOk, TFail> TryCatch<TOk, TFail, TException>(Action action, TOk ok, TFail fail) where TException : Exception
    {
        try
        {
            action();
            return Ok(ok, fail);
        }
        catch (TException)
        {
            return Fail(ok, fail);
        }
    }

    public static IResult<TOk, TFail> TryCatch<TOk, TFail, TException>(Action action, Func<TOk> fnOk, Func<TFail> fnFail) where TException : Exception
    {
        try
        {
            action();
            return Ok<TOk, TFail>(fnOk());
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(fnFail());
        }
    }

    public static IResult<TOk, TFail> TryCatch<TOk, TFail, TException>(Func<TOk> fnOk, TFail fail) where TException : Exception
    {
        try
        {
            return Ok(fnOk(), fail);
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(fail);
        }
    }

    public static IResult<TOk, TFail> TryCatch<TOk, TFail, TException>(Func<TOk> fnOk, Func<TFail> fnFail) where TException : Exception
    {
        try
        {
            return Ok<TOk, TFail>(fnOk());
        }
        catch (TException)
        {
            return Fail<TOk, TFail>(fnFail());
        }
    }
}