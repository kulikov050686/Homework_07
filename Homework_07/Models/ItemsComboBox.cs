namespace Homework_07
{
    public class ItemsComboBox : BaseModel
    {
        string nameItem;
        string numberItem;

        /// <summary>
        /// Название пункта
        /// </summary>
        public string NameItem 
        {
            get 
            { 
                return nameItem; 
            }
            set 
            {
                if(nameItem == value)
                {
                    return;
                }

                nameItem = value;
                OnPropertyChanged("NameItem");
            } 
        }

        /// <summary>
        /// Номер пункта
        /// </summary>
        public string NumberItem
        {
            get
            {
                return numberItem;
            }
            set
            {
                if(numberItem == value)
                {
                    return;
                }

                numberItem = value;
                OnPropertyChanged("NumberItem");
            }
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ItemsComboBox()
        {
            nameItem = "";
            numberItem = "";
        }
    }
}
