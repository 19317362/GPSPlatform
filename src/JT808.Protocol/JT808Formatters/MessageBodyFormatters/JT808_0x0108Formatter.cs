﻿using JT808.Protocol.Enums;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBody;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0108Formatter : IJT808Formatter<JT808_0x0108>
    {
        public JT808_0x0108 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0108 jT808_0X0108 = new JT808_0x0108();
            jT808_0X0108.UpgradeType= (JT808UpgradeType)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0108.UpgradeResult = (JT808UpgradeResult)JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0108;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0108 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.UpgradeType);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.UpgradeResult);
            return offset;
        }
    }
}
