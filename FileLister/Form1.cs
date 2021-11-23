using Task2.FileSystemVisitor.Lib;
namespace FileLister
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       
        Task? _fsvTask = null;
        bool _stopFsv = false;

        private void button1_Click(object sender, EventArgs e)
        {
            StopFSVTask();

            textboxResult.Clear();

            _fsvTask = new Task(FindPaths);
            _fsvTask.Start();
        }

        private void FindPaths()
        {
            FileSystemVisitor fsv;
            if (String.IsNullOrEmpty(textboxFilter.Text))
                fsv = new FileSystemVisitor(textboxStartingPoint.Text);
            else
                fsv = new FileSystemVisitor(textboxStartingPoint.Text, str => str.Contains(textboxFilter.Text));

            if (checkboxAddEventTexts.Checked)
            {
                fsv.Start += (o, e) => AppendThreadSafe(textboxResult, "Start\r\n");
                fsv.Finish += (o, e) => AppendThreadSafe(textboxResult, "Finish\r\n");

                fsv.DirectoryFinded += (o, e) => AppendThreadSafe(textboxResult, $"DirectoryFinded: {e.Path}\r\n");
                fsv.FileFinded += (o, e) => AppendThreadSafe(textboxResult, $"FileFinded: {e.Path}\r\n");
                fsv.FilteredDirectoryFinded += (o, e) => AppendThreadSafe(textboxResult, $"FilteredDirectoryFinded: {e.Path}\r\n");
                fsv.FilteredFileFinded += (o, e) => AppendThreadSafe(textboxResult, $"FilteredFileFinded: {e.Path}\r\n");
            }

            int countStopAfter = 0;
            if (checkboxStopAfter.Checked)
                fsv.FileFinded += (o, e) => { if (countStopAfter >= updownStopAfter.Value) e.Stop = true; };

            foreach (var path in fsv)
            {
                AppendThreadSafe(textboxResult, path + "\r\n");

                countStopAfter++;
                if (_stopFsv)
                {
                    _stopFsv = false;
                    break;
                }                   
            }
           
        }

        private void AppendThreadSafe(TextBox textbox, string text)
        {
            if (textbox.InvokeRequired == true)
                textbox.Invoke((MethodInvoker)delegate { textbox.AppendText(text); });
            else
                textbox.AppendText(text);
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            StopFSVTask();
        }

        private async void StopFSVTask()
        {
            if (_fsvTask != null && _fsvTask.Status == TaskStatus.Running)
            {
                _stopFsv = true;
                await _fsvTask;
            }
        }
    }
}