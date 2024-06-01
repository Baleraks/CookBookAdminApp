using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Model
{
    public class Ingridients : INotifyPropertyChanged
    {
        public string Ingridient { get; set; }
        public float Calories { get; set; }
        public string Qauntittys { get; set; }
        public bool IsEmpty => !String.IsNullOrEmpty(Ingridient) && !String.IsNullOrEmpty(Qauntittys);
        public string FullDate => Ingridient + " " + Qauntittys + $"- ({Calories}) Кал";
        public string IngredientData => $"{Ingridient} - {Qauntittys}";

        public Ingridients()
        {
            Ingridient = "";
            Calories = 0;
            Qauntittys = "";
        }

        public Ingridients(string ingridient)
        {
            Ingridient = ingridient;
            Qauntittys = "";
            Calories = 0;
        }

        public Ingridients(Ingridients ingridient)
        {
            Ingridient = ingridient.Ingridient;
            Qauntittys = ingridient.Qauntittys;
            Calories = ingridient.Calories;
        }

        public override bool Equals(object obj)
        {
            var item = obj as Ingridients;
            if (item == null) return false;
            return this.Ingridient.Equals(item.Ingridient);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
