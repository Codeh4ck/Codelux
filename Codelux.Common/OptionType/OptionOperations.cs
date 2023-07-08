using System;
using System.Threading.Tasks;

namespace Codelux.Common.OptionType;

public static partial class Option
{
    public static Option<T> Try<T>(Func<T> f)
    {
        try
        {
            return From(f());
        }
        catch
        {
            return None<T>();
        }
    }
    
    public static Option<TZ> Combine<TX, TY, TZ>(Option<TX> optionX, Option<TY> optionY,
        Func<TX, TY, TZ> combiningFn)
        => (optionX.IsNone || optionY.IsNone)
            ? Option.None<TZ>()
            : Option.From(combiningFn(optionX.Value, optionY.Value));

    public static Func<Option<T>, Option<T>> Compose<T>(params Func<Option<T>, Option<T>>[] functionList)
    {
        if (functionList.Length == 0)
            throw new InvalidOperationException("No function params given");

        return
            option =>
            {
                Option<T> result = option;
                foreach (Func<Option<T>, Option<T>> fn in functionList)
                {
                    if (result.IsNone)
                        return result;
                    
                    result = fn(result);
                }

                return result;
            };
    }

    public static Func<Option<T>, Task<Option<T>>> ComposeAsync<T>(params Func<Option<T>, Task<Option<T>>>[] functionList)
    {
        if (functionList.Length == 0)
            throw new InvalidOperationException("No function params given");

        return
            async option =>
            {
                Option<T> result = option;
                foreach (Func<Option<T>, Task<Option<T>>> fn in functionList)
                {
                    if (result.IsNone)
                        return result;
                    
                    result = await fn(result);
                }

                return result;
            };
    }
}