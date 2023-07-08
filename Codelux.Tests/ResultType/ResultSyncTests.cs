using System;
using Codelux.Common.ResultType;
using NUnit.Framework;

namespace Codelux.Tests.ResultType;

[TestFixture]
public class ResultSyncTests
{
    [Test]
    public void GivenOkTypeWhenIConstructResultThenValidResultIsConstructed()
    {
        TestOkResult okResult = TestOkResult.Instance;
        IResult<TestOkResult, TestFailResult> result = Result.Ok<TestOkResult, TestFailResult>(okResult);
        
        Assert.IsNotNull(result);
        
        Assert.IsTrue(result.IsOk);
        Assert.IsFalse(result.IsFail);
        
        Assert.IsTrue(result.OkValue == okResult);
        Assert.Throws<InvalidOperationException>(() => _ =result.FailValue);
    }
    
    [Test]
    public void GivenFailTypeWhenIConstructResultThenValidResultIsConstructed()
    {
        TestFailResult failResult = TestFailResult.Instance;
        IResult<TestOkResult, TestFailResult> result = Result.Fail<TestOkResult, TestFailResult>(failResult);
        
        Assert.IsNotNull(result);
        
        Assert.IsFalse(result.IsOk);
        Assert.IsTrue(result.IsFail);
        
        Assert.Throws<InvalidOperationException>(() => _ = result.OkValue);
        Assert.IsTrue(result.FailValue == failResult);
    }

    [Test]
    public void GivenValidOkAndFailResultsWhenICloneThenTheCorrectClonedResultsAreReturned()
    {
        TestOkResult testOkResult = TestOkResult.Instance;
        IResult<TestOkResult, TestFailResult> okResult = Result.Ok<TestOkResult, TestFailResult>(testOkResult);
        
        TestFailResult testFailResult = TestFailResult.Instance;
        IResult<TestOkResult, TestFailResult> failResult = Result.Fail<TestOkResult, TestFailResult>(testFailResult);
        
        var clonedOkResult = Result.Clone(okResult);
        var clonedFailResult = Result.Clone(failResult);
        
        Assert.AreEqual(clonedOkResult, okResult);
        Assert.AreEqual(clonedFailResult, failResult);
    }
}