using System.Collections.Generic;
using System.ComponentModel;

namespace Util
{
    public sealed class ObservableList<T> : List<T>
    {
        public event ListChangedEventHandler ListChanged;

        private void OnListChanged(ListChangedEventArgs e)
        {
            ListChanged?.Invoke(this, e);
        }
        
        public new void Add(T item)
        {
            base.Add(item);
            OnListChanged(new ListChangedEventArgs(ListChangedType.ItemAdded, this.Count - 1));
        }
        
        public new bool Remove(T item)
        {
            var index = IndexOf(item);
            if (index >= 0)
            {
                OnListChanged(new ListChangedEventArgs(ListChangedType.ItemDeleted, index));
            }
            
            var removed = base.Remove(item);

           
            return removed;
        }
    }
}
