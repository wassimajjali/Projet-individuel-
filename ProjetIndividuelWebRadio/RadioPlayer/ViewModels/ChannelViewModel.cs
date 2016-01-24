using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RadioPlayer.Infrastructure;
using RadioPlayer.Models;

namespace RadioPlayer.ViewModels
{
    public class ChannelViewModel : ViewModelBase
    {
        public ChannelViewModel()
        {
            DisplayName = "Add new radio channel";
        }

        public DialogService DialogService { get; set; }

        public RadioChannel RadioChannel { get; set; }

        public void Close()
        {
            DialogService.Close(this, true);
        }
    }
}
