using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using RadioPlayer.Infrastructure;
using RadioPlayer.Models;
using System.Windows.Input;

    //Exmple to save into Appdata 
  //    http://www.codeproject.com/Tips/343730/Persist-your-objects-settings-in-XML
 //
namespace RadioPlayer.ViewModels
{
    public class MainViewModel : ViewModelBase
    {


        private readonly DialogService dialogService;
        private RadioChannel selectedRadioChannel;
        private bool isPlaying;
        private MediaState mediaState;
       // private MediaElement mediaElement;
   

        public MainViewModel()
        {
            dialogService = new DialogService();

            RadioChannels = LoadRadioChannels();
        }


      //  http://stackoverflow.com/questions/867485/c-sharp-getting-the-path-of-appdata 

        public string AppDataFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Channel.xml");

        public ObservableCollection<RadioChannel> RadioChannels { get; set; }

        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged();
            }
        }
      
        public MediaState MediaState
        {
            get { return mediaState; }
            set
            {
                mediaState = value;             
                OnPropertyChanged();
            }
        }

        public RadioChannel SelectedRadioChannel
        {
            get { return selectedRadioChannel; }
            set
            {
                selectedRadioChannel = value;
                OnPropertyChanged();
                StartPlayback();
            }
        }


        private void StartPlayback()
        {
            MediaState = MediaState.Play;
            IsWorking = true;
            IsPlaying = true;
        }

        public void BufferingEnded()
        {
            IsWorking = false;
        }

        /*
        
            */
        private ObservableCollection<RadioChannel> LoadRadioChannels()
        {
            //Loading From appdata channel 
            if (File.Exists(AppDataFile))
            {
                using (var reader = new StreamReader(AppDataFile))
                {
                    var serializer = new XmlSerializer(typeof(List<RadioChannel>));
                    var deserializedData = (List<RadioChannel>) serializer.Deserialize(reader);

                    return new ObservableCollection<RadioChannel>(deserializedData);
                }
            }

            //add 2 radios channel 

            return new ObservableCollection<RadioChannel>
            {
                new RadioChannel
                {
                    ChannelName = "Rix FM",
                    ChannelUri = "http://stream-ice.mtgradio.com:8080/stat_rix_fm"
                },
                new RadioChannel
                {
                    ChannelName = "NRJ",
                    ChannelUri = "http://194.16.21.232:8000/nrj_ext_se_mp3"
                }
            };
        }

        public void AddNewRadioChannel()
        {
            var channel = new RadioChannel
            {
                ChannelName = string.Empty,

            };

            var channelViewModel = new ChannelViewModel {RadioChannel = channel, DialogService = dialogService};
            
            if (dialogService.ShowDialog(channelViewModel) == true)
            {
                RadioChannels.Add(channel);
                SaveRadioChannels();
            }
        }
       
        public void DeleteRadioChannel()
        {
            if (SelectedRadioChannel != null)
            {
                var result = dialogService.ShowMessageBox("Do you want to remove this channel?", "Remove channel", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    RadioChannels.Remove(SelectedRadioChannel);
                    SaveRadioChannels();
                }
            }
        }

        public void ChangePlaybackState()
        {
            if (SelectedRadioChannel != null)
            {
                if (MediaState == MediaState.Play)
                {
                    MediaState = MediaState.Close;
                    IsPlaying = false;
                }
                else
                {
                    StartPlayback();
                }
            }
            else
            {
                IsPlaying = false;
            }
        }

          private void SaveRadioChannels()
          {
            
              using (var writer = new StreamWriter(AppDataFile))
              {
                  var serializer = new XmlSerializer(typeof(List<RadioChannel>));
                  serializer.Serialize(writer, RadioChannels.ToList());
              }
          }
          
    }
}
