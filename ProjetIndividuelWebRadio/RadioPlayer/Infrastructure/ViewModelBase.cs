using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RadioPlayer.Infrastructure
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private string displayName;
        private bool isWorking;

        public bool IsWorking
        {
            get { return isWorking; }
            set
            {
                isWorking = value;
                OnPropertyChanged();
            }
        }

        public string DisplayName
        {
            get { return displayName; }
            set
            {
                displayName = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            handler?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
