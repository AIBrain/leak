﻿using System;
using System.IO;
using System.Linq;
using Leak.Client.Swarm;
using Pargos;

namespace Leak
{
    public class Options
    {
        [Parameter, At(0)]
        public string Command { get; set; }

        [Option("--trackers")]
        public string[] Trackers { get; set; }

        [Option("--hash")]
        public string Hash { get; set; }

        [Option("--destination")]
        public string Destination { get; set; }

        [Option("--listener")]
        public string Listener { get; set; }

        [Option("--port")]
        public string Port { get; set; }

        [Option("--connector")]
        public string Connector { get; set; }

        [Option("--accept")]
        public string[] Accept { get; set; }

        public bool IsValid()
        {
            Uri uri;
            int port;

            switch (Command)
            {
                case "download":
                    break;

                default:
                    return false;
            }

            if (Trackers == null || Trackers.Length == 0)
                return false;

            if (Trackers.All(x => Uri.TryCreate(x, UriKind.Absolute, out uri)) == false)
                return false;

            if (Hash?.Length != 40)
                return false;

            if (Destination == null || Path.IsPathRooted(Destination) == false)
                return false;

            if (Directory.Exists(Destination) == false)
                return false;

            if (Port != null)
            {
                if (Int32.TryParse(Port, out port) == false)
                    return false;

                if (port <= 0 || port > 65535)
                    return false;
            }

            if (Listener != null)
            {
                if (Listener != "on" && Listener != "off")
                    return false;
            }

            if (Connector != null)
            {
                if (Connector != "on" && Connector != "off")
                    return false;
            }

            if (Accept != null)
            {
                if (Accept.Length == 0)
                    return false;

                if (Accept.Any(x => x.Length != 2))
                    return false;
            }

            return true;
        }

        public SwarmSettings ToSettings()
        {
            SwarmSettings settings = new SwarmSettings();

            if (Connector != null)
            {
                settings.Connector = Connector == "on";
            }

            if (Listener != null)
            {
                settings.Listener = Listener == "on";
            }

            if (settings.Listener && Port != null)
            {
                settings.ListenerPort = Int32.Parse(Port);
            }

            if (Accept != null)
            {
                settings.Filter = new GeonFilter(Accept);
            }

            return settings;
        }
    }
}