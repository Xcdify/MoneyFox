﻿using Autofac;
using MoneyFox.Application.Common;
using MoneyFox.Application.Common.FileStore;
using MoneyFox.Infrastructure;
using MoneyFox.Presentation;
using PCLAppConfig;

namespace MoneyFox.Droid
{
    public class AndroidModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new TokenObject {CurrencyConverterApi = ConfigurationManager.AppSettings["CurrencyConverterApiKey"]});

            builder.RegisterModule<PresentationModule>();
            builder.RegisterModule<InfrastructureModule>();

            builder.RegisterType<DroidAppInformation>().AsImplementedInterfaces();
            builder.RegisterType<PlayStoreOperations>().AsImplementedInterfaces();
            builder.RegisterType<NavigationService>().AsImplementedInterfaces();
            builder.RegisterType<ThemeSelectorAdapter>().AsImplementedInterfaces();
            builder.Register(c => new FileStoreIoBase(Android.App.Application.Context.FilesDir.Path)).AsImplementedInterfaces();
        }
    }
}
