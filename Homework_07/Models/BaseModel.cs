using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_07
{
    public class BaseModel : INotifyPropertyChanged
    {
        public static readonly DependencyProperty PriceProperty;

        /// <summary>
        /// Событие появляется при изменении значения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Обработка события
        /// </summary>
        /// <param name="parameter"> Имя изменяемого свойства </param>
        public void OnPropertyChanged([CallerMemberName]string parameter = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(parameter));
            }
        }
    }
}
