using System;
using System.Threading.Tasks;
using Codelux.Common.OptionType;

namespace Codelux.Common.Extensions;

public static class OptionExtensions
{
      public static Option<TY> Bind<TX, TY>(this Option<TX> option, Func<TX, Option<TY>> fn)
            => option.HasValue ? fn(option.Value) : Option.None<TY>();

        public static Option<TY> Map<TX, TY>(this Option<TX> option, Func<TX, TY> fn)
            => option.HasValue ? Option.From(fn(option.Value)) : Option.None<TY>();

        public static Option<TX> MapNone<TX>(this Option<TX> option, Func<TX> fn)
            => option.IsNone ? Option.From(fn()) : option;

        public static Option<TY> Bimap<TX, TY>(this Option<TX> option, Func<TX, TY> fnSome, Func<TY> fnNone)
            => Option.From(option.HasValue ? fnSome(option.Value) : fnNone());

        public static Option<T> Return<T>(T value)
            => Option.Some(value);

        public static Option<TX> Join<TX>(this Option<Option<TX>> maybeOption)
            => maybeOption.HasValue ? maybeOption.Value : Option.None<TX>();

        public static async Task<Option<TX>> Join<TX>(this Task<Option<Option<TX>>> maybeOptionTask)
        {
            Option<Option<TX>> maybeOption = await maybeOptionTask;
            return maybeOption.HasValue ? maybeOption.Value : Option.None<TX>();
        }

        public static async Task<Option<TX>> Join<TX>(this Option<Task<Option<TX>>> maybeOptionTask) 
            => maybeOptionTask.HasValue ? await maybeOptionTask.Value : Option.None<TX>();

        public static Option<T> Apply<T>(this Option<T> option, Option<Func<T, T>> maybeFn)
            => option.HasValue && maybeFn.HasValue ? Option.From(maybeFn.Value(option.Value)) : Option.None<T>();

        public static void Handle<TX>(this Option<TX> option, Action<TX> actionSome, Action actionNone)
        {
            if (option.HasValue)
                actionSome(option.Value);
            else
                actionNone();
        }

        public static async Task<Option<TY>> BindAsync<TX, TY>(this Task<Option<TX>> optionTask, Func<TX, Option<TY>> fn)
        {
            Option<TX> option = await optionTask;
            return option.HasValue ? fn(option.Value) : Option.None<TY>();
        }

        public static async Task<Option<TY>> MapAsync<TX, TY>(this Task<Option<TX>> optionTask, Func<TX, TY> fn)
        {
            Option<TX> option = await optionTask;
            return option.HasValue ? Option.From(fn(option.Value)) : Option.None<TY>();
        }

        public static async Task<Option<TX>> MapNoneAsync<TX>(this Task<Option<TX>> optionTask, Func<TX> fn)
        {
            Option<TX> option = await optionTask;
            return option.IsNone ? Option.From(fn()) : option;
        }

        public static async Task<Option<TY>> MapAllAsync<TX, TY>(this Task<Option<TX>> optionTask, Func<TX, TY> fnSome, Func<TY> fnNone)
        {
            Option<TX> option = await optionTask;
            return Option.From(option.HasValue ? fnSome(option.Value) : fnNone());
        }

        public static Task<Option<T>> ReturnAsync<T>(T value) => Task.FromResult(Option.Some(value));

        public static async Task HandleAsync<TX>(this Task<Option<TX>> optionTask, Action<TX> actionSome, Action actionNone)
        {
            Option<TX> option = await optionTask;
            
            if (option.HasValue)
                actionSome(option.Value);
            else
                actionNone();
        }

        public static async Task<Option<TY>> BindAsync<TX, TY>(this Task<Option<TX>> optionTask, Func<TX, Task<Option<TY>>> fn)
        {
            Option<TX> option = await optionTask;
            return option.HasValue ? await fn(option.Value) : Option.None<TY>();
        }

        public static async Task<Option<TY>> MapAsync<TX, TY>(this Task<Option<TX>> optionTask, Func<TX, Task<Option<TY>>> fn)
        {
            Option<TX> option = await optionTask;
            return option.HasValue ? await fn(option.Value) : Option.None<TY>();
        }

        public static async Task<Option<TY>> BimapAsync<TX, TY>(this Task<Option<TX>> optionTask,
            Func<TX, Task<Option<TY>>> fnSome, Func<Task<Option<TY>>> fnNone)
        {
            Option<TX> option = await optionTask;
            
            return await (option.HasValue
                ? fnSome(option.Value)
                : fnNone());
        }

        public static async Task<Option<TX>> MapNoneAsync<TX>(this Task<Option<TX>> optionTask, Func<Task<TX>> fn)
        {
            Option<TX> option = await optionTask;
            return option.IsNone ? Option.From(await fn()) : option;
        }

        public static async Task HandleAsync<TX>(this Task<Option<TX>> optionTask, Func<TX, Task> fnSome, Func<Task> fnNone)
        {
            Option<TX> option = await optionTask;
            
            if (option.HasValue)
                await fnSome(option.Value);
            else
                await fnNone();
        }

        public static async Task<Option<T>> ApplyAsync<T>(this Task<Option<T>> optionTask, Option<Func<T, T>> maybeFn)
        {
            Option<T> option = await optionTask;
            return option.HasValue && maybeFn.HasValue ? Option.From(maybeFn.Value(option.Value)) : Option.None<T>();
        }

        public static TX? ValueOrNull<TX>(this Option<TX> option) where TX : struct => option.HasValue ? option.Value : null;

        public static Option<T> WhenSome<T>(this Option<T> option, Action<T> action)
        {
            if (option.HasValue)
                action(option.Value);

            return option;
        }

        public static Option<T> WhenNone<T>(this Option<T> option, Action action)
        {
            if (option.IsNone)
                action();

            return option;
        }

        public static async Task<Option<T>> WhenSome<T>(this Option<T> option, Func<T, Task> action)
        {
            if (option.HasValue)
                await action(option.Value);

            return option;
        }

        public static async Task<Option<T>> WhenNone<T>(this Option<T> option, Func<Task> action)
        {
            if (option.IsNone)
                await action();

            return option;
        }

        public static Option<T> WhenAny<T>(this Option<T> option, Action<Option<T>> action)
        {
            action(option);
            return option;
        }

        public static async Task<Option<T>> WhenSomeAsync<T>(this Task<Option<T>> optionTask, Func<T, Task> action)
        {
            Option<T> option = await optionTask;
            
            if (option.HasValue)
                await action(option.Value);

            return option;
        }

        public static async Task<Option<T>> WhenNoneAsync<T>(this Task<Option<T>> optionTask, Func<Task> action)
        {
            Option<T> option = await optionTask;
            if (option.IsNone)
                await action();

            return option;
        }

        public static async Task<Option<T>> WhenAnyAsync<T>(this Task<Option<T>> optionTask, Action<Option<T>> action)
        {
            Option<T> option = await optionTask;
            action(option);

            return option;
        }

        public static Option<T> OnSome<T>(this Option<T> option, Func<T, Option<T>> fn) => option.HasValue ? fn(option.Value) : option;

        public static Option<T> OnNone<T>(this Option<T> option, Func<Option<T>> fn) => option.IsNone ? fn() : option;

        public static Option<T> OnAny<T>(this Option<T> option, Func<Option<T>> fn) => fn();

        public static Task<Option<T>> OnSome<T>(this Option<T> option, Func<T, Task<Option<T>>> fn) 
            => option.HasValue ? fn(option.Value) : Task.FromResult(option);

        public static Task<Option<T>> OnNone<T>(this Option<T> option, Func<Task<Option<T>>> fn) 
            => option.IsNone ? fn() : Task.FromResult(option);

        public static Option<T> OnAny<T>(this Option<T> option, Func<Option<T>, Option<T>> fn) => fn(option);

        public static async Task<Option<T>> OnSomeAsync<T>(this Task<Option<T>> optionTask, Func<T, Option<T>> fn)
        {
            Option<T> option = await optionTask;
            return option.HasValue ? fn(option.Value) : option;
        }

        public static async Task<Option<T>> OnNoneAsync<T>(this Task<Option<T>> optionTask, Func<Option<T>> fn)
        {
            Option<T> option = await optionTask;
            return option.IsNone ? fn() : option;
        }

        public static async Task<Option<T>> OnSomeAsync<T>(this Task<Option<T>> optionTask, Func<T, Task<Option<T>>> fn)
        {
            Option<T> option = await optionTask;
            return option.HasValue ? await fn(option.Value) : option;
        }

        public static async Task<Option<T>> OnNoneAsync<T>(this Task<Option<T>> optionTask, Func<Task<Option<T>>> fn)
        {
            Option<T> option = await optionTask;
            return option.IsNone ? await fn() : option;
        }

        public static Task<Option<T>> OnAnyAsync<T>(this Task<Option<T>> optionTask, Func<Task<Option<T>>> fn) => fn();

        public static async Task<Option<T>> OnAnyAsync<T>(this Task<Option<T>> optionTask, Func<Option<T>, Option<T>> fn)
        {
            Option<T> option = await optionTask;
            return fn(option);
        }

        public static TU Substitute<T, TU>(this Option<T> option, TU some, TU none)
            => option.IsSome ? some : none;

        public static TU Substitute<T, TU>(this Option<T> option, Func<TU> someFn, Func<TU> noneFn)
            => option.IsSome ? someFn() : noneFn();
        
        public static TU Substitute<T, TU>(this Option<T> option, Func<T, TU> someFn, Func<TU> noneFn)
            => option.IsSome ? someFn(option.Value) : noneFn();

        public static async Task<TU> SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, TU some, TU none)
        {
            Option<T> option = await optionTask;
            return option.IsSome ? some : none;
        }

        public static async Task<TU> SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, Task<TU> someTask, Task<TU> noneTask)
        {
            Option<T> option = await optionTask;
            return option.IsSome ? await someTask : await noneTask;
        }

        public static async Task<TU> SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, Func<TU> someFn, Func<TU> noneFn)
        {
            Option<T> option = await optionTask;
            return option.IsSome
                ? someFn() 
                : noneFn();
        }

        public static async Task<TU>  SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, Func<T, TU> someFn, Func<TU> noneFn)
        {
            Option<T> option = await optionTask;
            return option.IsSome
                ? someFn(option.Value) 
                : noneFn();
        }

        public static async Task<TU> SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, Func<Task<TU>> someFnAsync, Func<Task<TU>> noneFnAsync)
        {
            Option<T> option = await optionTask;
            return option.IsSome
                ? await someFnAsync() 
                : await noneFnAsync();
        }

        public static async Task<TU> SubstituteAsync<T, TU>(this Task<Option<T>> optionTask, Func<T, Task<TU>> someFnAsync, Func<Task<TU>> noneFnAsync)
        {
            Option<T> option = await optionTask;
            return option.IsSome
                ? await someFnAsync(option.Value) 
                : await noneFnAsync();
        }

        public static Option<T> Ensure<T>(this Option<T> option, Predicate<T> predicate)
            => option.OnSome(value => predicate(value) ? option : Option.None<T>());

        public static Option<T> Ensure<T>(this Option<T> option, Predicate<T> predicate, Option<T> failOption)
            => option.OnSome(value =>  predicate( value) ? option : failOption);

        public static Option<T> Ensure<T>(this Option<T> option, Predicate<T> predicate, Func<Option<T>> failOptionFn)
            => option.OnSome(value =>  predicate( value) ? option : failOptionFn());

        public static Task<Option<T>> EnsureAsync<T>(this Task<Option<T>> optionTask, Predicate<T> predicate) 
            => optionTask.OnSomeAsync(value => predicate(value) ? optionTask : Task.FromResult(Option.None<T>()));
       
        public static Task<Option<T>> EnsureAsync<T>(this Task<Option<T>> optionTask, Predicate<T> predicate, Option<T> failOption)
            => optionTask.OnSomeAsync(value => predicate(value) ? optionTask : Task.FromResult(failOption));
        
       
        public static Task<Option<T>> EnsureAsync<T>(this Task<Option<T>> optionTask, Predicate<T> predicate, Func<Option<T>> failOptionFn)
            => optionTask.OnSomeAsync(value => predicate(value) ? optionTask : Task.FromResult(failOptionFn()));
}