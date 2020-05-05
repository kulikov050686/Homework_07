using Newtonsoft.Json;
using System.ComponentModel;
using System.IO;

namespace Homework_07
{
    /// <summary>
    /// Класс загрузки и выгрузки данных из файла
    /// </summary>
    class FileIOService
    {
        /// <summary>
        /// Путь к файлу
        /// </summary>
        private readonly string PathFile;

        /// <summary>
        /// Конструктор сохранения открытия файла
        /// </summary>
        /// <param name="path"> Путь к файлу </param>
        public FileIOService(string path)
        {
            PathFile = path;
        }

        /// <summary>
        /// Загрузить данные в лист из файла
        /// </summary>        
        public BindingList<NoteModel> LoadDataList()
        {
            var fileExists = File.Exists(PathFile);

            if(!fileExists)
            {
                File.CreateText(PathFile).Dispose();
                return new BindingList<NoteModel>();
            }

            using (var reader =File.OpenText(PathFile))
            {
                var fileTaxt = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<BindingList<NoteModel>>(fileTaxt);
            }                
        }

        /// <summary>
        /// Сохранить лист в файл
        /// </summary>
        public void SaveDataList(BindingList<NoteModel> listToSave)
        {
            using (StreamWriter writer = File.CreateText(PathFile))
            {
                string output = JsonConvert.SerializeObject(listToSave);
                writer.Write(output);
            }
        }        
    }
}
