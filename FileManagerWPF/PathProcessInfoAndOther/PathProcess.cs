using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace FileManagerWPF
{
    public static class PathProcess
    {
        public static List<DriveInfo> GetDrives()
        {
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            List<DriveInfo> newAllDrives = new List<DriveInfo>();
            foreach (var driveInfo in allDrives)
            {
                if (driveInfo.IsReady)
                {
                    newAllDrives.Add(driveInfo);
                }
            }
            return newAllDrives;
        }

        public static List<DirectoryInfo> GetDirectories(String path)
        {
            if (path == "" || path == null)
            {
                return null;
            }
            if (Path.HasExtension(path))
            {
                path = Path.GetDirectoryName(path);
            }
            return new DirectoryInfo(path).GetDirectories().ToList();
        }

        public static List<FileInfo> GetFiles(String path)
        {
            if (path == "" || path == null)
            {
                return null;
            }
            if (Path.HasExtension(path))
            {
                path = Path.GetDirectoryName(path);
            }
            return new DirectoryInfo(path).GetFiles().ToList();
        }

        public static DirectoryContentRepository GetDirectoryContentRepository(String path, bool full)
        {
            DirectoryContentRepository newDirectoryContentRepository = new DirectoryContentRepository();
            DirectoryContent directoryContent;
            if (path == "" || path == null)
            {
                var driveList = GetDrives();
                newDirectoryContentRepository = new DirectoryContentRepository();
                foreach (var driveInfo in driveList)
                {
                    directoryContent = new DirectoryContent
                    {
                        Name = driveInfo.Name,
                        TotalSize = driveInfo.TotalSize,
                        FreeSpace = driveInfo.AvailableFreeSpace,
                        Icon = new BitmapImage(new Uri(@"icons/drive.png", UriKind.Relative))
                    };
                    newDirectoryContentRepository.DirectoryContent.Add(directoryContent);
                }
                return newDirectoryContentRepository;
            }
            var directoriesList = GetDirectories(path);
            foreach (var directoryInfo in directoriesList)
            {
                directoryContent = new DirectoryContent
                {
                    Name = directoryInfo.Name,
                    LastWriteTime = directoryInfo.LastWriteTime,
                    Icon = new BitmapImage(new Uri(@"icons/folder.png", UriKind.Relative))
                };
                newDirectoryContentRepository.DirectoryContent.Add(directoryContent);
            }
            if (full)
            {
                var filesList = GetFiles(path);
                foreach (var file in filesList)
                {
                    directoryContent = new DirectoryContent
                    {
                        Name = file.Name,
                        LastWriteTime = file.LastWriteTime,
                        TotalSize = file.Length
                    };

                    switch (file.Extension)
                    {
                        case ".mp3":
                        case ".mp2":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/mp3_file.png", UriKind.Relative));
                            break;
                        case ".exe":
                        case ".com":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/exe_file.png", UriKind.Relative));
                            break;

                        case ".mp4":
                        case ".avi":
                        case ".mkv":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/mp4_file.png", UriKind.Relative));
                            break;
                        case ".pdf":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/pdf_file.png", UriKind.Relative));
                            break;
                        case ".doc":
                        case ".docx":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/doc_file.png", UriKind.Relative));
                            break;
                        case ".png":
                        case ".jpg":
                        case ".jpeg":
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/image_file.png", UriKind.Relative));
                            break;

                        default:
                            directoryContent.Icon = new BitmapImage(new Uri(@"icons/file.png", UriKind.Relative));
                            break;
                    }

                    newDirectoryContentRepository.DirectoryContent.Add(directoryContent);
                }
            }            
            return newDirectoryContentRepository;
        }
    }
}
