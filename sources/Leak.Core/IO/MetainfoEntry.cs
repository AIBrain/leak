﻿using System;
using System.IO;
using Leak.Core.Bencoding;

namespace Leak.Core.IO
{
    public class MetainfoEntry
    {
        private readonly BencodedValue data;

        public MetainfoEntry(BencodedValue data)
        {
            this.data = data;
        }

        public string Name
        {
            get { return GetNameInSingleFileMode() ?? GetNameInMultipleFileMode(); }
        }

        public long Size
        {
            get
            {
                return data.Find("length", x => x.ToInt64());
            }
        }

        private string GetNameInSingleFileMode()
        {
            return data.Find("name", x => x.ToText());
        }

        private string GetNameInMultipleFileMode()
        {
            return data.Find("path", path =>
            {
                string[] items = data.AllTexts();
                string separator = Path.DirectorySeparatorChar.ToString();

                return String.Join(separator, items);
            });
        }
    }
}