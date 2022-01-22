using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Codelux.Factories;
using Codelux.Plugins;
using Codelux.Plugins.Base;
using Codelux.Plugins.Metadata;
using Moq;
using NUnit.Framework;

namespace Codelux.Tests.Plugins
{
    [TestFixture]
    public class PluginInstanceManagerTests
    {
        private PluginInstanceManager<TestablePluginBase> _testablePluginManager;
        private Mock<IPluginConfigurationSource> _configSourceMock;
        private Mock<ITypeInstanceFactory> _instanceFactoryMock;

        [SetUp]
        public void Setup()
        {
            _configSourceMock = new Mock<IPluginConfigurationSource>();
            _instanceFactoryMock = new Mock<ITypeInstanceFactory>();

            List<PluginConfiguration> pluginConfig = new List<PluginConfiguration>()
            {
                TestablePlugin.GetConfiguration(),
                SecondTestablePlugin.GetConfiguration()
            };

            _configSourceMock.Setup(x => x.ReadConfiguration(It.IsAny<bool>())).Returns(pluginConfig);

            _instanceFactoryMock.Setup(x => x.CreateInstance<TestablePluginBase>(typeof(TestablePlugin)))
                .Returns(new TestablePlugin());

            _instanceFactoryMock.Setup(x => x.CreateInstance<TestablePluginBase>(typeof(SecondTestablePlugin)))
                .Returns(new SecondTestablePlugin());

            _testablePluginManager =
                new PluginInstanceManager<TestablePluginBase>(_configSourceMock.Object, _instanceFactoryMock.Object);
        }

        [Test]
        public void GivenPluginInstanceManagerWhenIInstantiateWithNullPluginConfigurationSourceThenItThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PluginInstanceManager<TestablePluginBase>(null, _instanceFactoryMock.Object));
        }

        [Test]
        public void GivenPluginInstanceManagerWhenIInstantiateWithNullTypeInstanceFactoryThenItThrows()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new PluginInstanceManager<TestablePluginBase>(_configSourceMock.Object, null));
        }

        [Test]
        public void GivenPluginInstanceManagerWhenIInstantiateThenConfigurationIsLoaded()
        {
            List<PluginConfiguration> pluginConfig = new List<PluginConfiguration>()
            {
                TestablePlugin.GetConfiguration()
            };

            _configSourceMock.Setup(x => x.ReadConfiguration(It.IsAny<bool>())).Returns(pluginConfig);

            Assert.DoesNotThrow(() => new PluginInstanceManager<TestablePluginBase>(_configSourceMock.Object, _instanceFactoryMock.Object));

            _configSourceMock.Verify(x => x.ReadConfiguration(It.IsAny<bool>()), Times.AtLeastOnce);
        }

        [Test]
        public void GivenPluginInstanceManagerWhenIInstantiateThenConfigurationIsLoadedAndIsStored()
        {
            List<PluginConfiguration> pluginConfig = new List<PluginConfiguration>()
            {
                TestablePlugin.GetConfiguration()
            };

            _configSourceMock.Setup(x => x.ReadConfiguration(It.IsAny<bool>())).Returns(pluginConfig);

            PluginInstanceManager<TestablePluginBase> instanceManager =
                new PluginInstanceManager<TestablePluginBase>(_configSourceMock.Object, _instanceFactoryMock.Object);

            List<PluginConfiguration> pluginConfigurations = instanceManager.GetPluginConfigurations();

            Assert.AreEqual(pluginConfig.Count, pluginConfigurations.Count);

            _configSourceMock.Verify(x => x.ReadConfiguration(It.IsAny<bool>()), Times.AtLeastOnce);
        }

        [Test]
        public void GivenPluginInstanceManagerWithLoadedPluginConfigurationsWhenIStartThenPluginsAreStarted()
        {
            TestablePlugin plugin = new TestablePlugin();

            _instanceFactoryMock.Setup(x => x.CreateInstance<TestablePluginBase>(typeof(TestablePlugin)))
                .Returns(plugin);

            _testablePluginManager.Start();

            Assert.IsTrue(plugin.IsRunning);
            Assert.AreEqual(_testablePluginManager.GetPluginConfigurations().Count, _testablePluginManager.GetRunningPlugins().Count);
            
            _instanceFactoryMock.Verify(x => x.CreateInstance<TestablePluginBase>(typeof(TestablePlugin)));
        }

        [Test]
        public void GivenPluginInstanceManagerWithRunningPluginsWhenIStopThenPlugginsAreStopped()
        {
            TestablePlugin plugin = new TestablePlugin();

            _instanceFactoryMock.Setup(x => x.CreateInstance<TestablePluginBase>(typeof(TestablePlugin)))
                .Returns(plugin);

            _testablePluginManager.Start();

            Assert.IsTrue(plugin.IsRunning);
            Assert.AreEqual(_testablePluginManager.GetPluginConfigurations().Count, _testablePluginManager.GetRunningPlugins().Count);

            _testablePluginManager.Stop();

            Assert.IsFalse(plugin.IsRunning);
            Assert.AreEqual(0, _testablePluginManager.GetRunningPlugins().Count);

            _instanceFactoryMock.Verify(x => x.CreateInstance<TestablePluginBase>(typeof(TestablePlugin)));
        }

        [Test]
        public void GivenInstanceManagerWithRunningPluginsWhenIProcessPluginsThenActionIsExecuted()
        {
            _testablePluginManager.Start();

            Assert.IsTrue(_testablePluginManager.GetLoadedPlugins().TrueForAll(x => x.Flag));

            _testablePluginManager.ProcessPlugins(x => x.Process(false));

            Assert.IsTrue(_testablePluginManager.GetLoadedPlugins().TrueForAll(x => !x.Flag));
        }

        [Test]
        public async Task GivenInstanceManagerWithRunningPluginsWhenIProcessAsyncPluginsThenActionIsExecuted()
        {
            _testablePluginManager.Start();

            Assert.IsTrue(_testablePluginManager.GetLoadedPlugins().TrueForAll(x => x.Flag));

            await _testablePluginManager.ProcessPluginsAsync(x => x.Process(false));

            Assert.IsTrue(_testablePluginManager.GetLoadedPlugins().TrueForAll(x => !x.Flag));
        }

        [Test]
        public void GivenInstanceManagerWhenIUnloadRunningPluginThenPluginIsStoppedAndRemoved()
        {
            _testablePluginManager.Start();
            List<TestablePluginBase> runningPlugins = _testablePluginManager.GetRunningPlugins();
            Assert.AreEqual(2, runningPlugins.Count);

            _testablePluginManager.UnloadPlugin(runningPlugins[1].Configuration.Header.Id);

            Assert.AreEqual(1, _testablePluginManager.GetRunningPlugins().Count);
        }

        [Test]
        public void GivenInstanceManagerWithRunningPluginsWhenIGetRunningPluginsThenOnlyRunningPluginsAreReturned()
        {
            _testablePluginManager.Start();

            List<TestablePluginBase> runningPlugins = _testablePluginManager.GetRunningPlugins();
            Assert.AreEqual(2, runningPlugins.Count);

            _testablePluginManager.GetRunningPlugins()[1].Stop();

            Assert.AreEqual(1, _testablePluginManager.GetRunningPlugins().Count);
        }

        [Test]
        public void GivenInstanceManagerWithRunningPluginsWhenIGetLoadedPluginsTheAllLoadedPluginsAreReturned()
        {
            _testablePluginManager.Start();

            List<TestablePluginBase> loadedPlugins = _testablePluginManager.GetLoadedPlugins();
            Assert.AreEqual(2, loadedPlugins.Count);

            _testablePluginManager.GetRunningPlugins()[1].Stop();

            Assert.AreEqual(2, _testablePluginManager.GetLoadedPlugins().Count);
        }
    }
}
