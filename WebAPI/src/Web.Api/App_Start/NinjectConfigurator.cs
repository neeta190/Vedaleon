using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using log4net.Config;
using Ninject;
using Web.Api.Common;
using Web.Api.Common.Logging;
using Web.Api.Processors;
using Ninject.Web.Common;

namespace Web.Api
{
    public class NinjectConfigurator
    {
        public void Configure(IKernel container)
        {
            AddBindings(container);
        }
        private void AddBindings(IKernel container)
        {
            ConfigureLog4net(container);
            container.Bind<IDateTime>().To<DateTimeAdapter>().InSingletonScope();
            container.Bind<IRecordProcessor>().To<RecordProcessor>().InRequestScope();
        }

        private void ConfigureLog4net(IKernel container)
        {
            XmlConfigurator.Configure();
            var logManager = new LogManagerAdapter();
            container.Bind<ILogManager>().ToConstant(logManager);
        }
    }
}