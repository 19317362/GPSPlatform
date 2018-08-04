﻿using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{
    [JT808Formatter(typeof(JT808_0x0200_0x31Formatter))]
    public class JT808LocationAttachImpl0x31 : JT808LocationAttachBase
    {
        /// <summary>
        /// GNSS 定位卫星数
        /// </summary>
        public byte GNSSCount { get; set; }
        public override byte AttachInfoId { get;  set; } = 0x31;
        public override byte AttachInfoLength { get;  set; } = 1;
    }
}
