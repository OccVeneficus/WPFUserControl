using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.CommandWpf;
using MainModel;
using MainVM.Annotations;

namespace MainVM
{
    public class MainVM : INotifyPropertyChanged
    {
        private ObservableCollection<FileItem> _fileItems;

        public ObservableCollection<FileItem> FileList
        {
            get => _fileItems;
            set
            {
                _fileItems = value;
                OnPropertyChanged(nameof(FileList));
            }
        }

        private FileItem _currentFileItem;

        public FileItem CurrentFileItem
        {
            get => _currentFileItem;
            set
            {
                _currentFileItem = value;
                OnPropertyChanged(nameof(CurrentFileItem));
            }
        }

        public MainVM()
        {
            FileList = new ObservableCollection<FileItem>
            {
                new FileItem("text1.txt"),
                new FileItem("text2.txt"),
                new FileItem("text3.txt"),
                new FileItem("text4.txt")
            };
        }

        private RelayCommand<string> _deleteCommand;

        public RelayCommand<string> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand<string>(obj =>
                {
                    var toDelete = FileList.Where(o=> o.Name.Equals(obj)).ToList();
                    FileList.Remove(toDelete[0]);
                }));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(() => {FileList.Add(new FileItem("123"));}));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
