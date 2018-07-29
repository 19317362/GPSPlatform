﻿using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x31Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x31>
    {
        public JT808LocationAttachImpl0x31 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x31 jT808LocationAttachImpl0x31 = new JT808LocationAttachImpl0x31();
            jT808LocationAttachImpl0x31.AttachInfoId = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x31.AttachInfoLength = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x31.GNSSCount = JT808BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            readSize = offset;
            return jT808LocationAttachImpl0x31;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x31 value, IFormatterResolver formatterResolver)
        {
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += JT808BinaryExtensions.WriteLittle(ref bytes, offset, value.GNSSCount);
            return offset;
        }
    }
}
