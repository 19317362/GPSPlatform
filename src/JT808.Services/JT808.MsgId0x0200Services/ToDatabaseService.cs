﻿using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using GPS.PubSub.Abstractions;
using JT808.Protocol.Extensions;

namespace JT808.MsgId0x0200Services
{
    public class ToDatabaseService : IHostedService
    {
        private readonly IConsumerFactory ConsumerFactory;

        private readonly ILogger<ToDatabaseService> logger;

        public ToDatabaseService(ILoggerFactory loggerFactory, IConsumerFactory consumerFactory)
        {
            ConsumerFactory = consumerFactory;
            logger = loggerFactory.CreateLogger<ToDatabaseService>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            ConsumerFactory
                .Subscribe(((ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报).ToString())
                .OnMessage((msg) =>
                {
                    logger.LogDebug($"{msg.Key},{ msg.data.ToHexString()}");
                });
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Stop ...");
            ConsumerFactory.Unsubscribe(((ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报).ToString());
            logger.LogInformation("Stop CompletedTask");
            return Task.CompletedTask;
        }
    }
}
