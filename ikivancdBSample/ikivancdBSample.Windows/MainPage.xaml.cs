using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using System.ComponentModel;

using IoT.WinRT;
using System.ComponentModel;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.UI.Core;
using Windows.UI.Popups;


namespace ikivancdBSample
{
    sealed partial class MainPage : Page
    {
        public double Value { get; set; }
        private MobileServiceCollection<TodoItem, TodoItem> items;
        private IMobileServiceTable<TodoItem> audioTable = App.MobileService.GetTable<TodoItem>();
        bool animState = true;
        MediaCapture media;

        public MainPage()
        {
            this.InitializeComponent();
            ProgressBar1.Maximum = ToDB(UInt16.MaxValue);
            ProgressBar1.Minimum = ToDB(UInt16.MaxValue);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 3);
            timer.Tick += new EventHandler<object>(timer_Tick);
            timer.Start();        
        }

        async void timer_Tick(object sender, object e)
        {
            if (ProgressBar1.Value > 0)
            {
                TodoItem audioItem = new TodoItem { Text = ((int)ProgressBar1.Value).ToString() + " - Windows 8.1", Complete = false };
                await InsertTodoItem(audioItem);
                await RefreshTodoItems();
            }
        }
        
        protected override async void OnNavigatedTo(Windows.UI.Xaml.Navigation.NavigationEventArgs e)
        {
            media = new MediaCapture();
            var captureInitSettings = new MediaCaptureInitializationSettings();
            captureInitSettings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            await media.InitializeAsync(captureInitSettings);
            media.Failed += (_, ex) => new MessageDialog(ex.Message).ShowAsync();
            media.RecordLimitationExceeded += (_) => new MessageDialog("record limit exceeded").ShowAsync();

            var stream = new AudioAmplitudeStream();
            media.StartRecordToStreamAsync(MediaEncodingProfile.CreateWav(AudioEncodingQuality.Low), stream);
            stream.AmplitudeReading += stream_AmplitudeReading;

            base.OnNavigatedTo(e);

            await RefreshTodoItems();
        }

        private async void ButtonRefresh_Click(object sender, RoutedEventArgs e)
        {
            await RefreshTodoItems();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (animState)
            { AnimStart.Begin(); animState = !animState; }
            else { AnimBack.Begin(); animState = !animState; }            
        }

        private async Task InsertTodoItem(TodoItem todoItem)
        {
            await audioTable.InsertAsync(todoItem);
            items.Add(todoItem);
        }

        private async Task RefreshTodoItems()
        {
            MobileServiceInvalidOperationException exception = null;
            try
            {
                items = await audioTable.OrderByDescending(w => w.CreatedAt).ToCollectionAsync();
            }
            catch (MobileServiceInvalidOperationException e)
            {
                exception = e;
            }

            if (exception != null)
            {
                await new MessageDialog(exception.Message, "Error loading items").ShowAsync();
            }
            else
            {
                ListItems.ItemsSource = items;
            }
        }

        void stream_AmplitudeReading(object sender, double reading)
        {
            this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var dbs = ToDB(reading);
                ProgressBar1.Value = dbs;
                ProgressBar1.Minimum = Math.Min(ProgressBar1.Minimum, dbs);
                TextBlock1.Text = string.Format("{0:0} dB", dbs);

            }).AsTask().Wait();
        }

        static double ToDB(double value)
        {
            return 20 * Math.Log10(Math.Sqrt(value * 2));
        }

    }
}
