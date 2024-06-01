using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Model
{

    public class Tag : INotifyPropertyChanged
    {
        public string _Tag { get; set; }
        public int Id { get; set; }
        public bool IsSelected { get; set; }
        public bool IsEmpty => !String.IsNullOrEmpty(_Tag);

        public Tag()
        {
            _Tag = "";
        }

        public Tag(string tag)
        {
            _Tag = tag;
        }


        public override bool Equals(object obj)
        {
            var item = obj as Tag;
            if (item == null) return false;
            return this._Tag.Equals(item._Tag);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

}
