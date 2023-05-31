using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Silverback.Messaging.Configuration;
using Silverback.Messaging.Messages;
using MassTransit;
using MassTransit.KafkaIntegration;
using Confluent.Kafka;
using Silverback.Messaging.Broker;
using System;
using System.Threading;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace Silverback.Samples.Kafka.Batch.Consumer
{
    public class Startup
    {
        private System.IServiceProvider _serviceProvider;
        private const string TaskEventsTopic = "testdata5";
        private const string KafkaBroker = "localhost:9092";
        private const string SchemaRegistryUrl = "http://localhost:8081";
        //public void ConfigureServices1(IServiceCollection services)
        //{

           
        //    try
        //    {
        //        services.AddMassTransit(x =>
        //        {
        //            //x.UsingRabbitMq((context, cfg) => cfg.ConfigureEndpoints(context));
        //            x.UsingInMemory((context, config) => config.ConfigureEndpoints(context));
        //            x.AddRider(rider =>
        //            {
        //                rider.AddConsumer<KafkaMessageConsumer>();

        //                rider.UsingKafka((context, k) =>
        //                {
        //                    k.Host("localhost:9092");

        //                    k.TopicEndpoint<KafkaMessage>("testdata4", "consumer-group-name", e =>
        //                    {
        //                        //e.ConfigureConsumer<KafkaMessageConsumer>(context);

                             
        //                      e.ConfigureConsumer<LogBatchConsumer>(context);
        //                     e.PrefetchCount = 20;

        //                                      e.Batch<KafkaMessage>(b =>
        //                                      {
        //                                           b.MessageLimit = 20;
        //                                          b.TimeLimit = TimeSpan.FromSeconds(5);

        //                                       });

        //                    });
        //                });
        //            });
        //        });

        //        _serviceProvider = services.BuildServiceProvider();
            

        //        //services.AddMassTransit(x =>
        //        //{               
        //        //    x.AddRider(rider =>
        //        //    {
        //        //        rider.AddConsumer<LogBatchConsumer>();
        //        //        rider.UsingKafka((context, k) =>
        //        //        {
        //        //            k.Host("localhost:9092");
        //        //            k.TopicEndpoint<KafkaMessage>("testdata4", "consumer-group-name", e =>
        //        //            {
        //        //                e.ConfigureConsumer<LogBatchConsumer>(context);
        //        //                e.PrefetchCount = 20;

        //    //                e.Batch<KafkaMessage>(b =>
        //    //                {
        //    //                    b.MessageLimit = 20;
        //    //                    b.TimeLimit = TimeSpan.FromSeconds(5);

        //    //                });
        //    //            });

        //    //        });
        //    //    });

        //    //});

        //    //services.AddMassTransitHostedService(true);
        //    //_serviceProvider = services.BuildServiceProvider();
        //    }
        //    catch (Exception ex)
        //    {

        //        string errMessage = ex.Message;
        //    }
        //}
        //    public void Configure1(IApplicationBuilder app)
        //{
        //    // app.UseMiddleware<SampleMessageBatchSubscriber>();
        //    //app.UseDeveloperExceptionPage();
                       

        //    //var a = _serviceProvider.GetRequiredService<IRiderRegistrationConfigurator>();

        //    //app.Use(async (context, next) =>
        //    //{
        //    //     var a = await _serviceProvider.GetRequiredService<LogBatchConsumer>().Consume(context: ConsumeContext<Batch<KafkaMessage>>)context);
        //    //   // await _serviceProvider.GetRequiredService<IRiderRegistrationConfigurator>();
        //    //    //context.RequestServices.GetRequiredService<ServiceCollectionAddSilverbackExtensions>.Run();
        //    //    await next(context);
        //    //});



        //    app.Use(async (context, next) =>
        //    {
        //        var a = _serviceProvider.GetRequiredService<LogBatchConsumer>().Consume(null);
        //        // await _serviceProvider.GetRequiredService<IRiderRegistrationConfigurator>();
        //        //context.RequestServices.GetRequiredService<ServiceCollectionAddSilverbackExtensions>.Run();
        //        await next(context);
        //    });


        //    app.Use(async (context, next) =>
        //    {
        //        var a = _serviceProvider.GetRequiredService<SampleMessageBatchSubscriber>().OnBatchReceivedAsync(null);
        //        // await _serviceProvider.GetRequiredService<IRiderRegistrationConfigurator>();
        //        //context.RequestServices.GetRequiredService<ServiceCollectionAddSilverbackExtensions>.Run();
        //        await next(context);
        //    });




        //}
        public void ConfigureServices(IServiceCollection services)
        {
            // Enable Silverback
            services
                .AddSilverback()

                // Use Apache Kafka as message broker
                .WithConnectionToMessageBroker(
                    options => options
                        .AddKafka())

                // Delegate the inbound/outbound endpoints configuration to a separate
                // class.
                .AddEndpointsConfigurator<EndpointsConfigurator>()            
                // Register the subscribers
                .AddSingletonSubscriber<TimedHostedService>();

           // services.AddHostedService<TimedHostedService>();
            _serviceProvider = services.BuildServiceProvider();
        }
        public void Configure(IApplicationBuilder app)
        {
            // app.UseMiddleware<SampleMessageBatchSubscriber>();
            //app.UseDeveloperExceptionPage();
            //var a = _serviceProvider.GetRequiredService<SampleMessageBatchSubscriber>();
            var address = app.ServerFeatures.Get<IServerAddressesFeature>();
            address.Addresses.Clear();
            address.Addresses.Add("http://*:5556");

            app.Use(async (context, next) =>
            {
                // var a= _serviceProvider.GetRequiredService<SampleMessageBatchSubscriber>();
              //  await _serviceProvider.GetRequiredService<SampleMessageBatchSubscriber>().OnBatchReceivedAsync(null);
                //context.RequestServices.GetRequiredService<ServiceCollectionAddSilverbackExtensions>.Run();
                await next(context);
            });


        }
       
    }
}
