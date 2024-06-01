using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CookBookAdminApp.Model
{
    public class Step : INotifyPropertyChanged
    {
        public string CookingProcess { get; set; }
        public string FileId { get; set; }
        public string ImageUrl { get; set; }
        public ImageSource Image { get; set; }
        public bool IsEmpty => !String.IsNullOrEmpty(CookingProcess) && Image != null;



        public Step()
        {
            FileId = "";
            CookingProcess = "";
            Image = null;
        }

        public Step(Step step)
        {
            CookingProcess = step.CookingProcess;
            FileId = step.FileId;
            ImageUrl = step.ImageUrl;
            Image = null;
        }

        public void SetFileId()
        {
            FileId = Guid.NewGuid().ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
