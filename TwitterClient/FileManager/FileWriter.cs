using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.FileManager
{
    public class FileWriter : IDisposable
    {

        private StreamWriter stream;

        public StreamWriter Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        public FileWriter(string path, string name)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            Stream = System.IO.File.CreateText(string.Format("{0}/{1}", path, name));
        }

        public FileWriter(string path)
        {
            Stream = System.IO.File.CreateText(path);
        }

        public void close()
        {
            Stream.Close();
        }

        public void Dispose()
        {
            Stream.Close();
        }
    }
}