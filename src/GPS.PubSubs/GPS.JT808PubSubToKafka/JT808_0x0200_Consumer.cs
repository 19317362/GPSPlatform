﻿using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public class JT808_0x0200_Consumer : JT808MsgIdConsumerBase
    {
        private Consumer<Null, byte[]> consumer;

        protected override ILogger Logger { get; }

        public JT808_0x0200_Consumer(ILoggerFactory loggerFactory) :base(loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_0x0200_Consumer>();
            consumer = new Consumer<Null, byte[]>(Config, null, new ByteArrayDeserializer());
            RegisterEvent();
        }

        public JT808_0x0200_Consumer(Dictionary<string, object> config, ILoggerFactory loggerFactory) : base(config, loggerFactory)
        {
            Logger = loggerFactory.CreateLogger<JT808_0x0200_Consumer>();
            consumer = new Consumer<Null, byte[]>(Config, null, new ByteArrayDeserializer());
            RegisterEvent();
        }

        public override ushort CategoryId => (ushort)JT808.Protocol.Enums.JT808MsgId.位置信息汇报;

        public override void OnMessage(Action<(string Key, byte[] data)> callback)
        {
            consumer.OnMessage += (_, msg) =>
            {
                callback(("", msg.Value));
                // todo: 处理定位数据
                Logger.LogDebug($"Topic: {msg.Topic} Partition: {msg.Partition} Offset: {msg.Offset} {msg.Value.ToHexString()}");
            };
        }

        public override void Subscribe()
        {
            Task.Run(() =>
            {
                while (!Cts.IsCancellationRequested)
                {
                    try
                    {
                        consumer.Poll(TimeSpan.FromMilliseconds(100));
                    }
                    catch (Exception ex)
                    {
                        Logger.LogError(Thread.CurrentThread.Name, ex);
                        Thread.Sleep(5000);
                    }
                }
            }, Cts.Token);
            consumer.Subscribe(JT808MsgIdTopic);
        }

        public override void Unsubscribe()
        {
            Cts.Cancel();
            consumer.Unsubscribe();
        }

        protected override void RegisterEvent()
        {
            consumer.OnError += (_, error) =>
            {
                Logger.LogError($"Error: {error}");
            };
            consumer.OnConsumeError += (_, msg) =>
            {
                Logger.LogError($"Error consuming from topic/partition/offset {msg.Topic}/{msg.Partition}/{msg.Offset}: {msg.Error}");
            };
        }
    }
}
