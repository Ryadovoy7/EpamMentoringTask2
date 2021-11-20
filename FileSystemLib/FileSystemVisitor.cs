using System.Collections;
using System.IO;

namespace FileSystemLib
{
    public class FileSystemVisitor : IEnumerable
    {
        private string startPath;
        private FSVFilter? filter;

        public event EventHandler? Start;
        public event EventHandler? Finish;

        public event FileSystemVisitorEventHandler? FileFinded;
        public event FileSystemVisitorEventHandler? DirectoryFinded;
        public event FileSystemVisitorEventHandler? FilteredFileFinded;
        public event FileSystemVisitorEventHandler? FilteredDirectoryFinded;

        public FileSystemVisitor(string startPath)
        {
            this.startPath = startPath;
            this.treeListing = new List<string>();
        }

        public FileSystemVisitor(string startPath, FSVFilter? filter) : this(startPath)
        {
            this.filter = filter;
        }

        private List<string> treeListing;

        public IEnumerator GetEnumerator()
        {
            Start?.Invoke(this, null);
            if (Directory.Exists(startPath))
            {
                foreach (PathEnumerationState path in TraverseDirectoryTree(startPath))
                {
                    if (path.stop)
                        break;
                    else
                        yield return path.path;
                }
            }
            Finish?.Invoke(this, null);
            yield break;
        }

        private IEnumerable<PathEnumerationState> TraverseDirectoryTree(string dir)
        {
            // ищем директории
            List<string> directories = Directory.GetDirectories(dir).ToList();
            foreach (string directory in directories)
            {
                foreach (PathEnumerationState path in TraverseDirectoryTree(directory))
                    yield return path;                    
            }
            foreach (PathEnumerationState path in ProcessFoundPaths(directories, DirectoryFinded, FilteredDirectoryFinded))
                yield return path;

            // ищем файлы
            List<string> files = Directory.GetFiles(dir).ToList();
            foreach (PathEnumerationState path in ProcessFoundPaths(files, FileFinded, FilteredFileFinded))
                yield return path;            
        }

        private IEnumerable<PathEnumerationState> ProcessFoundPaths(List<string> paths, 
            FileSystemVisitorEventHandler? actionFound, 
            FileSystemVisitorEventHandler? actionFiltered)
        {
            foreach (string path in paths)
            {
                var foundArgs = new FileSystemVisitorEventArgs(path);
                actionFound?.Invoke(this, foundArgs);

                if (foundArgs.RemoveFromList)
                    continue;
                if (foundArgs.Stop)
                    yield return new PathEnumerationState { path = path, stop = true };

                if (filter != null)
                {
                    bool filterResult = filter(path);
                    if (filterResult)
                    {
                        var filteredFoundArgs = new FileSystemVisitorEventArgs(path);
                        actionFiltered?.Invoke(this, filteredFoundArgs);
                        if (filteredFoundArgs.RemoveFromList)
                            continue;
                        if (filteredFoundArgs.Stop)
                            yield return new PathEnumerationState { path = path, stop = true };
                    }
                    else
                        continue;
                }

                treeListing.Add(path);
                yield return new PathEnumerationState { path = path, stop = false };
            }
        }

        struct PathEnumerationState
        {
            public string path;
            public bool stop;
        }
    }

    public delegate bool FSVFilter(string path);

    public delegate void FileSystemVisitorEventHandler(object sender, FileSystemVisitorEventArgs e);
    public class FileSystemVisitorEventArgs
    {
        public string Path { get; }
        public bool RemoveFromList { get; set; } 
        public bool Stop { get; set; }

        public FileSystemVisitorEventArgs(string path)
        {
            Path = path;
            RemoveFromList = false;
            Stop = false;
        }
    }
}

