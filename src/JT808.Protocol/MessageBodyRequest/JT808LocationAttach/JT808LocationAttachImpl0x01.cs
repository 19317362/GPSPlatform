﻿using System.Runtime.Serialization;
using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters.JT808LocationAttach;

namespace JT808.Protocol.MessageBodyRequest.JT808LocationAttach
{

    [JT808Formatter(typeof(JT808_0x0200_0x01Formatter))]
    public class JT808LocationAttachImpl0x01 : JT808LocationAttachBase
    {
        public override byte AttachInfoId { get;  set; } = 0x01;
        public override byte AttachInfoLength { get;  set; } = 4;
        /// <summary>
        /// 里程
        /// </summary>
        public int Mileage { get; set; }
        /// <summary>
        /// 里程 1/10km，对应车上里程表读数
        /// </summary>
        [IgnoreDataMember]
        public double ConvertMileage => Mileage / 10;
    }
}
