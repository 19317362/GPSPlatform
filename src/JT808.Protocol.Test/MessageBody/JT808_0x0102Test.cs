
using Xunit;
using JT808.Protocol.MessageBody;
using JT808.Protocol.Extensions;

namespace JT808.Protocol.Test.MessageBodyRequest
{
    public class JT808_0x0102Test: JT808PackageBase
    {
        [Fact]
        public void Test1()
        {
            JT808_0x0102 jT808LoginRequestProperty = new JT808_0x0102();
            jT808LoginRequestProperty.Code = "45612";
            string hex= JT808Serializer.Serialize(jT808LoginRequestProperty) .ToHexString();
        }

        [Fact]
        public void Test2()
        {
            byte[] bodys = "34 35 36 31 32".ToHexBytes();
            JT808_0x0102 jT808LoginRequest = JT808Serializer.Deserialize<JT808_0x0102>(bodys);
            Assert.Equal("45612", jT808LoginRequest.Code);
        }

        [Fact]
        public void Test3()
        {
            JT808Package jT808LoginRequest = new JT808Package();
            jT808LoginRequest.Header = new JT808Header
            {
                 MsgId= Enums.JT808MsgId.�ն˼�Ȩ,
                 MsgNum=12345,
                 TerminalPhoneNo="12345678900",
            };
            jT808LoginRequest.Bodies = new JT808_0x0102
            {
                 Code= "456121111"
            };
            string hex = JT808Serializer.Serialize(jT808LoginRequest).ToHexString();
        }

        [Fact]
        public void Test4()
        {
            byte[] bodys = "7E 01 02 00 09 01 23 45 67 89 00 30 39 34 35 36 31 32 31 31 31 31 BE 7E".ToHexBytes();
            JT808Package jT808LoginRequest = JT808Serializer.Deserialize<JT808Package>(bodys);
            Assert.Equal(Enums.JT808MsgId.�ն˼�Ȩ, jT808LoginRequest.Header.MsgId);
            Assert.Equal(12345, jT808LoginRequest.Header.MsgNum);
            Assert.Equal("12345678900", jT808LoginRequest.Header.TerminalPhoneNo);

            JT808_0x0102 JT808Bodies = (JT808_0x0102)jT808LoginRequest.Bodies;
            Assert.Equal("456121111", JT808Bodies.Code);
        }
    }
}
