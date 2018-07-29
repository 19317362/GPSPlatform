﻿using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using MessagePack.Formatters;
using Protocol.Common.Extensions;

namespace JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach
{
    public class JT808_0x0200_0x12Formatter : IMessagePackFormatter<JT808LocationAttachImpl0x12>
    {
        public JT808LocationAttachImpl0x12 Deserialize(byte[] bytes, int offset, IFormatterResolver formatterResolver, out int readSize)
        {
            offset = 0;
            JT808LocationAttachImpl0x12 jT808LocationAttachImpl0x12 = new JT808LocationAttachImpl0x12();
            jT808LocationAttachImpl0x12.AttachInfoId = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x12.AttachInfoLength = BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x12.JT808PositionType =(JT808PositionType)BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            jT808LocationAttachImpl0x12.AreaId = BinaryExtensions.ReadInt32Little(bytes, offset);
            offset = offset + 4;
            jT808LocationAttachImpl0x12.Direction =(JT808DirectionType)BinaryExtensions.ReadByteLittle(bytes, offset);
            offset = offset + 1;
            readSize = offset;
            return jT808LocationAttachImpl0x12;
        }

        public int Serialize(ref byte[] bytes, int offset, JT808LocationAttachImpl0x12 value, IFormatterResolver formatterResolver)
        {
            offset += BinaryExtensions.WriteLittle(ref bytes, offset,value.AttachInfoId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AttachInfoLength);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.JT808PositionType);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, value.AreaId);
            offset += BinaryExtensions.WriteLittle(ref bytes, offset, (byte)value.Direction);
            return offset;
        }
    }
}
