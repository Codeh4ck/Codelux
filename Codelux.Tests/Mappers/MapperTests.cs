using System;
using System.Collections.Generic;
using System.Linq;
using Codelux.Mappers;
using NUnit.Framework;

namespace Codelux.Tests.Mappers
{
    [TestFixture]
    public class MapperTests
    {
        private TestMapper _testMapper;

        [SetUp]
        public void Setup()
        {
            _testMapper = new();
        }

        [Test]
        public void GivenMapperWhenIMapAModelToAnotherThenTheValuesAreCorrect()
        {
            TestMapperInputModel model = new()
            {
                StringValue = "This is a test string value.",
                IntegerValue = 764
            };

            TestMapperOutputModel output = _testMapper.Map(model);

            Assert.AreEqual(model.IntegerValue, output.IntegerValue);
            Assert.AreEqual(model.StringValue, output.StringValue);
            Assert.IsTrue(output.BoolValue);
            Assert.NotNull(output.GuidValue);
        }

        [Test]
        public void GivenMapperWhenIMapManyModelsToAnotherThenTheValuesAreCorrect()
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

            List<TestMapperOutputModel> outputModels = _testMapper.Map(inputModels).ToList();

            for (int x = 0; x < outputModels.Count; x++)
            {
                Assert.AreEqual(inputModels[x].StringValue, outputModels[x].StringValue);
                Assert.AreEqual(inputModels[x].IntegerValue, outputModels[x].IntegerValue);
                Assert.IsTrue(outputModels[x].BoolValue);
                Assert.NotNull(outputModels[x].GuidValue);
            }
        }
    }

    class TestMapper : MapperBase<TestMapperInputModel, TestMapperOutputModel>
    {
        public override TestMapperOutputModel Map(TestMapperInputModel model)
        {
            return new()
            {
                BoolValue = true,
                GuidValue = Guid.NewGuid(),
                IntegerValue = model.IntegerValue,
                StringValue = model.StringValue
            };
        }
    }
}
