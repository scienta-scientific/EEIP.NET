﻿using System;
using Sres.Net.EEIP.CIP.Path;
using Sres.Net.EEIP.Data;

namespace Sres.Net.EEIP.CIP.ObjectLibrary
{
    /// <summary>
    /// Message router request. CIP specification 2-4.1.
    /// </summary>
    public record MessageRouterRequest :
        Byteable
    {
        public MessageRouterRequest(byte service, EPath path, IByteable data = null)
        {
            Service = service;
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Data = data ?? Bytes.Empty;
        }

        /// <summary>
        /// Service code
        /// </summary>
        public byte Service { get; init; }
        /// <summary>
        /// Request path
        /// </summary>
        public EPath Path { get; }
        /// <summary>
        /// Request data
        /// </summary>
        public IByteable Data { get; }

        public sealed override ushort ByteCount => (ushort)(1 + Path.ByteCount + Data.ByteCount);

        protected sealed override void DoToBytes(byte[] bytes, ref int index)
        {
            bytes[index++] = Service;
            Path.ToBytes(bytes, ref index);
            Data.ToBytes(bytes, ref index);
        }
    }

}
