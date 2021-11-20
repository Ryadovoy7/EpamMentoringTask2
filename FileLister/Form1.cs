using FileSystemVisitorLib;

namespace FileLister
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textboxResult.Clear();

            FileSystemVisitor fsv;
            if (String.IsNullOrEmpty(textboxMustContain.Text))
                fsv = new FileSystemVisitor(textboxStartingPoint.Text);
            else
                fsv = new FileSystemVisitor(textboxStartingPoint.Text, str => str.Contains(textboxMustContain.Text));
            
            if(checkboxAddEventTexts.Checked)
            {
                fsv.Start += (o, e) => textboxResult.AppendText("Start\r\n");
                fsv.Finish += (o, e) => textboxResult.AppendText("Finish\r\n");

                fsv.DirectoryFinded += (o, e) => textboxResult.AppendText($"DirectoryFinded: {e.Path}\r\n");
                fsv.FileFinded += (o, e) => textboxResult.AppendText($"FileFinded: {e.Path}\r\n");
                fsv.FilteredDirectoryFinded += (o, e) => textboxResult.AppendText($"FilteredDirectoryFinded: {e.Path}\r\n");
                fsv.FilteredFileFinded += (o, e) => textboxResult.AppendText($"FilteredFileFinded: {e.Path}\r\n");
            }

            int count = 0;
            if (checkboxStopAfter.Checked)
                fsv.FileFinded += (o,e) => { if (count >= updownStopAfter.Value) e.Stop = true; };

            foreach (var path in fsv)
            {
                textboxResult.AppendText(path + "\r\n");
                count++;
            }
        }
    }
}