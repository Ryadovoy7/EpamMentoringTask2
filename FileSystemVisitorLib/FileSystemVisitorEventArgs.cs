namespace Task2.FileSystemVisitor.Lib
{ 
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

