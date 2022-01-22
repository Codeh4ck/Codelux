using NUnit.Framework;

namespace Codelux.Tests.Plugins
{
    [TestFixture]
    public class PluginTests
    {
        [Test]
        public void GivenPluginImplementationWhenIStartPluginThenPluginStarts()
        {
            TestablePlugin plugin = new TestablePlugin();
            plugin.Configure(TestablePlugin.GetConfiguration());

            plugin.Start();
            Assert.IsTrue(plugin.Flag);
        }

        [Test]
        public void GivenPluginImplementationAndIsRunningWhenIStopPluginThenPluginStops()
        {
            TestablePlugin plugin = new TestablePlugin();
            plugin.Configure(TestablePlugin.GetConfiguration());

            plugin.Start();
            Assert.IsTrue(plugin.Flag);

            plugin.Stop();
            Assert.IsFalse(plugin.Flag);
        }
    }
}
