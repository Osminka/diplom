using diplom.Models.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace diplom.Interfaces
{
    public interface IFilesService
    {
        string StorageDirectory { get; }
        void DownloadFile(string url, string folder);
        event EventHandler<DownloadEventArgs> OnFileDownloaded;
        void OpenFile(string path);
    }
}
