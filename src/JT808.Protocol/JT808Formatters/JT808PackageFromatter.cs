﻿using JT808.Protocol.MessageBodyRequest;
using MessagePack;
using MessagePack.Formatters;
using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.JT808Formatters
{
    /// <summary>
    /// JT808包序列化器
    /// </summary>
    public class JT808PackageFromatter : IMessagePackFormatter<JT808Package>
    {
        public JT808Package Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            readSize = 1;
            return null;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808Package value, IFormatterResolver formatterResolver)
        {
            offset += MessagePackBinary.WriteByte(ref bytes, offset, value.Begin);
            offset += formatterResolver.GetFormatter<JT808Header>().Serialize(ref bytes, offset, value.Header, formatterResolver);
            Type type = JT808FormattersBodiesFactory.Create(value.Header.MsgId);
            offset += formatterResolver.GetFormatterDynamic(type).JT808DynamicSerialize(ref bytes, offset, value.Bodies,formatterResolver);
            //offset += bodyFormatter.Serialize(ref bytes, offset, value.Bodies, formatterResolver);
            offset += MessagePackBinary.WriteByte(ref bytes, offset, 01);
            offset += MessagePackBinary.WriteByte(ref bytes, offset, value.End);
            return offset;
        }
    }
}
