namespace MainVM
{
    /// <summary>
    /// Интерфейс сервиса для показа окна выбора файла
    /// </summary>
    public interface IFileWindowService
    {
        /// <summary>
        /// Имя выбранного файла
        /// </summary>
        string FileName { get; set; }

        /// <summary>
        /// Показывает окно пользователю
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <returns>Результат диалога</returns>
        bool? ShowDialog(string title);
    }
}
