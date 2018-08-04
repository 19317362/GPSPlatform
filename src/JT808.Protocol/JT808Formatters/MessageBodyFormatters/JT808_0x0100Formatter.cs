﻿using JT808.Protocol.MessageBodyRequest;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters
{
    public class JT808_0x0100Formatter : IJT808Formatter<JT808_0x0100>
    {
        public JT808_0x0100 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808_0x0100 jT808_0X0100 = new JT808_0x0100();
            jT808_0X0100.AreaID = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0100.CityOrCountyId = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808_0X0100.MakerId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset,5);
            jT808_0X0100.TerminalType = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 20);
            jT808_0X0100.TerminalId = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset, 7);
            jT808_0X0100.PlateColor = JT808BinaryExtensions.ReadByteLittle(bytes, ref offset);
            jT808_0X0100.PlateNo = JT808BinaryExtensions.ReadStringLittle(bytes, ref offset);
            readSize = offset;
            return jT808_0X0100;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808_0x0100 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.AreaID);
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.CityOrCountyId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.MakerId.PadRight(5, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalType.PadRight(20,'0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.TerminalId.PadRight(7, '0'));
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.PlateColor);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.PlateNo);
            return offset;
        }
    }
}
