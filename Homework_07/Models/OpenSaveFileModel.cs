using System;
using System.ComponentModel;

namespace Homework_07
{
    /// <summary>
    /// Модель сохранения и открытия файла 
    /// </summary>
    public class OpenSaveFileModel
    {
        /// <summary>
        /// Дата создания файла
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Лист с данными
        /// </summary>
        public BindingList<NoteModel> ListData { get; set; }

        /// <summary>
        /// Суммарная прибыль
        /// </summary>
        public double TotalIncome { get; set; }

        /// <summary>
        /// Конструктор модели
        /// </summary>
        public OpenSaveFileModel()
        {
            if(ListData == null)
            {
                ListData = new BindingList<NoteModel>();
            }           
        }
    }
}
