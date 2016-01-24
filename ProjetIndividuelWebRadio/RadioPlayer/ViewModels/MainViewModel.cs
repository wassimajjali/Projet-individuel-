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

/*
      Exemple sur lequel je me suis appuyé pour faire la persistence et pouvoir sauvgarder et charger les radios channel
      http://www.codeproject.com/Tips/343730/Persist-your-objects-settings-in-XML
 */
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

 /*
        http://stackoverflow.com/questions/867485/c-sharp-getting-the-path-of-appdata 
        on utilise les variable d'environement pour faire la persistence 
 */
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
            //on charge à partir du ficher de la variable d'env les channels deja ajouté
            if (File.Exists(AppDataFile))
            {
                using (var reader = new StreamReader(AppDataFile))
                {
                    var serializer = new XmlSerializer(typeof(List<RadioChannel>));
                    var deserializedData = (List<RadioChannel>) serializer.Deserialize(reader);

                    return new ObservableCollection<RadioChannel>(deserializedData);
                }
            }

            //ajouter les 2 radios qui seront par defauts dans la liste

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


        //on sauvgagrde les channels ajoutés aupravant dans App_Data File de notre application
        //ce fichier peu etre changé 
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
