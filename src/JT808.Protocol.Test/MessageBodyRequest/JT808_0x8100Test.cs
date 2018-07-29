﻿using System.Text;
using Xunit;
using JT808.Protocol.Extensions;
using JT808.Protocol.MessageBodyReply;
using MessagePack;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public  class JT808_0x8100Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.终端注册应答,
                MsgNum = 10,
                TerminalPhoneNo = "012345678900",
            };
            jT808Package.Bodies = new JT808_0x8100
            {
                Code="123456",
                 JT808TerminalRegisterResult =  Enums.JT808TerminalRegisterResult.成功,
                MsgNum = 100
            };
            //"7E 
            //81 00 
            //00 09 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64 
            //00 
            //31 32 33 34 35 36 
            //68 
            //7E"
            var hex = MessagePackSerializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test2()
        {
            var bytes = "7E 81 00 00 09 01 23 45 67 89 00 00 0A 00 64 00 31 32 33 34 35 36 68 7E".ToHexBytes();
            JT808Package jT808Package = MessagePackSerializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端注册应答, jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("012345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Equal("123456", JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.成功, JT808Bodies.JT808TerminalRegisterResult);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.终端注册应答,
                MsgNum = 10,
                TerminalPhoneNo = "012345678900",
            };
            jT808Package.Bodies = new JT808_0x8100
            {
                Code = "123456",
                JT808TerminalRegisterResult = Enums.JT808TerminalRegisterResult.数据库中无该终端,
                MsgNum = 100
            };
            //"7E 
            //81 00
            //00 03 
            //01 23 45 67 89 00 
            //00 0A 
            //00 64 
            //04 
            //61 
            //7E"
            var hex = MessagePackSerializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test4()
        {
            var bytes = "7E 81 00 00 03 01 23 45 67 89 00 00 0A 00 64 04 61 7E".ToHexBytes();
            JT808Package jT808Package = MessagePackSerializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端注册应答, jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("012345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Null(JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.数据库中无该终端, JT808Bodies.JT808TerminalRegisterResult);
        }


        [Fact]
        public void Test5()
        {
            JT808Package jT808Package = new JT808Package();
            jT808Package.Header = new JT808Header
            {
                MsgId = Enums.JT808MsgId.终端注册应答,
                MsgNum = 10,
                TerminalPhoneNo = "012345678900",
            };
            jT808Package.Bodies = new JT808_0x8100
            {
                Code = "zssdaf23124sfdsc",
                JT808TerminalRegisterResult = Enums.JT808TerminalRegisterResult.成功,
                MsgNum = 100
            };
            //"7E 81 00 00 13 01 23 45 67 89 00 00 0A 00 64 00 7A 73 73 64 61 66 32 33 31 32 34 73 66 64 73 63 3B 7E"
            var hex = MessagePackSerializer.Serialize(jT808Package).ToHexString();
        }

        [Fact]
        public void Test6()
        {
            var bytes = "7E 81 00 00 13 01 23 45 67 89 00 00 0A 00 64 00 7A 73 73 64 61 66 32 33 31 32 34 73 66 64 73 63 3B 7E".ToHexBytes();
            JT808Package jT808Package = MessagePackSerializer.Deserialize<JT808Package>(bytes);
            Assert.Equal(Enums.JT808MsgId.终端注册应答, jT808Package.Header.MsgId);
            Assert.Equal(10, jT808Package.Header.MsgNum);
            Assert.Equal("012345678900", jT808Package.Header.TerminalPhoneNo);

            JT808_0x8100 JT808Bodies = (JT808_0x8100)jT808Package.Bodies;
            Assert.Equal("zssdaf23124sfdsc", JT808Bodies.Code);
            Assert.Equal(100, JT808Bodies.MsgNum);
            Assert.Equal(Enums.JT808TerminalRegisterResult.成功, JT808Bodies.JT808TerminalRegisterResult);
        }
    }
}
