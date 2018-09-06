using System.IO;

namespace LibaryDocumentGenerator.Documents.FileLogica
{
    /// <summary>
    /// Класс очистки старых документов
    /// </summary>
   public class FileLogica
    {
        /// <summary>
        /// Удаление старых докуметов по пути сохранения
        /// </summary>
        /// <param name="path">Путь сохранения</param>
        public void FileDelete(string path)
        {
          DirectoryInfo dir = new DirectoryInfo(path);
            
            foreach (var file in dir.GetFiles())
            {
                File.Delete(file.FullName);
            }
        }
    }
}
