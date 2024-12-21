using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_Clicker.Helpers
{
    public class FileManager
    {
        private readonly string _filePath;

        public FileManager(string filePath)
        {
            _filePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
        }

        public void WriteRecord(int record)
        {
            try
            {
                using (var writer = new StreamWriter(_filePath, append: false))
                {
                    writer.Write(record.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Ошибка записи в файл!", ex);
            }
        }

        public int ReadRecord()
        {
            try
            {
                using (var reader = new StreamReader(_filePath))
                {
                    var content = reader.ReadLine();
                    return int.TryParse(content, out int record) ? record : 0;
                }
            }
            catch (Exception ex)
            {
                throw new IOException("Ошибка чтения из файла!", ex);
            }
        }
    }
}
