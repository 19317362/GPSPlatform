﻿using JT808.Protocol.Enums;
using JT808.Protocol.MessageBodyReply;
using System;
using System.Collections.Generic;
using System.Text;

namespace JT808.Protocol.JT808PackageImpl.Reply
{
    /// <summary>
    /// 终端注册应答
    /// </summary>
    public class JT808_0x8100Package : JT808PackageBase<JT808_0x8100>
    {
        public JT808_0x8100Package(JT808Header jT808Header, ushort msgNum, JT808_0x8100 bodies) : base(jT808Header, msgNum, bodies)
        {
        }

        protected override JT808Package Create(JT808Header jT808Header, ushort msgNum, JT808_0x8100 bodies)
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header();
            jT808Package.Header.MsgId = JT808MsgId.终端注册应答;
            jT808Package.Header.MsgNum = msgNum;
            jT808Package.Header.TerminalPhoneNo = jT808Header.TerminalPhoneNo;
            jT808Package.Bodies = bodies;
            return jT808Package;
        }
    }
}
