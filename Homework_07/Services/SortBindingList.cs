using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Homework_07
{
    public static class SortBindingList
    {        
        static BindingList<NoteModel> TempBindingList;
        
        /// <summary>
        /// Сортировка листа по названию актива
        /// </summary>
        /// <param name="bList"> Сортируемый лист </param>
        static public void SortAssetName(BindingList<NoteModel> bList)
        {
            TempBindingList = new BindingList<NoteModel>();
            IEnumerable<NoteModel> e = bList.OrderBy(man => man.AssetName);

            TempBindingList.Clear();

            foreach (NoteModel my in e)
            {
                TempBindingList.Add(my);
                bList.Remove(my);
            }

            foreach (NoteModel my in TempBindingList)
            {
                bList.Add(my);
            }
        }

        /// <summary>
        /// Сортировка листа по дате создания записей
        /// </summary>
        /// <param name="bList"> Сортируемый лист </param>
        static public void SortDate(BindingList<NoteModel> bList)
        {
            TempBindingList = new BindingList<NoteModel>();
            IEnumerable<NoteModel> e = bList.OrderBy(man => man.Date);

            TempBindingList.Clear();

            foreach (NoteModel my in e)
            {
                TempBindingList.Add(my);
                bList.Remove(my);
            }

            foreach (NoteModel my in TempBindingList)
            {
                bList.Add(my);
            }
        }

        /// <summary>
        /// Сортировка листа по полученному доходу
        /// </summary>        
        static public void SortIncome(BindingList<NoteModel> bList)
        {
            TempBindingList = new BindingList<NoteModel>();
            IEnumerable<NoteModel> e = bList.OrderBy(man => man.Income);

            TempBindingList.Clear();

            foreach (NoteModel my in e)
            {
                TempBindingList.Add(my);
                bList.Remove(my);
            }

            foreach (NoteModel my in TempBindingList)
            {
                bList.Add(my);
            }
        }
    }
}
