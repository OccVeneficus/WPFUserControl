using MainVM;
using Microsoft.Win32;

namespace MainView
{
    public class OpenFileDialogService : IOpenFileDialogService
    {
        public string FileName { get; set; }
        public bool? ShowDialog(string title)
        {
            var fileDialog = new OpenFileDialog
            {
                Title = title
            };
            
            if (fileDialog.ShowDialog() == true)
            {
                FileName = fileDialog.SafeFileName;
                return true;
            }
            return false;
        }
    }
}
