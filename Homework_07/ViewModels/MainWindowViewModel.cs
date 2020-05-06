using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace Homework_07
{
    /// <summary>
    /// Модель представление основного окна
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Закрытые поля
                
        private string PATH;
        BindingList<NoteModel> dataInNotebookList;
        string profit;        
        bool changeFile;
        ItemsComboBox currentSelection;
        RelayCommand openFileClick;
        RelayCommand saveFileClick;
        RelayCommand closeApplication;
        RelayCommand selectedSort;       

        #endregion

        #region Открытые поля        

        /// <summary>
        /// Название приложения
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Лист записей
        /// </summary>
        public BindingList<NoteModel> DataInNotebookList
        {
            get
            {
                if (dataInNotebookList == null)
                {
                    dataInNotebookList = new BindingList<NoteModel>();
                }
                
                return dataInNotebookList;
            }
            set
            {
                if (dataInNotebookList == null)
                {
                    dataInNotebookList = new BindingList<NoteModel>();
                }

                dataInNotebookList = value;
                RaisePropertyChanged(() => DataInNotebookList);
            }
        }
        
        /// <summary>
        /// Вывод суммарного дохода
        /// </summary>
        public string Profit
        {
            get
            {
                return profit;
            }
            private set
            {
                profit = value;
                RaisePropertyChanged(() => Profit);
            }
        }
       
        /// <summary>
        /// Команда открытия файла
        /// </summary>
        public RelayCommand OpenFileClick
        {
            get
            {
                return openFileClick ?? (openFileClick = new RelayCommand(CommandOpenFile));
            }
        }

        /// <summary>
        /// Команда сохранеия в файл
        /// </summary>
        public RelayCommand SaveFileClick
        {
            get
            {
                return saveFileClick ?? (saveFileClick = new RelayCommand(CommandSaveFile));
            }
        }

        /// <summary>
        /// Команда закрытия приложения
        /// </summary>
        public RelayCommand CloseApplication 
        {
            get 
            {
                return closeApplication ?? (closeApplication = new RelayCommand(Close)); 
            }
        }

        /// <summary>
        /// Команда выбора сортировки листа
        /// </summary>
        public RelayCommand SelectedSort
        {
            get 
            {
                return selectedSort ?? (selectedSort = new RelayCommand(Sort));
            } 
        }

        /// <summary>
        /// Выбор пункта
        /// </summary>
        public ItemsComboBox CurrentSelection 
        {
            get 
            { 
                return currentSelection; 
            }
            set 
            { 
                currentSelection = value;
                RaisePropertyChanged(() => CurrentSelection);
            } 
        }

        /// <summary>
        /// Список названий пунктов
        /// </summary>
        public List<ItemsComboBox> Items { get; } = new List<ItemsComboBox>
        {
            new ItemsComboBox { NameItem = "Дате", NumberItem = "0"},
            new ItemsComboBox { NameItem = "Названию актива", NumberItem = "1"},
            new ItemsComboBox { NameItem = "Полученному доходу", NumberItem = "2"}
        };

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Модели представления
        /// </summary>
        public MainWindowViewModel()
        {
            Title = "Блокнот трейдера";            

            DataInNotebookList = new BindingList<NoteModel>();           
            changeFile = false;

            dataInNotebookList.ListChanged += DataInNotebookList_ListChanged;            
        }

        #endregion

        /// <summary>
        /// Событие вызываемое при обновлении листа
        /// </summary>        
        private void DataInNotebookList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded ||
                e.ListChangedType == ListChangedType.ItemDeleted ||
                e.ListChangedType == ListChangedType.ItemChanged)
            {
                Profit = TotalIncome().ToString();
                changeFile = true;
            }           
        }

        /// <summary>
        /// Рачёт суммарного дохода
        /// </summary>        
        private double TotalIncome()
        {
            if (DataInNotebookList == null)
            {
                return 0;
            }

            double sum = 0;

            foreach (NoteModel temp in DataInNotebookList)
            {
                sum += temp.Income;
            }

            return sum;
        }
                
        /// <summary>
        /// Запись в файл
        /// </summary>        
        private bool SaveFile()
        {
            FileIOService fileIOService = new FileIOService(PATH);

            try
            {
                fileIOService.SaveDataList(DataInNotebookList);
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Чтение из файла
        /// </summary>        
        private bool LoadFile()
        {
            FileIOService fileIOService = new FileIOService(PATH);           

            try
            {
                if(DataInNotebookList.Count == 0)
                {
                    DataInNotebookList = fileIOService.LoadDataList();
                }
                else
                {
                    BindingList<NoteModel> Temp = new BindingList<NoteModel>();

                    Temp = fileIOService.LoadDataList();

                    foreach(NoteModel note in Temp)
                    {
                        DataInNotebookList.Add(note);
                    }
                }
                
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Открытие диалогового окна для чтения из файла
        /// </summary>
        private void CommandOpenFile(object obj)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();

            openFileDialog.Title = "Открыть файл";
            openFileDialog.Filter = "files (*.json)|*.json";
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (openFileDialog.ShowDialog() == true)
            {
                PATH = openFileDialog.FileName;

                if (!LoadFile())
                {
                    Application.Current.Shutdown();
                }

                Profit = TotalIncome().ToString();
                DataInNotebookList.ListChanged += DataInNotebookList_ListChanged;
            }
        }

        /// <summary>
        /// Открытие диалогового окна для записи в файл
        /// </summary>
        private void CommandSaveFile(object obj)
        {
            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();

            saveFileDialog.Title = "Сохранить файл";
            saveFileDialog.Filter = "files (*.json)|*.json";
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;

            if (saveFileDialog.ShowDialog() == true)
            {
                PATH = saveFileDialog.FileName;

                if (!SaveFile())
                {
                    Application.Current.Shutdown();
                }

                changeFile = false;
            }
        }        

        /// <summary>
        /// Закрытие приложения
        /// </summary>        
        private void Close(object obj)
        {
            if(DataInNotebookList.Count != 0 && changeFile)
            {
                if(MessageBoxResult.OK == MessageBox.Show("Сохранить изменения в файле?", "Сообщение", MessageBoxButton.OKCancel))
                {
                    CommandSaveFile(null);
                }                
            }
        }
        
        /// <summary>
        /// Сортировка листа по дате создания записей
        /// </summary>        
        private void Sort(object obj)
        {
            if(DataInNotebookList != null)
            {
                switch (CurrentSelection.NumberItem)
                {
                    case "0":
                        SortBindingList.SortDate(DataInNotebookList);
                        break;
                    case "1":
                        SortBindingList.SortAssetName(DataInNotebookList);
                        break;
                    case "2":
                        SortBindingList.SortIncome(DataInNotebookList);
                        break;
                }
            }            
        }        
    }
}
