using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Windowing;
using WinRT.Interop;
using Microsoft.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winmodoro
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private AppWindow _appWindow;
        DispatcherTimer dispatcherTimer;
        public MainWindow()
        {
            this.InitializeComponent();

            // Hide default title bar.
            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);
            //this.Title = "Winmodoro";

            // For fullscreen
            //_appWindow = GetAppWindowForCurrentWindow();

            // For starting the window in a specified size
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 300, Height = 400 });
        }

        //private void myButton_Click(object sender, RoutedEventArgs e)
        //{
        //    if (_appWindow.Presenter.Kind == AppWindowPresenterKind.FullScreen)
        //    {
        //        _appWindow.SetPresenter(AppWindowPresenterKind.Default);
        //        myButton.Content = "Full Screen";
        //    }
        //    else
        //    {
        //        _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
        //        myButton.Content = "Exit Full Screen";
        //    }
        //}
        private AppWindow GetAppWindowForCurrentWindow()
        {
            // For fullscreen
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }

        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            //displayTime.Text = $"{focusTime.Value} minutes";
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            dispatcherTimer.Start();

        }

        void dispatcherTimer_Tick(object sender, object e)
        {
            
            focusTime.Value--;
            displayTime.Text = $"{focusTime.Value} minutes";
            if (focusTime.Value == 0)
            {
                dispatcherTimer.Stop();
            }
        }

    }
}
