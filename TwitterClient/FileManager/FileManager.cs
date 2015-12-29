using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.FileManager
{
    public class FileManager
    {

        private StreamWriter stream;

        public StreamWriter Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        public FileManager(String path)
        {
            Stream = System.IO.File.CreateText(path);
        }

        public void close()
        {
            Stream.Close();
        }

    }
}