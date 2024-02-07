using EmailService.Messaging;

namespace EmailService.Extensions
{
    public static class AzureServiceBusExtension
    {

        public static IAzureServiceBusConsumer azureServiceBusConsumer { get; set; }
        public static IApplicationBuilder useAzure(this IApplicationBuilder app)
        {

            //know about the Consumer service and alos about app Lifetime

            azureServiceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();

            var HostLifetime = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            HostLifetime.ApplicationStarted.Register(OnAppStart);
            HostLifetime.ApplicationStopping.Register(OnAppStopping);

            return app;

        }

        private static void OnAppStopping()
        {
            azureServiceBusConsumer.Stop();
        }

        private static void OnAppStart()
        {
            azureServiceBusConsumer.Start();
        }
    }
}
