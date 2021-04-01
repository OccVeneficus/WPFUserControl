using System.ComponentModel;
using System.Runtime.CompilerServices;
using MainModel.Annotations;

namespace MainModel
{
    /// <summary>
    /// Класс, представляющий файл
    /// </summary>
    public class FileItem : INotifyPropertyChanged
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
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public FileItem()
        {

        }

        public FileItem(string name)
        {
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
