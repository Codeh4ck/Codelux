using System;
using NUnit.Framework;
using Codelux.Common.ResultType;

namespace Codelux.Tests.ResultType;

[TestFixture]
public class ResultTryCatchTests
{
    [Test]
    public void GivenResultWhenITryCatchAnOperationAndItDoesNotThrowThenResultOkIsReturnedAndOkTypeIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, Exception>(
            ResultExceptionHelper.DoesNotThrowException,
            TestOkResult.Instance, TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }
    
    [Test]
    public void GivenResultWhenITryCatchAnOperationAndItThrowsThenResultFailIsReturnedAndFailIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, InvalidOperationException>(
            ResultExceptionHelper.ThrowsInvalidOperationException,
            TestOkResult.Instance, TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(typeof(TestFailResult),result.FailValue.GetType());
        Assert.AreEqual(TestFailResult.Instance.Message, result.FailValue.Message);
    }
    
    [Test]
    public void GivenResultWhenITryCatchAnOperationAndItDoesNotThrowThenResultOkIsReturnedFromFactoryAndOkTypeIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, Exception>(
            ResultExceptionHelper.DoesNotThrowException,
            () => TestOkResult.Instance, () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }
    
    [Test]
    public void GivenResultWhenITryCatchAnOperationAndItThrowsThenResultFailIsReturnedFromFactoryAndFailIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, InvalidOperationException>(
            ResultExceptionHelper.ThrowsInvalidOperationException,
            () => TestOkResult.Instance, () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(typeof(TestFailResult),result.FailValue.GetType());
        Assert.AreEqual(TestFailResult.Instance.Message, result.FailValue.Message);
    }
    
    [Test]
    public void GivenResultWhenITryCatchInOkFactoryAndItDoesNotThrowThenResultOkIsReturnedFromFactoryAndOkTypeIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, Exception>(
            () =>
            {
                ResultExceptionHelper.DoesNotThrowException();
                return TestOkResult.Instance;
            }, TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }
    
    [Test]
    public void GivenResultWhenITryCatchInOkFactoryAndItDoesNotThrowThenResultOkIsReturnedFromFactoryAndOkTypeIsProvidedModelAndFactoryFailIsIgnored()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, Exception>(
            () =>
            {
                ResultExceptionHelper.DoesNotThrowException();
                return TestOkResult.Instance;
            }, () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);

        Assert.AreEqual(result.OkValue.GetType(), TestOkResult.Instance.GetType());
    }
    
    [Test]
    public void GivenResultWhenITryCatchInOkFactoryAndItThrowsThenResultFailIsReturnedFromFactoryAndFailIsProvidedModel()
    {
        var result = Result.TryCatch<TestOkResult, TestFailResult, InvalidOperationException>(
            ResultExceptionHelper.ThrowsInvalidOperationException,
            () =>
            {
                ResultExceptionHelper.ThrowsInvalidOperationException();
                return TestOkResult.Instance;
            }, () => TestFailResult.Instance);

        Assert.IsNotNull(result);

        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);

        Assert.AreEqual(typeof(TestFailResult),result.FailValue.GetType());
        Assert.AreEqual(TestFailResult.Instance.Message, result.FailValue.Message);
    }
}