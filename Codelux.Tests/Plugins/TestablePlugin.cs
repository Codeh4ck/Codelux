using System;
using Codelux.Plugins.Base;
using Codelux.Plugins.Metadata;
using NUnit.Framework;

namespace Codelux.Tests.Plugins
{

    public abstract class TestablePluginBase : PluginBase
    {
        public abstract bool Flag { get; set; }
        public abstract void Process(bool flag);
    }

    public class TestablePlugin : TestablePluginBase
    {
        public override bool Flag { get; set; }

        public override void OnStart(object context = null)
        {
            Assert.IsTrue(IsRunning);
            Process(IsRunning);
        }

        public override void OnStop()
        {
            Assert.IsFalse(IsRunning);
            Process(IsRunning);
        }

        public override void Configure(PluginConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void Process(bool flag)
        {
            Flag = flag;
        }

        public static PluginConfiguration GetConfiguration()
        {
            return new()
            {
                Header = new()
                {
                    Name = "Testable Plugin",
                    Id = Guid.Parse("4d96f32f-6aa2-4441-aca4-2baefad23cd5"),
                    Description = "A testable plugin for unit tests"
                },
                PluginType = typeof(TestablePlugin)
            };
        }

    }

    public class SecondTestablePlugin : TestablePluginBase
    {

        public override bool Flag { get; set; }

        public override void OnStart(object context = null)
        {
            Assert.IsTrue(IsRunning);
            Process(IsRunning);
        }

        public override void OnStop()
        {
            Assert.IsFalse(IsRunning);
            Process(IsRunning);
        }

        public override void Configure(PluginConfiguration configuration)
        {
            Configuration = configuration;
        }

        public override void Process(bool flag)
        {
            Flag = flag;
        }

        public static PluginConfiguration GetConfiguration()
        {
            return new()
            {
                Header = new()
                {
                    Name = "Second Testable Plugin",
                    Id = Guid.Parse("2427ae5b-2e42-46ae-815c-61fd18fd78b7"),
                    Description = "A second testable plugin for unit tests"
                },
                PluginType = typeof(SecondTestablePlugin)
            };
        }
    }
}
