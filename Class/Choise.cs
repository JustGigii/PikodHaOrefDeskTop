using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikodAorfLayout.Class
{
    class Choise
    {
        public static Choise choiselist = new Choise();
        List<String> _items;
        public Choise()
        {
            _items = new List<String>();
        }
        public List<String> Items
        {
            get { return _items; }
        }
        public void AddChoise(String item) 
        {
            if (!_items.Contains(item)) { _items.Add(item); _items.Sort(); }
        }
        public void RemoveChoise(String item)
        {
            if(_items.Contains(item))
            {
                _items.Remove(item);
            }
        }
        public void Clear()
        {
            _items.Clear();
        }
 
    }
}
