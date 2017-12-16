using System.Collections.Generic;
using System.Linq;
using IMAPI2;

namespace CDBurn
{
    class FileManager
    {
        private readonly List<FileNode> _fileToBurningList = new List<FileNode>();

        public IReadOnlyCollection<FileNode> FileToBurningList => _fileToBurningList.AsReadOnly();

        public long FilesSize => _fileToBurningList.Sum(file => file.SizeOnDisc);
            
        public long DiscSize { get; set; }

        public void AddNewFiles(string[] fileNames)
        {
            foreach (var path in fileNames)
            {
                if (_fileToBurningList.Any(x => x.Path.Equals(path))) continue;
                var file = new FileNode(path);
                if (FilesSize + file.SizeOnDisc <= DiscSize)
                {
                    _fileToBurningList.Add(file);
                }
            }
        }

        public void RemoveFiles()
        {
            _fileToBurningList.Clear();
        }

        public void RemoveFile(int number)
        {
            _fileToBurningList.RemoveAt(number);
        }
    }
}
