using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;

namespace MainModel
{
    /// <summary>
    /// Класс, представляющий файл
    /// </summary>
    public class FileItem : ViewModelBase, INotifyDataErrorInfo
    {
        private string _name;

        /// <summary>
        /// Имя файла
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                Set(ref _name, value);
                var extension = Path.GetExtension(_name);
                if (extension != ".dll" && extension != ".exe")
                {
                    AddError(_name);
                }
            }
        }

        public FileItem()
        {

        }

        public FileItem(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Список для хранения ошибок 
        /// </summary>
        private readonly List<string> _errors =
            new List<string>();

        /// <summary>
        /// Добавить ошибку в список
        /// </summary>
        /// <param name="fileName">Описание ошибки</param>
        private void AddError(string fileName)
        {
            if (!_errors.Contains(fileName))
            {
                _errors.Add(fileName);
                OnErrorsChanged(nameof(FileItem));
            }
        }

        /// <summary>
        /// Очистить список ошибок
        /// </summary>
        /// <param name="propertyName"></param>
        private void ClearErrors(string propertyName)
        {
            if (_errors.Contains(propertyName))
            {
                _errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }

        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return _errors.Contains(propertyName) ?
                _errors : null;
        }

        /// <summary>
        /// Ищет ошибку для последнего добавленного файла
        /// </summary>
        public bool HasErrors => _errors.Any();
    }
}
