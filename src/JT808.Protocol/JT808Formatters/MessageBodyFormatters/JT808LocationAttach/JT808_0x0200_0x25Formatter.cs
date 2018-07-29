﻿using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x25Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x25>
    {
        public JT808LocationAttachImpl0x25 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x25 jT808LocationAttachImpl0x13 = new JT808LocationAttachImpl0x25();
            jT808LocationAttachImpl0x13.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x13.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x13.CarSignalStatus =BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            readSize = offset;
            return jT808LocationAttachImpl0x13;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x25 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.CarSignalStatus);
            return offset;
        }
    }
}
