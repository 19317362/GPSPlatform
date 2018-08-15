﻿using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.MessageBody.JT808_0x8900_0x0900_Body;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.MessageBody
{
    /// <summary>
    /// 数据上行透传
    /// </summary>
    [JT808Formatter(typeof(JT808_0x0900Formatter))]
    public class JT808_0x0900 : JT808Bodies
    {
        /// <summary>
        /// 透传消息类型
        /// </summary>
        public byte PassthroughType { get; set; }

        /// <summary>
        /// 透传消息内容
        /// </summary>
        public JT808_0x0900_BodyBase JT808_0x0900_BodyBase { get; set; }
    }
}
