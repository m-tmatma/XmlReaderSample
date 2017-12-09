using System;
using System.Xml;
using System.Collections.Generic;
#if DEBUG
using System.Diagnostics;
#endif

namespace XmlReaderSample
{
    public class XmlSampleParser
    {
        /// <summary>
        /// remove the last item from a list and return it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns>removed item</returns>
        private T PopList<T>(List<T> list)
        {
            var last = list[list.Count - 1];
            list.RemoveAt(list.Count - 1);
            return last;
        }

        /// <summary>
        /// Parse and dump a xml file
        /// </summary>
        /// <param name="url">url to a xml file</param>
        public void Dump(string url)
        {
            using (var reader = XmlReader.Create(url))
            {
#if DEBUG
                var level = 0;
#endif
                var tag = "";
                var tags = new List<string>();
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.WriteLine("<" + reader.Name + ">");
                            tags.Add(reader.Name);
#if DEBUG
                            level++;
#endif
                            while (reader.MoveToNextAttribute())
                            {
                                Console.WriteLine(reader.Name + ":" + reader.Value);
                            }
                            reader.MoveToElement();

                            if (reader.IsEmptyElement)
                            {
                                tag = PopList(tags);
                                Console.WriteLine("</" + tag + ">");
#if DEBUG
                                level--;
#endif
                            }
                            break;
                        case XmlNodeType.Text:
                            Console.WriteLine(reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                        case XmlNodeType.ProcessingInstruction:
                            Console.WriteLine(reader.Name, reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            break;
                        case XmlNodeType.EndElement:
                            tag = PopList(tags);
                            Console.WriteLine("</" + tag + ">");
#if DEBUG
                            Debug.Assert(String.Equals(tag, reader.Name));
                            level--;
#endif
                            break;
                        case XmlNodeType.Whitespace:
                            break;
                        default:
                            break;
                    }
                }
#if DEBUG
                Console.WriteLine(level.ToString());
#endif
            }
        }
    }
}
