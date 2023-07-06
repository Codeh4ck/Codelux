using System;
using System.Collections.Generic;
using System.Linq;
using Codelux.Common.ResultType;

namespace Codelux.Common.Extensions;

public static class ResultExtensions
{
    public static IResult<TOk, TFail> ToResultOk<TOk, TFail>(this object objOk) => new ResultOk<TOk, TFail>((TOk)objOk);

    public static IResult<TOk, TFail> ToResultFail<TOk, TFail>(this object objFail) => new ResultFail<TOk, TFail>((TFail)objFail);

    public static IResult<TOk, TFail> WhenOk<TOk, TFail>(this IResult<TOk, TFail> result, Action action)
    {
        if (result.IsOk)
            action();

        return result;
    }

    public static IResult<TOk, TFail> WhenFail<TOk, TFail>(this IResult<TOk, TFail> result, Action action)
    {
        if (result.IsFail)
            action();

        return result;
    }

    public static IResult<TOk, TFail> WhenMatch<TOk, TFail>(this IResult<TOk, TFail> result, Action actionOk, Action actionFail)
    {
        if (result.IsOk)
            actionOk();
        else
            actionFail();

        return result;
    }

    public static IResult<TOk, TFail> WhenOk<TOk, TFail>(this IResult<TOk, TFail> result, Action<TOk> action)
    {
        if (result.IsOk)
            action(result.OkValue);

        return result;
    }

    public static IResult<TOk, TFail> WhenFail<TOk, TFail>(this IResult<TOk, TFail> result, Action<TFail> action)
    {
        if (result.IsFail)
            action(result.FailValue);

        return result;
    }

    public static IResult<TOk, TFail> WhenMatch<TOk, TFail>(this IResult<TOk, TFail> result, Action<TOk> actionOk, Action<TFail> actionFail)
    {
        if (result.IsOk)
            actionOk(result.OkValue);
        else
            actionFail(result.FailValue);

        return result;
    }

    public static IResult<TOk, TFail> WhenOk<TOk, TFail>(this IResult<TOk, TFail> result, Action<IResult<TOk, TFail>> action)
    {
        if (result.IsOk)
            action(result);

        return result;
    }

    public static IResult<TOk, TFail> WhenFail<TOk, TFail>(this IResult<TOk, TFail> result, Action<IResult<TOk, TFail>> action)
    {
        if (result.IsFail)
            action(result);

        return result;
    }

    public static IResult<TOk, TFail> WhenMatch<TOk, TFail>(this IResult<TOk, TFail> result,
        Action<IResult<TOk, TFail>> actionOk, Action<IResult<TOk, TFail>> actionFail)
    {
        if (result.IsOk)
            actionOk(result);
        else
            actionFail(result);

        return result;
    }

    public static IResult<TOk, TFail> OnOk<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>> fn) 
        => result.IsOk ? fn() : result;

    public static IResult<TOk, TFail> OnFail<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>> fn) 
        => result.IsFail ? fn() : result;

    public static IResult<TOk, TFail> OnAny<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>> fn) 
        => fn();

    public static IResult<TOk, TFail> OnOk<TOk, TFail>(this IResult<TOk, TFail> result, Func<TOk, IResult<TOk, TFail>> fn) 
        => result.IsOk ? fn(result.OkValue) : result;

    public static IResult<TOk, TFail> OnFail<TOk, TFail>(this IResult<TOk, TFail> result, Func<TFail, IResult<TOk, TFail>> fn) 
        => result.IsFail ? fn(result.FailValue) : result;

    public static IResult<TOk, TFail> OnOk<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>, IResult<TOk, TFail>> fn) 
        => result.IsOk ? fn(result) : result;

    public static IResult<TOk, TFail> OnFail<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>, IResult<TOk, TFail>> fn) 
        => result.IsFail ? fn(result) : result;

    public static IResult<TOk, TFail> OnAny<TOk, TFail>(this IResult<TOk, TFail> result, Func<IResult<TOk, TFail>, IResult<TOk, TFail>> fn)
        => fn(result);

    public static IResult<TOk, TFail> Join<TOk, TFail>(this IResult<IResult<TOk, TFail>, TFail> result)
    {
        if (result.IsFail)
            return Result.Fail<TOk, TFail>(result.FailValue);

        IResult<TOk, TFail> inner = result.OkValue;
        
        return inner.IsOk
            ? Result.Ok<TOk, TFail>(inner.OkValue)
            : Result.Fail<TOk, TFail>(inner.FailValue);
    }

    public static void Handle<TOk, TFail>(this IResult<TOk, TFail> result, Action actionOk, Action actionFail)
    {
        if (result.IsOk)
            actionOk();
        else
            actionFail();
    }

    public static IResult<TOk2, TFail2> Either<TOk, TFail, TOk2, TFail2>(
        this IResult<TOk, TFail> result,
        Func<IResult<TOk, TFail>, IResult<TOk2, TFail2>> fnOk,
        Func<IResult<TOk, TFail>, IResult<TOk2, TFail2>> fnFail) 
        => result.IsOk ? fnOk(result) : fnFail(result);

    public static IResult<TOkNew, TFail> Bind<TOk, TFail, TOkNew>(this IResult<TOk, TFail> result, Func<TOk, IResult<TOkNew, TFail>> fn) 
        => result.IsFail ? Result.Fail<TOkNew, TFail>(result.FailValue) : fn(result.OkValue);

    public static IResult<TOkNew, TFail> Map<TOk, TFail, TOkNew>(this IResult<TOk, TFail> result, Func<TOk, TOkNew> fn)
    {
        return result.IsFail
            ? Result.Fail<TOkNew, TFail>(result.FailValue)
            : Result.Ok<TOkNew, TFail>(fn(result.OkValue));
    }

    public static IResult<TOk, TFailNew> MapFail<TOk, TFail, TFailNew>(this IResult<TOk, TFail> result, Func<TFail, TFailNew> fnFail)
        => result.IsFail
            ? Result.Fail<TOk, TFailNew>(fnFail(result.FailValue))
            : Result.Ok<TOk, TFailNew>(result.OkValue);

    public static IResult<TOkNew, TFailNew> MapAll<TOk, TFail, TOkNew, TFailNew>(this IResult<TOk, TFail> result,
        Func<TOk, TOkNew> fnOk, Func<TFail, TFailNew> fnFail) 
        => result.IsFail
            ? Result.Fail<TOkNew, TFailNew>(fnFail(result.FailValue))
            : Result.Ok<TOkNew, TFailNew>(fnOk(result.OkValue));

    public static IResult<TOkNew, TFailNew> ApplyAll<TOk, TFail, TOkNew, TFailNew>(this IResult<TOk, TFail> result,
        Func<TOk, IResult<TOkNew, TFailNew>> fnOk, Func<TFail, IResult<TOkNew, TFailNew>> fnFail) 
        => result.IsFail ? fnFail(result.FailValue) : fnOk(result.OkValue);

    public static IResult<IEnumerable<TOkNew>, TFail> MapEnumerable<TOk, TFail, TOkNew>(
        this IResult<IEnumerable<TOk>, TFail> result, Func<TOk, TOkNew> fn)
    {
        if (result.IsFail)
            return Result.Fail<IEnumerable<TOkNew>, TFail>(result.FailValue);

        if (result.OkValue == null)
            throw new ArgumentException($"Enumerator in OkValue cannot be null");

        List<TOkNew> list = result.OkValue.Select(fn).ToList();

        return Result.Ok<IEnumerable<TOkNew>, TFail>(list.AsReadOnly());
    }

    public static TReturn Substitute<TOk, TFail, TReturn>(this IResult<TOk, TFail> result, TReturn ok, TReturn fail)
        => (result.IsOk) ? ok : fail;

    public static TReturn Substitute<TOk, TFail, TReturn>(this IResult<TOk, TFail> result, Func<TReturn> okFn,
        Func<TReturn> failFn)
        => (result.IsOk) ? okFn() : failFn();

    public static TReturn Substitute<TOk, TFail, TReturn>(this IResult<TOk, TFail> result, Func<TOk, TReturn> okFn,
        Func<TFail, TReturn> failFn)
        => (result.IsOk) ? okFn(result.OkValue) : failFn(result.FailValue);

    public static IResult<TOk, TFail> Ensure<TOk, TFail>(this IResult<TOk, TFail> result,
        Predicate<TOk> predicate,
        TFail fail)
        => result.OnOk(value => predicate(value)
            ? result
            : Result.Fail<TOk, TFail>(fail));

    public static IResult<TOk, TFail> Ensure<TOk, TFail>(this IResult<TOk, TFail> result,
        Predicate<TOk> predicate,
        IResult<TOk, TFail> resultfail)
        => result.OnOk(value => predicate(value)
            ? result
            : resultfail);

    public static IResult<TOk, TFail> Ensure<TOk, TFail>(this IResult<TOk, TFail> result,
        Predicate<TOk> predicate,
        Func<IResult<TOk, TFail>, TFail> failFn)
        => result.OnOk(value => predicate(value)
            ? result
            : Result.Fail<TOk, TFail>(failFn(result)));

    public static IResult<TOk, TFail> Ensure<TOk, TFail>(this IResult<TOk, TFail> result,
        Predicate<TOk> predicate,
        Func<IResult<TOk, TFail>, IResult<TOk, TFail>> resultfailFn)
        => result.OnOk(value => predicate(value)
            ? result
            : resultfailFn(result));
}