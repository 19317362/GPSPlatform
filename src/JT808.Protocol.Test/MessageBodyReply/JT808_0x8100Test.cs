﻿using System;
using System.Collections.Generic;
using System.Text;
using JT808.Protocol.Enums;
using Xunit;
using JT808.Protocol.MessageBodyReply;
using Protocol.Common.Extensions;

namespace JT808.Protocol.Test.MessageBodyReply
{
    public class JT808_0x8100Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            // "00 0A 00 73 6D 61 6C 6C 63 68 69"
            JT808_0x8100 jT808TerminalRegisterReplyProperty = new JT808_0x8100();
            jT808TerminalRegisterReplyProperty.MsgNum = 10;
            jT808TerminalRegisterReplyProperty.JT808TerminalRegisterResult = JT808TerminalRegisterResult.成功;
            jT808TerminalRegisterReplyProperty.Code = "smallchi";
            jT808TerminalRegisterReplyProperty.WriteBuffer(jT808GlobalConfigs);  
            var hex = jT808TerminalRegisterReplyProperty.Buffer.ToArray().ToHexString();
        }

        [Fact]
        public void Test1_2()
        {
            var bytes = "00 0A 00 73 6D 61 6C 6C 63 68 69".ToHexBytes();
            JT808_0x8100 jT808TerminalRegisterReply = new JT808_0x8100(bytes);
            jT808TerminalRegisterReply.ReadBuffer(jT808GlobalConfigs);
            Assert.Equal(10, jT808TerminalRegisterReply.MsgNum);
            Assert.Equal(JT808TerminalRegisterResult.成功, jT808TerminalRegisterReply.JT808TerminalRegisterResult);
            Assert.Equal("smallchi", jT808TerminalRegisterReply.Code);
        }
    }
}
