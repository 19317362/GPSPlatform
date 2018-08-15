﻿using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;
using System.Runtime.Serialization;

namespace JT808.Protocol.MessageBody.JT808LocationAttach
{

    [JT808Formatter(typeof(JT808_0x0200_0x03Formatter))]
    public class JT808LocationAttachImpl0x03 : JT808LocationAttachBase
    {
        /// <summary>
        /// 行驶记录功能获取的速度
        /// </summary>
        public ushort Speed { get; set; }
        /// <summary>
        /// 行驶记录功能获取的速度 1/10km/h
        /// </summary>
        [IgnoreDataMember]
        public double ConvertSpeed => Speed / 10.0;
        public override byte AttachInfoId { get;  set; } = 0x03;
        public override byte AttachInfoLength { get;  set; } = 2;
    }
}
