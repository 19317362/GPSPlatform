﻿using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using JT808.Protocol.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GPS.JT808PubSubToKafka
{
    public sealed class JT808_0x0200_Producer : JT808MsgIdProducerBase
    {
        private Producer<Null, byte[]> producer;

        public JT808_0x0200_Producer()
        {
            producer = new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public JT808_0x0200_Producer(Dictionary<string, object> config) : base(config)
        {
            producer = new Producer<Null, byte[]>(Config, null, new ByteArraySerializer());
        }

        public override void ProduceAsync(string key,byte[] data)
        {
             producer.ProduceAsync(TopicName, null, data);
        }

        public override void Dispose()
        {
            producer.Dispose();
        }

        public override string TopicName  => JT808.Protocol.Enums.JT808MsgId.位置信息汇报.ToValueString();


    }
}
