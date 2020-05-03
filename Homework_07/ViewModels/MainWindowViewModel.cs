using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Homework_07
{
    /// <summary>
    /// Модель представление основного окна
    /// </summary>
    public class MainWindowViewModel : ViewModelBase
    {
        #region Закрытые поля

        const string title = "Блокнот трейдера";
        private string PATH;
        BindingList<NoteModel> dataInNotebookList;
        string profit;
        ICommand openFileClick;
        ICommand saveFileClick;

        #endregion

        #region Открытые поля

        /// <summary>
        /// Название приложения
        /// </summary>
        public string Title
        {
            get
            {
                return title;
            }
        }

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
        /// Вывод даты
        /// </summary>
        public string DateNow
        {
            get
            {
                return DateModel.ToString();
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
        public ICommand OpenFileClick
        {
            get
            {
                return openFileClick ?? (openFileClick = new RelayCommand(CommandOpenFile));
            }
        }

        /// <summary>
        /// Команда сохранеия в файл
        /// </summary>
        public ICommand SaveFileClick
        {
            get
            {
                return saveFileClick ?? (saveFileClick = new RelayCommand(CommandSaveFile));
            }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор Модели представления
        /// </summary>
        public MainWindowViewModel()
        {
            DataInNotebookList = new BindingList<NoteModel>();
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
            OpenSaveFileModel openSaveFile = new OpenSaveFileModel();

            openSaveFile.Date = DateTime.Now;
            openSaveFile.ListData = DataInNotebookList;
            openSaveFile.TotalIncome = TotalIncome();

            FileIOService fileIOService = new FileIOService(PATH);

            try
            {
                fileIOService.SaveData(openSaveFile);
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
            OpenSaveFileModel openSaveFile = new OpenSaveFileModel();

            try
            {
                openSaveFile = fileIOService.LoadData();
                DataInNotebookList = openSaveFile.ListData;
                profit = openSaveFile.TotalIncome.ToString();

                dataInNotebookList.ListChanged += DataInNotebookList_ListChanged;

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
        private void CommandOpenFile()
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
        private void CommandSaveFile()
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
            }
        }
    }
}
