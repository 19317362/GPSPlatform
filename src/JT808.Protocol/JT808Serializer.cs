﻿using JT808.Protocol.Attributes;
using JT808.Protocol.JT808Formatters;
using JT808.Protocol.JT808Resolvers;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace JT808.Protocol
{
    public static class JT808Serializer
    {
        static IJT808FormatterResolver defaultResolver;

        public static IJT808FormatterResolver DefaultResolver
        {
            get
            {
                if (defaultResolver == null)
                {
                    defaultResolver = JT808StandardResolver.Instance;
                }
                return defaultResolver;
            }
        }

        public static bool IsInitialized
        {
            get
            {
                return defaultResolver != null;
            }
        }

        public static byte[] Serialize<T>(T obj)
        {
            return Serialize(obj, defaultResolver);
        }

        public static byte[] Serialize<T>(T obj, IJT808FormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatter<T>();
            // ref https://www.cnblogs.com/TianFang/p/9193881.html
            var pool = MemoryPool<byte>.Shared;
            var buffer = pool.Rent(65536);
            try
            {
                var len = formatter.Serialize(buffer.Memory.Span, 0, obj, DefaultResolver);
                return buffer.Memory.Slice(0, len).ToArray();
            }
            finally
            {
                buffer.Dispose();
            }
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes)
        {
            return Deserialize<T>(bytes, defaultResolver);
        }

        public static T Deserialize<T>(ReadOnlySpan<byte> bytes, IJT808FormatterResolver resolver)
        {
            if (resolver == null) resolver = DefaultResolver;
            var formatter = resolver.GetFormatter<T>();
            int readSize;
            return formatter.Deserialize(bytes, 0, resolver, out readSize);
        }
    }
}
