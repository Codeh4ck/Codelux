using System;

namespace Codelux.Tests.ResultType;

public static class ResultExceptionHelper
{
    public const string InvalidOperationExceptionMessage = "An invalid operation exception was thrown.";
    public static void ThrowsInvalidOperationException() =>
        throw new InvalidOperationException(InvalidOperationExceptionMessage);

    public static void DoesNotThrowException()
    {
        // No op
        ((Action)(() => { }))();
    }
}