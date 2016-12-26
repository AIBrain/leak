﻿using System.Collections.Generic;
using Leak.Bencoding;
using Leak.Common;
using Leak.Communicator.Messages;
using Leak.Extensions;

namespace Leak.Glue
{
    public class GlueFacts
    {
        private int? pieces;
        private readonly MoreContainer more;

        public GlueFacts(GlueConfiguration configuration)
        {
            more = new MoreContainer();
            pieces = configuration.Pieces;
        }

        public void Install(IReadOnlyCollection<MorePlugin> plugins)
        {
            foreach (MorePlugin plugin in plugins)
            {
                plugin.Install(more);
            }
        }

        public void ApplyPieces(int value)
        {
            pieces = value;
        }

        public Bitfield ApplyHave(Bitfield bitfield, int piece)
        {
            if (bitfield == null && pieces != null)
            {
                bitfield = new Bitfield(pieces.Value);
            }

            if (bitfield != null && bitfield.Length > piece)
            {
                bitfield[piece] = true;
            }

            return bitfield;
        }

        public Bitfield ApplyBitfield(Bitfield bitfield, Bitfield received)
        {
            if (pieces != null)
            {
                received = new Bitfield(pieces.Value, received);
            }

            return received;
        }

        public Extended GetHandshake()
        {
            BencodedValue encoded = more.Encode();
            byte[] binary = Bencoder.Encode(encoded);

            return new Extended(0, binary);
        }

        public string[] GetExtensions()
        {
            return more.ToArray();
        }

        public string Translate(byte id, out MoreHandler handler)
        {
            string code = more.Translate(id);
            handler = more.GetHandler(code);

            return code;
        }

        public MoreHandler GetHandler(string code)
        {
            return more.GetHandler(code);
        }
    }
}