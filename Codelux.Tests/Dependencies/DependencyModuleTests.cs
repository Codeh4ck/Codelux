using NUnit.Framework;
using Unity;

namespace Codelux.Tests.Dependencies
{
    [TestFixture]
    public class DependencyModuleTests
    {
        private IUnityContainer _unityContainer;
        private TestDependencyModule _dependencyModule;

        [SetUp]
        public void Setup()
        {
            _unityContainer = new UnityContainer();
            _dependencyModule = new(_unityContainer);
        }

        [TearDown]
        public void TearDown()
        {
            _unityContainer = null;
            _dependencyModule = null;
        }

        [Test]
        public void GivenDependencyModuleWhenIRegisterDependenciesThenDependenciesAreRegistered()
        {
            Assert.DoesNotThrow(delegate()
            {
                _dependencyModule.RegisterDependencies();
            });

            ITestInterface testInterface = _unityContainer.Resolve<ITestInterface>();
            int number = testInterface.GetOne();


            Assert.IsInstanceOf<TestConcreteClass>(testInterface);
            Assert.AreEqual(1, number);
        }
    }
}
