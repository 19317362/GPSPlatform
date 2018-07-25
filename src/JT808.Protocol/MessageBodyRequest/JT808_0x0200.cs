﻿using System;
using System.Collections.Generic;
using JT808.Protocol.JT808Formatters.MessageBodyFormatters;
using JT808.Protocol.JT808RequestProperties;
using JT808.Protocol.MessageBodyRequest.JT808LocationAttach;
using MessagePack;
using Protocol.Common.Extensions;

namespace JT808.Protocol.MessageBodyRequest
{
    /// <summary>
    /// 位置信息汇报
    /// </summary>
    [MessagePackObject]
    [MessagePackFormatter(typeof(JT808_0x0200Formatter))]
    public class JT808_0x0200 : JT808Bodies
    {
        public JT808_0x0200()
        {
        }

        public JT808_0x0200(Memory<byte> buffer) : base(buffer)
        {
        }

        /// <summary>
        /// 报警标志 
        /// </summary>
        [Key(0)]
        public int AlarmFlag { get; set; }
        /// <summary>
        /// 状态位标志
        /// </summary>
        [Key(1)]
        public int StatusFlag { get; set; }
        /// <summary>
        /// 纬度
        /// 以度为单位的纬度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        [Key(2)]
        public int Lat { get; set; }
        /// <summary>
        /// 经度
        /// 以度为单位的经度值乘以 10 的 6 次方，精确到百万分之一度
        /// </summary>
        [Key(3)]
        public int Lng { get; set; }
        /// <summary>
        /// 高程
        /// 海拔高度，单位为米（m）
        /// </summary>
        [Key(4)]
        public ushort Altitude { get; set; }
        /// <summary>
        /// 速度 1/10km/h
        /// </summary>
        [Key(5)]
        public ushort Speed { get; set; }
        /// <summary>
        /// 方向 0-359，正北为 0，顺时针
        /// </summary>
        [Key(6)]
        public ushort Direction { get; set; }
        /// <summary>
        /// YY-MM-DD-hh-mm-ss（GMT+8 时间，本标准中之后涉及的时间均采用此时区）
        /// </summary>
        [Key(7)]
        public DateTime GPSTime { get; set; }
        /// <summary>
        /// 位置附加信息
        /// </summary>
        [IgnoreMember]
        public IDictionary<byte, JT808LocationAttachBase> JT808LocationAttachData { get; set; }

        public override void ReadBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            AlarmFlag = Buffer.Span.ReadIntH2L(0, 4);
            StatusFlag =Buffer.Span.ReadIntH2L(4, 4);
            Lat = Buffer.Span.ReadIntH2L(8, 4);
            Lng = Buffer.Span.ReadIntH2L(12, 4);
            JT808StatusProperty jT808StatusProperty = new JT808StatusProperty(Convert.ToString(StatusFlag, 2).PadLeft(32, '0'));
            if (jT808StatusProperty.Bit28 == '1')//西经
            {
                Lng = -Lng;
            }
            if (jT808StatusProperty.Bit29 == '1')//南纬
            {
                Lat = -Lat;
            }
            Altitude = (ushort)Buffer.Span.ReadIntH2L(16, 2);
            Speed = (ushort)Buffer.Span.ReadIntH2L(18, 2);
            Direction = (ushort)Buffer.Span.ReadIntH2L(20, 2);
            GPSTime = Buffer.Span.ReadDateTimeLittle(22, 6);
            //JT808AlarmProperty jT808AlarmProperty = new JT808AlarmProperty(Convert.ToString(AlarmFlag, 2).PadLeft(32, '0'));
            // 位置附加信息
            JT808LocationAttachData = new Dictionary<byte, JT808LocationAttachBase>();
            if (Buffer.Length > 28)
            {
                Span<byte> locationAttachSpan = Buffer.Span.Slice(28);
                int offset = 0;
                while (locationAttachSpan.Length>offset)
                {
                    try
                    {
                        Type jT808LocationAttachType;
                        if (JT808LocationAttachBase.JT808LocationAttachMethod.TryGetValue(locationAttachSpan[offset], out jT808LocationAttachType))
                        {
                            int attachId = 1;
                            int attachLen = 1;
                            int attachContentLen = locationAttachSpan[offset+1];
                            int locationAttachTotalLen = attachId+ attachLen+ attachContentLen;
                            Memory<byte> tempData = locationAttachSpan.Slice(offset, locationAttachTotalLen).ToArray(); 
                            JT808LocationAttachBase jT808LocationAttachImpl = (JT808LocationAttachBase)Activator.CreateInstance(jT808LocationAttachType, tempData);
                            jT808LocationAttachImpl.ReadBuffer(jT808GlobalConfigs);
                            offset = offset + locationAttachTotalLen;
                            JT808LocationAttachData.Add(jT808LocationAttachImpl.AttachInfoId, jT808LocationAttachImpl);
                        }
                        else
                        {
                            break;
                        }
                    }
                    catch(Exception ex)
                    {
                        continue;
                    }
                }
            }
        }

        public override void WriteBuffer(JT808GlobalConfigs jT808GlobalConfigs)
        {
            Memory<byte> buffer1 = new byte[28];
            buffer1.Span.WriteLittle(AlarmFlag, 0, 4);
            buffer1.Span.WriteLittle(StatusFlag, 4, 4);
            buffer1.Span.WriteLatLng(Lat, 8);
            buffer1.Span.WriteLatLng(Lng, 12);
            buffer1.Span.WriteLittle(Altitude, 16,2);
            buffer1.Span.WriteLittle((int)(Speed * 10.0), 18, 2);
            buffer1.Span.WriteLittle(Direction, 20, 2);
            buffer1.Span.WriteLittle(GPSTime, 22);
            List<byte> attachBytes = new List<byte>();
            attachBytes.AddRange(buffer1.ToArray());
            if (JT808LocationAttachData!=null && JT808LocationAttachData.Count > 0)
            {
                foreach(var item in JT808LocationAttachData)
                {
                    try
                    {
                        item.Value.WriteBuffer(jT808GlobalConfigs);
                        attachBytes.AddRange(item.Value.Buffer.ToArray());
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }
            Buffer = attachBytes.ToArray();
        }
    }
}
