using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Codelux.Mappers;
using NUnit.Framework;

namespace Codelux.Tests.Mappers
{
    [TestFixture]
    public class AsyncMapperTests
    {
        private AsyncTestMapper _testMapper;

        [SetUp]
        public void Setup()
        {
            _testMapper = new();
        }

        [Test]
        public async Task GivenAsyncMapperWhenIMapAModelToAnotherThenTheValuesAreCorrect()
        {
            TestMapperInputModel model = new()
            {
                StringValue = "This is a test string value.",
                IntegerValue = 764
            };

            TestMapperOutputModel output = await _testMapper.MapAsync(model).ConfigureAwait(false);

            Assert.AreEqual(model.IntegerValue, output.IntegerValue);
            Assert.AreEqual(model.StringValue, output.StringValue);
            Assert.IsTrue(output.BoolValue);
            Assert.NotNull(output.GuidValue);
        }

        [Test]
        public async Task GivenAsyncMapperWhenIMapManyModelsToAnotherThenTheValuesAreCorrect()
        {
            List<TestMapperInputModel> inputModels = new()
            {
                new()
                {
                    StringValue = "Model 1.",
                    IntegerValue = 12
                },
                new()
                {
                    StringValue = "Model 2.",
                    IntegerValue = 4555
                },
                new()
                {
                    StringValue = "Model 2.",
                    IntegerValue = 533
                },
            };

            IAsyncEnumerable<TestMapperOutputModel> result = _testMapper.MapAsync(inputModels);
            List<TestMapperOutputModel> outputModels = await result.ToListAsync();

            for (int x = 0; x < outputModels.Count; x++)
            {
                Assert.AreEqual(inputModels[x].StringValue, outputModels[x].StringValue);
                Assert.AreEqual(inputModels[x].IntegerValue, outputModels[x].IntegerValue);
                Assert.IsTrue(outputModels[x].BoolValue);
                Assert.NotNull(outputModels[x].GuidValue);
            }
        }
    }

    class AsyncTestMapper : AsyncMapperBase<TestMapperInputModel, TestMapperOutputModel>
    {
        public override Task<TestMapperOutputModel> MapAsync(TestMapperInputModel model, CancellationToken token = default)
        {
            return Task.FromResult(new TestMapperOutputModel()
            {
                BoolValue = true,
                GuidValue = Guid.NewGuid(),
                IntegerValue = model.IntegerValue,
                StringValue = model.StringValue
            });
        }
    }
}
