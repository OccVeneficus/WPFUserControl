using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using GalaSoft.MvvmLight.CommandWpf;
using MainModel;
using MainVM.Annotations;

namespace MainVM
{
    public class MainVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        private ObservableCollection<FileItem> _fileItems = new ObservableCollection<FileItem>();

        private IFileWindowService _fileWindowService;

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

        public MainVM(IFileWindowService fileWindowService)
        {
            _fileWindowService = fileWindowService;
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
                    ClearErrors(nameof(FileItem));
                }));
            }
        }

        private RelayCommand _addCommand;

        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ?? (_addCommand = new RelayCommand(() =>
                {
                    if (_fileWindowService.ShowDialog("ChooseFile") == true)
                    {
                        FileList.Add(new FileItem(_fileWindowService.FileName));
                        ValidateFileName(FileList[FileList.Count-1]);
                    }
                }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidateFileName(FileItem file)
        {
            var extension = Path.GetExtension(file.Name);
            if (extension != ".dll" && extension != ".exe")
            {
                AddError(nameof(FileItem),file.Name);
            }
        }

        public IEnumerable GetErrors(string propertyName)
        {
            return _errorsByPropertyName.ContainsKey(propertyName) ?
                _errorsByPropertyName[propertyName] : null;
        }

        /// <summary>
        /// Словарь для хранения ошибок проверки свойств 
        /// </summary>
        private readonly Dictionary<string, List<string>> _errorsByPropertyName =
            new Dictionary<string, List<string>>();

        /// <summary>
        /// Добавить ошибку в словарь
        /// </summary>
        /// <param name="propertyName">Имя проверяемого свойства</param>
        /// <param name="fileName">Описание ошибки</param>
        private void AddError(string propertyName, string fileName)
        {
            if (!_errorsByPropertyName.ContainsKey(propertyName))
                _errorsByPropertyName[propertyName] = new List<string>();

            if (!_errorsByPropertyName[propertyName].Contains(fileName))
            {
                _errorsByPropertyName[propertyName].Add(fileName);
                OnErrorsChanged(propertyName);
            }
        }

        /// <summary>
        /// Очистить словарь ошибок
        /// </summary>
        /// <param name="propertyName"></param>
        private void ClearErrors(string propertyName)
        {
            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public bool HasErrors => _errorsByPropertyName.Any(x=>x.Key == nameof(FileItem) && x.Value.Contains(_fileItems[_fileItems.Count-1].Name));


        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
    }
}
