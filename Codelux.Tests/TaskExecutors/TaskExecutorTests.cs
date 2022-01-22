using System.Threading;
using System.Threading.Tasks;
using Codelux.Executors;
using NUnit.Framework;

namespace Codelux.Tests.TaskExecutors
{
    [TestFixture]
    public class TaskExecutorTests
    {
        private ExecutorBase<TestExecutorInputModel, TestExecutorOutputModel> _executor;

        [SetUp]
        public void Setup()
        {
            _executor = new TestExecutor();
        }

        [Test]
        public async Task GivenTaskExecutorWhenIExecuteThenExecutorExecutesAndResultIsCorrect()
        {
            TestExecutorInputModel input = new()
            {
                GreaterThanZero = false
            };

            TestExecutorOutputModel model = await _executor.ExecuteAsync(input).ConfigureAwait(false);

            Assert.IsNotNull(model);
            Assert.AreEqual(-10, model.Value);
        }
    }


    class TestExecutor : ExecutorBase<TestExecutorInputModel, TestExecutorOutputModel>
    {
        protected override Task<TestExecutorOutputModel> OnExecuteAsync(TestExecutorInputModel tin, CancellationToken token = default)
        {
            if (tin.GreaterThanZero)
                return Task.FromResult(new TestExecutorOutputModel() {Value = 10});

            return Task.FromResult(new TestExecutorOutputModel() { Value = -10 });
        }
    }
}
