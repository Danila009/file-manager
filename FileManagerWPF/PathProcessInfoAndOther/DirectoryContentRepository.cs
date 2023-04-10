using System.Collections.ObjectModel;

namespace FileManagerWPF
{
    public class DirectoryContentRepository
    {
        private ObservableCollection<DirectoryContent> _directoryContent;

        public DirectoryContentRepository()
        {
            _directoryContent = new ObservableCollection<DirectoryContent>();
        }

        public ObservableCollection<DirectoryContent> DirectoryContent
        {
            get { return _directoryContent; }
            set { _directoryContent = value; }
        }
    }
}
