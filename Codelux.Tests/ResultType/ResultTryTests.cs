using System;
using NUnit.Framework;
using Codelux.Common.ResultType;

namespace Codelux.Tests.ResultType;

[TestFixture]
public class ResultTryTests
{
    [Test]
    public void GivenResultWhenITryAnOperationAndItDoesNotThrowThenResultOkIsReturnedAndOkTypeIsProvidedModel()
    {
        var result = Result.Try(ResultExceptionHelper.DoesNotThrowException, TestOkResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }

    [Test]
    public void
        GivenResultWhenITryAnOperationAndItDoesNotThrowThenResultOkIsReturnedAndOkTypeIsProvidedModelAndFailTypeIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.DoesNotThrowException, TestOkResult.Instance,
            TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }

    [Test]
    public void GivenResultWhenITryAnOperationAndItThrowsThenResultFailIsReturnedAndFailTypeIsException()
    {
        var result = Result.Try(ResultExceptionHelper.ThrowsInvalidOperationException, TestOkResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(typeof(InvalidOperationException), result.FailValue.GetType());
        Assert.AreEqual(ResultExceptionHelper.InvalidOperationExceptionMessage, result.FailValue.Message);
    }

    [Test]
    public void
        GivenResultWhenITryAnOperationAndItThrowsThenResultFailIsReturnedAndFailTypeIsExceptionAndOkTypeIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.ThrowsInvalidOperationException, TestOkResult.Instance,
            TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(TestFailResult.Instance.GetType(), result.FailValue.GetType());
    }

    [Test]
    public void GivenResultWhenITryAnOperationAndItDoesNotThrowThenOkResultIsReturnedAndFailResultIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.DoesNotThrowException, TestResults.OkResult,
            TestResults.FailResult);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(TestResults.OkResult.GetType(), result.GetType());
    }

    [Test]
    public void GivenResultWhenITryAnOperationAndItThrowsThenFailResultIsReturnedAndOkResultIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.ThrowsInvalidOperationException, TestResults.OkResult,
            TestResults.FailResult);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(TestResults.FailResult.GetType(), result.GetType());
    }

    [Test]
    public void GivenResultWhenITryAnOperationAndItDoesNotThrowThenOkResultIsReturnedFromFactoryAndFailResultIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.DoesNotThrowException, () => TestOkResult.Instance,
            () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(TestResults.OkResult.GetType(), result.GetType());
    }

    [Test]
    public void GivenResultWhenITryAnOperationAndItThrowsThenFailResultIsReturnedFromFactoryAndOkResultIsIgnored()
    {
        var result = Result.Try(ResultExceptionHelper.ThrowsInvalidOperationException, () => TestOkResult.Instance,
            () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(TestResults.FailResult.GetType(), result.GetType());
    }
}