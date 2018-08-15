﻿using System;
using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;


namespace JT808.Protocol.MessageBody.JT808LocationAttach
{
    [JT808Formatter(typeof(JT808_0x0200_0x30Formatter))]
    public class JT808LocationAttachImpl0x30 : JT808LocationAttachBase
    {
        /// <summary>
        /// 无线通信网络信号强度
        /// </summary>
        public byte WiFiSignalStrength { get; set; }
        public override byte AttachInfoId { get;  set; } = 0x30;
        public override byte AttachInfoLength { get;  set; } = 1;
    }
}
