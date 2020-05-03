using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Homework_07
{
    /// <summary>
    /// Класс записи в записной книжке
    /// </summary>
    public class NoteModel : INotifyPropertyChanged
    {        
        string assetName;
        uint lotVolume;
        uint numberOfLots;
        double purchasePrice;
        double sellingPrice;
        double income;

        public static readonly DependencyProperty PriceProperty;

        /// <summary>
        /// Название актива
        /// </summary>
        public string AssetName 
        {
            get 
            {
                return assetName;
            }
            set 
            {
                if(assetName == value)
                {
                    return;
                }

                assetName = value;
                OnPropertyChanged("AssetName");
                OnPropertyChanged("Income");
            } 
        }

        /// <summary>
        /// Объём лота
        /// </summary>
        public uint LotVolume 
        { 
            get
            {
                return lotVolume;
            }
            set 
            {
                if(lotVolume == value)
                {
                    return;
                }

                lotVolume = value;
                OnPropertyChanged("LotVolume");
                OnPropertyChanged("Income");
            } 
        }

        /// <summary>
        /// Количество лотов
        /// </summary>
        public uint NumberOfLots
        {
            get 
            {
                return numberOfLots;
            }
            set
            {
                if(numberOfLots == value)
                {
                    return;
                }

                numberOfLots = value;
                OnPropertyChanged("NumberOfLots");
                OnPropertyChanged("Income");
            }            
        }

        /// <summary>
        /// Цена покупки
        /// </summary>
        public double PurchasePrice
        {
            get 
            {
                return purchasePrice;
            }
            set 
            {
                if(purchasePrice == value)
                {
                    return;
                }

                purchasePrice = value;
                OnPropertyChanged("PurchasePrice");
                OnPropertyChanged("Income");
            }
        }

        /// <summary>
        /// Цена продажи
        /// </summary>
        public double SellingPrice
        {
            get 
            {
                return sellingPrice;
            }
            set
            {
                if(sellingPrice == value)
                {
                    return;
                }

                sellingPrice = value;
                OnPropertyChanged("SellingPrice");
                OnPropertyChanged("Income");
            } 
        }

        /// <summary>
        /// Полученный доход
        /// </summary>
        public double Income
        {
            get 
            {
                income = Math.Round(lotVolume * numberOfLots * (sellingPrice - purchasePrice), 4);                
                return income;
            }            
        }

        /// <summary>
        /// Конструктор модели
        /// </summary>
        public NoteModel()
        {
            assetName = "";
            lotVolume = 0;
            numberOfLots = 0;
            purchasePrice = 0;
            sellingPrice = 0;
            income = 0;
        }

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
