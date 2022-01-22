using System;
using System.Linq;
using Codelux.Dependencies;
using NUnit.Framework;
using Unity;

namespace Codelux.Tests.Dependencies
{
    [TestFixture]
    public class DependencyRegistrarTests
    {

        private DependencyRegistrar _dependencyRegistrar;

        [SetUp]
        public void Setup()
        {
            _dependencyRegistrar = new();
        }

        [Test]
        public void GivenDependencyRegistrarWhenIRegisterAllDependenciesThenDependenciesAreRegistered()
        {
            Assert.DoesNotThrow(delegate ()
            {
                _dependencyRegistrar.RegisterAllDependencies();
            });

            // We are testing for 2 registrations because Unity registers an instance of IUnityContainer by itself
            Assert.AreEqual(2, _dependencyRegistrar.Registrations.Count());

            ITestInterface testInterface = _dependencyRegistrar.Resolve<ITestInterface>();

            int number = testInterface.GetOne();

            Assert.IsNotNull(testInterface);
            Assert.IsInstanceOf<TestConcreteClass>(testInterface);

            Assert.AreEqual(1, number);
        }

        [Test]
        public void GivenDependencyRegistrarWhenIRegisterAllDependenciesButDependenciesWhereRegisteredAlreadyThenItThrows()
        {
            Assert.DoesNotThrow(delegate ()
            {
                _dependencyRegistrar.RegisterAllDependencies();
            });

            Assert.AreEqual(2, _dependencyRegistrar.Registrations.Count());

            ITestInterface testInterface = _dependencyRegistrar.Resolve<ITestInterface>();

            int number = testInterface.GetOne();

            Assert.IsNotNull(testInterface);
            Assert.IsInstanceOf<TestConcreteClass>(testInterface);

            Assert.AreEqual(1, number);

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(delegate ()
            {
                _dependencyRegistrar.RegisterAllDependencies();
            });

            Assert.IsNotNull(exception);
            Assert.AreEqual("Dependencies have already been registered.", exception.Message);
        }
    }
}
