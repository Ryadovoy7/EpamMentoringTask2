using System.Collections;
using System.IO;

namespace Task2.FileSystemVisitor.Lib
{
    public class FileSystemVisitor : IEnumerable
    {     
        private string _startPath;
        private FSVFilter? _filter;

        struct PathEnumerationState
        {
            public string Path;
            public bool Stop;
        }

        public event EventHandler? Start;
        public event EventHandler? Finish;

        public event FileSystemVisitorEventHandler? FileFinded;
        public event FileSystemVisitorEventHandler? DirectoryFinded;
        public event FileSystemVisitorEventHandler? FilteredFileFinded;
        public event FileSystemVisitorEventHandler? FilteredDirectoryFinded;

        private List<string> _pathsFoundList;
        public List<string> PathsFoundList
        {
            get { return new List<string>(_pathsFoundList); }
        }

        public FileSystemVisitor(string startPath)
        {
            _startPath = startPath;
            _pathsFoundList = new List<string>();
        }

        public FileSystemVisitor(string startPath, FSVFilter filter) : this(startPath)
        {
            _filter = filter;
        }

        public IEnumerator GetEnumerator()
        {
            Start?.Invoke(this, new EventArgs());
            if (Directory.Exists(_startPath))
            {
                foreach (PathEnumerationState path in TraverseDirectoryTree(_startPath))
                {
                    if (path.Stop)
                        break;
                    else
                        yield return path.Path;
                }
            }
            Finish?.Invoke(this, new EventArgs());
            yield break;
        }

        private IEnumerable<PathEnumerationState> TraverseDirectoryTree(string dir)
        {
            // ищем директории
            string[]? directories = null; 
            bool inaccessible = false;
            try
            {
                directories = Directory.GetDirectories(dir);
            }
            catch (UnauthorizedAccessException)
            {
                inaccessible = true;
            }
            if (inaccessible)
            {
                yield return new PathEnumerationState() { Path = $"The directory \"{dir}\" is inaccessible.", Stop = false };
                yield break;
            }

            foreach (string directory in directories)
            {
                foreach (PathEnumerationState pathState in TraverseDirectoryTree(directory))
                    yield return pathState;
            }
            foreach (PathEnumerationState pathState in ProcessFoundPaths(directories, DirectoryFinded, FilteredDirectoryFinded))
                yield return pathState;

            // ищем файлы
            var files = Directory.GetFiles(dir);
            foreach (PathEnumerationState pathState in ProcessFoundPaths(files, FileFinded, FilteredFileFinded))
                yield return pathState;
        }

        private IEnumerable<PathEnumerationState> ProcessFoundPaths(IEnumerable<string> paths,
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
                    yield return new PathEnumerationState { Path = path, Stop = true };

                if (_filter != null)
                {
                    bool filterResult = _filter(path);
                    if (filterResult)
                    {
                        var filteredFoundArgs = new FileSystemVisitorEventArgs(path);
                        actionFiltered?.Invoke(this, filteredFoundArgs);
                        if (filteredFoundArgs.RemoveFromList)
                            continue;
                        if (filteredFoundArgs.Stop)
                            yield return new PathEnumerationState { Path = path, Stop = true };
                    }
                    else
                        continue;
                }

                _pathsFoundList.Add(path);
                yield return new PathEnumerationState { Path = path, Stop = false };
            }
        }


    }

    public delegate bool FSVFilter(string path);

    public delegate void FileSystemVisitorEventHandler(object sender, FileSystemVisitorEventArgs e);
}

