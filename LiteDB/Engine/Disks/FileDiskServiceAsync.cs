using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteDB.Engine.Disks
{
    public class FileDiskServiceAsync : FileDiskService
    {
        public FileDiskServiceAsync(String filename, bool journal = true) : base(filename, journal) { }

        public override FileStream CreateFileStream(string path, System.IO.FileMode mode, FileAccess access, FileShare share, int bufferSize)
        {
            return syncOverAsync(() =>
            {
                return new FileStream(path, mode, access, share, bufferSize);
            });
        }

        private T syncOverAsync<T>(Func<T> f)
        {
            return Task.Run<T>(f).ConfigureAwait(false).GetAwaiter().GetResult();
        }
    }
}
