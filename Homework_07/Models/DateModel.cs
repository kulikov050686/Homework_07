using System;

namespace Homework_07
{
    /// <summary>
    /// Модель даты
    /// </summary>
    public static class DateModel
    {
        static DateTime dateTimeNow;        

        /// <summary>
        /// День
        /// </summary>
        public static int Day { get => dateTimeNow.Day; }

        /// <summary>
        /// Месяц
        /// </summary>
        public static int Month { get => dateTimeNow.Month; }

        /// <summary>
        /// Год
        /// </summary>
        public static int Year { get => dateTimeNow.Year; }

        /// <summary>
        /// Конструктор
        /// </summary>
        static DateModel()
        {
            dateTimeNow = DateTime.Now;
        }        

        /// <summary>
        /// Вывод даты в виде строки
        /// </summary>        
        public new static string ToString()
        {
            dateTimeNow = DateTime.Now;
            string strDay;
            string strMonth;

            if (dateTimeNow.Day < 10)
            {
                strDay = "0" + dateTimeNow.Day.ToString();
            }
            else
            {
                strDay = dateTimeNow.Day.ToString();
            }

            if (dateTimeNow.Month < 10)
            {
                strMonth = "0" + dateTimeNow.Month.ToString();
            }
            else
            {
                strMonth = dateTimeNow.Month.ToString();
            }

            return strDay + "." + strMonth + "." + dateTimeNow.Year.ToString();
        }
    }
}
