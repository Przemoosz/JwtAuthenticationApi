namespace JwtAuthenticationApi.Common
{
    using Abstraction.Factories.Polly;
    using Abstraction.Factories.Wrappers;
    using Abstraction.Handlers;
    using Abstraction.Wrappers;
    using Factories.Polly;
    using Factories.Wrappers;
    using Handlers;
    using JwtAuthenticationApi.Common.Abstraction.Factories;
    using JwtAuthenticationApi.Common.Factories;
    using Microsoft.Extensions.DependencyInjection;
    using Wrappers;

    public static class CommonInstaller
	{
		public static void InstallCommon(this IServiceCollection serviceCollection)
		{
			serviceCollection.AddTransient<IPollySleepingIntervalsFactory, PollySleepingIntervalsFactory>();
			serviceCollection.AddTransient<IMutexWrapperFactory, MutexWrapperFactory>();
			serviceCollection.AddTransient<ISemaphoreWrapperFactory, SemaphoreWrapperFactory>();
			serviceCollection.AddTransient<ICommandHandler, CommandHandler>();
			serviceCollection.AddTransient<IGuidFactory, GuidFactory>();
		}

	}
}
