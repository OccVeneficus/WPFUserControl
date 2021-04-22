using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MainModel;

namespace MainVM
{
    /// <summary>
    /// View Model для главного окна
    /// </summary>
    public class MainVM : ViewModelBase
    {
        private ObservableCollection<FileItem> _filesList = new ObservableCollection<FileItem>();

        private readonly IOpenFileDialogService _openFileDialogService;

        private FileItem _currentFileItem;

        private RelayCommand<FileItem> _deleteCommand;

        private RelayCommand _addCommand;

        /// <summary>
        /// Коллекция добавленных файлов
        /// </summary>
        public ObservableCollection<FileItem> FileList
        {
            get => _filesList;
            set => Set(ref _filesList, value);
        }

        /// <summary>
        /// Текущий выбранный файл
        /// </summary>
        public FileItem CurrentFileItem
        {
            get => _currentFileItem;
            set => Set(ref _currentFileItem, value);
        }

        /// <summary>
        /// Ищет ошибку для последнего добавленного файла
        /// </summary>
        public bool HasErrors => _filesList[_filesList.Count - 1].HasErrors;

        /// <summary>
        /// Команда для удаления файла из списка
        /// </summary>
        public RelayCommand<FileItem> DeleteCommand
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand<FileItem>(obj =>
                {
                    FileList.Remove(obj);
                }));
            }
        }


        /// <summary>
        /// Команда для добавления файла в список
        /// </summary>
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(() =>
                {
                    if (_openFileDialogService.ShowDialog("ChooseFile") == true)
                    {
                        FileList.Add(new FileItem(_openFileDialogService.FileName));
                    }
                }));
            }
        }

        /// <summary>
        /// Конструктор с сервисами 
        /// </summary>
        /// <param name="openFileDialogService">Сервис окна выбора файла</param>
        public MainVM(IOpenFileDialogService openFileDialogService)
        {
            _openFileDialogService = openFileDialogService;
        }
    }
}
