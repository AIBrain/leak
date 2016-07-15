﻿using Leak.Core.Network;

namespace Leak.Core.Negotiator
{
    public class HandshakeCryptoMessage : NetworkOutgoingMessage
    {
        public static readonly int MinimumSize = 2;

        public static int GetSize(NetworkIncomingMessage message)
        {
            return 2 + message[0] * 256 + message[1];
        }

        public int Length
        {
            get { return 2; }
        }

        public byte[] ToBytes()
        {
            return Bytes.Parse("0000");
        }
    }
}