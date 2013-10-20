using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DDSExtractor
{
    class BinaryFileHandler
    {

        // filestream
        protected readonly FileStream fs;
        protected readonly BinaryReader reader;
        protected readonly string path;

        public BinaryFileHandler(string path, bool writable = false)
        {
            this.path = path;
            fs = new FileStream(path, FileMode.Open, writable ? FileAccess.ReadWrite : FileAccess.Read);
            reader = new BinaryReader(fs);
        }
        

        ~BinaryFileHandler() {
            reader.Close();
            fs.Close();
            fs.Dispose();
        }

    }
}
