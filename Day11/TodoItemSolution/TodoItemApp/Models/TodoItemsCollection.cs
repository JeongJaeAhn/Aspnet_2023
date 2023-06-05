using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace TodoItemApp.Models
{
    public class TodoItemsCollection : ObservableCollection<TodoItem> // IList<TodoItem> , List<TodoItem>
    {
        public void CopyFrom(IEnumerable<TodoItem> todoItems)
        {
            this.Items.Clear(); // ObservableCollection<T> 자체가 Items 속성을 가지고 있음. 모든 삭제 삭제

            foreach (TodoItem item in todoItems)
            {
                this.Items.Add(item); // 하나씩 다시 추가
            }

            // 데이터바꼈어요! (전부 초기화)
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }
    }
}
