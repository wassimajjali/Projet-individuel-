using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using Microsoft.Win32;
using RadioPlayer.Windows;

namespace RadioPlayer.Infrastructure
{
    /// <summary>
    ///     Service for managing windows and showing common dialogs
    /// </summary>
    public class DialogService 
    {
        private static readonly object lockObject = new object();
        private readonly List<Window> openedWindows = new List<Window>();
        public event EventHandler Closed;

        public event CancelEventHandler Closing;

        public bool? ShowDialog(ViewModelBase viewModel)
        {
            lock (lockObject)
            {
                var window = GetWindow(viewModel, true);
                window.DataContext = viewModel;
                return window.ShowDialog();
            }
        }

        public void ShowWindow(ViewModelBase viewModel)
        {
            lock (lockObject)
            {
                var window = GetWindow(viewModel, false);
                window.DataContext = viewModel;             
                window.Show();
            }
        }

        public void Close(ViewModelBase viewModel, bool dialogResult = false)
        {
            lock (lockObject)
            {
                var window = openedWindows.SingleOrDefault(w => w.DataContext == viewModel);

                if (window != null)
                {
                    if (dialogResult) window.DialogResult = true;

                    openedWindows.Remove(window);
                    window.Close();
                }
            }
        }

        public void CloseOpenWindows()
        {
            lock (lockObject)
            {
                if (openedWindows.Any())
                {
                    var windowsToClose = openedWindows.ToList();
                    windowsToClose.ForEach(x => x.Close());
                    openedWindows.Clear();
                }
            }
        }

        public MessageBoxResult ShowMessageBox(string message, string title = null, MessageBoxButton buttons = MessageBoxButton.OK, MessageBoxImage messageBoxImage = MessageBoxImage.None)
        {
            return MessageBox.Show(message, title, buttons, messageBoxImage);
        }

        public string ShowOpenFileDialog(string initialDirectory = null)
        {
            var dialog = new OpenFileDialog
            {
                InitialDirectory = initialDirectory,
                Multiselect = false,
                CheckFileExists = true,
                Filter = "Torrent files|*.torrent;"
            };

            return dialog.ShowDialog() == true ? dialog.FileName : null;
        }

        public void ShowFileInExplorer(string path)
        {
            Process.Start("explorer.exe", "/select," + path);
        }

        public void OpenFile(string file)
        {
            Process.Start(file);
        }

        private Window GetWindow(ViewModelBase viewModel, bool isDialogWindow)
        {
            lock (lockObject)
            {
                var window = openedWindows.SingleOrDefault(x => x.DataContext == viewModel);

                if (window != null)
                {
                    window.WindowState = WindowState.Normal;
                    window.Activate();
                    return window;
                }

                if (isDialogWindow)
                {
                    // Set window owner in order to prevent that it becomes hidden when minimizing the application
                    window = new DialogWindow
                    {
                        Owner = openedWindows.Any() ? openedWindows.Last() : Application.Current.MainWindow
                    };
                }
                else
                {
                    // Doesn't need a window owner since it's shown in the taskbar
                    window = new StandardWindow();
                }

                openedWindows.Add(window);

                window.Closed += (sender, e) =>
                {
                    Closed?.Invoke(sender, e);

                    openedWindows.Remove(window);
                    window = null;

                    if (!openedWindows.Any() && Application.Current.MainWindow != null)
                    {
                        Application.Current.MainWindow.Activate();
                    }
                };

                window.Closing += (sender, e) =>
                {
                    Closing?.Invoke(sender, e);
                };
                return window;
            }
        }
    }
}
