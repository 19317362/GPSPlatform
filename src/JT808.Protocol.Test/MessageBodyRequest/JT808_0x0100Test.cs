﻿using Xunit;
using JT808.Protocol.MessageBodyRequest;
using Protocol.Common.Extensions;
using MessagePack;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0100Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808Package jT808_0X0100 = new JT808Package();
            jT808_0X0100.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.终端注册,
                 MsgNum=10,
                 TerminalPhoneNo="123456789",
            };
            jT808_0X0100.Bodies = new JT808_0x0100
            {
                 AreaID=40,
                 CityOrCountyId=50,
                 MakerId="1234",
                 PlateColor=1,
                 PlateNo="粤A12345",
                 TerminalId="CHI123",
                 TerminalType="smallchi123"
            };
            var hex = MessagePackSerializer.Serialize(jT808_0X0100).ToHexString();
        }

        [Fact]
        public void Test1_1()
        {
                byte[] bytes = "7E 01 00 00 2D 00 01 23 45 67 89 00 0A 00 28 00 32 31 32 33 34 30 73 6D 61 6C 6C 63 68 69 31 32 33 30 30 30 30 30 30 30 30 30 43 48 49 31 32 33 30 01 D4 C1 41 31 32 33 34 35 BA 7E".ToHexBytes();
                JT808Package jT808_0X0100 = MessagePack.MessagePackSerializer.Deserialize<JT808Package>(bytes);
                Assert.Equal(Enums.JT808MsgId.终端注册, jT808_0X0100.Header.MsgId);
                Assert.Equal(10, jT808_0X0100.Header.MsgNum);
                Assert.Equal("000123456789", jT808_0X0100.Header.TerminalPhoneNo);

                JT808_0x0100 JT808Bodies = (JT808_0x0100)jT808_0X0100.Bodies;
                Assert.Equal(40, JT808Bodies.AreaID);
                Assert.Equal(50, JT808Bodies.CityOrCountyId);
                Assert.Equal("12340", JT808Bodies.MakerId);
                Assert.Equal(1, JT808Bodies.PlateColor);
                Assert.Equal("粤A12345", JT808Bodies.PlateNo);
                Assert.Equal("CHI1230", JT808Bodies.TerminalId);
                Assert.Equal("smallchi123000000000", JT808Bodies.TerminalType);
        }
    }
}
