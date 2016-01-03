using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitterClient.FileManager
{
    public class FileOpen : IDisposable
    {
        private Stream stream;

        public Stream Stream
        {
            get { return stream; }
            set { stream = value; }
        }

        public FileOpen(string path)
        {
            Stream = new FileStream(path, FileMode.Open);
        }

        public void Dispose()
        {
            Stream.Close();
        }
    }
}
