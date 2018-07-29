﻿using JT808.Protocol.Enums;
using JT808.Protocol.JT808Formatters;
using MessagePack;

namespace JT808.Protocol
{
    /// <summary>
    /// 头部
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808HeaderFormatter))]
    public class JT808Header
    {
        /// <summary>
        /// 消息ID 
        /// </summary>
        [Key(0)]
        public JT808MsgId MsgId { get; set; }
        [Key(1)]
        public JT808MessageBodyProperty MessageBodyProperty { get; set; } = new JT808MessageBodyProperty();
        /// <summary>
        /// 终端手机号
        /// 根据安装后终端自身的手机号转换。手机号不足 12 位，则在前补充数字，大陆手机号补充数字 0，港澳台则根据其区号进行位数补充
        /// </summary>
        [Key(2)]
        public string TerminalPhoneNo { get;  set; }
        /// <summary>
        /// 消息流水号
        /// 发送计数器
        /// 占用四个字节，为发送信息的序列号，用于接收方检测是否有信息的丢失，上级平台和下级平台接自己发送数据包的个数计数，互不影响。
        /// 程序开始运行时等于零，发送第一帧数据时开始计数，到最大数后自动归零
        /// </summary>
        [Key(3)]
        public ushort MsgNum { get;  set; }
    }
}
