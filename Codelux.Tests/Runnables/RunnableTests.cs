using System;
using Codelux.Runnables;
using NUnit.Framework;

namespace Codelux.Tests.Runnables
{
    [TestFixture]
    public class RunnableTests
    {
        private TestRunnable _runnable;

        [SetUp]
        public void Setup()
        {
            _runnable = new();
        }

        [Test]
        public void GivenRunnableWhenIStartWihoutContextThenTheRunnableIsExecuted()
        {
            Assert.DoesNotThrow(delegate () { _runnable.Start(); });

            Assert.IsTrue(_runnable.IsRunning);
            Assert.AreEqual(10, _runnable.CurrentValue);
        }

        [Test]
        public void GivenRunnableWhenIStartWihContextThenTheRunnableIsExecutedAndContextIsHonored()
        {
            Assert.DoesNotThrow(delegate() { _runnable.Start(50); });

            Assert.IsTrue(_runnable.IsRunning);
            Assert.AreEqual(50, _runnable.CurrentValue);
        }

        [Test]
        public void GivenRunnableWhenIStopItThenRunnableStops()
        {
            Assert.DoesNotThrow(delegate () { _runnable.Start(); });

            Assert.AreEqual(_runnable.IsRunning, true);
            Assert.AreEqual(10, _runnable.CurrentValue);

            Assert.DoesNotThrow(delegate() { _runnable.Stop(); });

            Assert.IsFalse(_runnable.IsRunning);
            Assert.AreEqual(-1, _runnable.CurrentValue);
        }

        [Test]
        public void GivenRunnableIsRunningAndThrowsThenItStopsAndExceptionIsThrown()
        {
            Exception ex = Assert.Throws<Exception>(delegate () { _runnable.Start(true); });

            Assert.IsFalse(_runnable.IsRunning);
            Assert.AreEqual("Runnable has thrown an exception", ex.Message);
        }
    }


    public class TestRunnable : Runnable
    {
        public int CurrentValue { get; set; } = 0;

        public override void OnStart(object context = null)
        {
            if (context != null)
            {
                if (context is int nextValue)
                    CurrentValue = nextValue;

                if (context is bool)
                    throw new("Runnable has thrown an exception");
            }
            else
                CurrentValue = 10;
        }

        public override void OnStop()
        {
            CurrentValue = -1;
        }
    }
}
