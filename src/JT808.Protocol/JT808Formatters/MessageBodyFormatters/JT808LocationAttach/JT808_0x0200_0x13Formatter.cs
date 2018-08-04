﻿using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using JT808.Protocol.Extensions;
using System;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x13Formatter : IJT808Formatter<JT808LocationAttachImpl0x13>
    {
        public JT808LocationAttachImpl0x13 Deserialize(ReadOnlySpan<byte> bytes, int offset, IJT808FormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x13 jT808LocationAttachImpl0x13 = new JT808LocationAttachImpl0x13();
            jT808LocationAttachImpl0x13.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x13.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            jT808LocationAttachImpl0x13.DrivenRouteId =JT808BinaryExtensions.ReadInt32Little(bytes,ref offset);
            jT808LocationAttachImpl0x13.Time = JT808BinaryExtensions.ReadUInt16Little(bytes,ref offset);
            jT808LocationAttachImpl0x13.DrivenRoute =(JT808DrivenRouteType)JT808BinaryExtensions.ReadByteLittle(bytes,ref offset);
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x13 value, IJT808FormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.DrivenRouteId);
            offset += JT808BinaryExtensions.WriteUInt16Little(ref bytes, offset, value.Time);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.DrivenRoute);
            return offset;
        }
    }
}
