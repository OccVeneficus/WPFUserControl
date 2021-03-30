namespace MainVM
{
    public interface IFileWindowService
    {
        string FileName { get; set; }

        bool? ShowDialog(string title);
    }
}
