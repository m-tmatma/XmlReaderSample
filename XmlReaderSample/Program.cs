﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XmlReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var sampleParser = new XmlSampleParser();

            sampleParser.Dump(args[0]);
        }
    }
}
