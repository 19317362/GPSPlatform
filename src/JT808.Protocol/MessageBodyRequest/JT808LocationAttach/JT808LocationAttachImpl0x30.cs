﻿using System;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    public class JT808LocationAttachImpl0x30 : JT808LocationAttachBase
    {
        public JT808LocationAttachImpl0x30()
        {
        }

        public JT808LocationAttachImpl0x30(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        public byte WiFiSignalStrength { get; set; }

        public override byte AttachInfoId { get; protected set; } = 0x30;

        public override byte AttachInfoLength { get; protected set; } = 1;

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AttachInfoId = Buffer.Span[0];
            AttachInfoLength = Buffer.Span[1];
            WiFiSignalStrength = Buffer.Span[2];
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Buffer = new byte[1+1+AttachInfoLength];
            Buffer.Span.WriteLittle(AttachInfoId, 0);
            Buffer.Span.WriteLittle(AttachInfoLength, 1);
            Buffer.Span.WriteLittle(WiFiSignalStrength, 2);
        }
    }
}
