﻿using Leak.Core.Common;

namespace Leak.Core.Negotiator
{
    public class Handshake
    {
        private readonly PeerHash local;
        private readonly PeerHash remote;
        private readonly FileHash hash;
        private readonly HandshakeOptions options;

        public Handshake(PeerHash local, PeerHash remote, FileHash hash, HandshakeOptions options)
        {
            this.local = local;
            this.remote = remote;
            this.hash = hash;
            this.options = options;
        }

        public PeerHash Local
        {
            get { return local; }
        }

        public PeerHash Remote
        {
            get { return remote; }
        }

        public FileHash Hash
        {
            get { return hash; }
        }

        public HandshakeOptions Options
        {
            get { return options; }
        }
    }
}