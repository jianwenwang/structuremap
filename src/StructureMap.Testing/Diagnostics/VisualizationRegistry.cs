﻿using StructureMap.Configuration.DSL;
using StructureMap.Testing.Acceptance;
using StructureMap.Testing.Widget;

namespace StructureMap.Testing.Diagnostics
{
    public class VisualizationRegistry : Registry
    {
        public VisualizationRegistry()
        {
            For<IDevice>().Use<DefaultDevice>();

            For<IDevice>().Add(() => new ADevice()).Named("A");
            For<IDevice>().Add(new BDevice()).Named("B");

            For<IDevice>().Add<DeviceWithArgs>()
                .Named("GoodSimpleArgs")
                .Ctor<string>("color").Is("Blue")
                .Ctor<string>("direction").Is("North")
                .Ctor<string>("name").Is("Declan");


            For<Rule>().Use<ColorRule>().Ctor<string>("color").Is("Red").Named("Red");
        }
    }

    public interface IDevice{}

    public class DefaultDevice : IDevice { }

    public class ADevice : Activateable, IDevice
    {

    }

    public class BDevice : Activateable, IDevice
    {

    }

    public class CDevice : Activateable, IDevice
    {

    }

    public class DeviceWithArgs : IDevice
    {
        public DeviceWithArgs(string color, string direction, string name)
        {
        }
    }



    public class DeviceWrapper
    {
        public IDevice Wrap(IDevice Device)
        {
            return new DeviceDecorator(Device);
        }
    }

    public class DeviceDecorator : IDevice
    {
        private readonly IDevice _inner;

        public DeviceDecorator(IDevice inner)
        {
            _inner = inner;
        }

        public IDevice Inner
        {
            get { return _inner; }
        }
    }
}