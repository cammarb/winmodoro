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

            // For fullscreen
            _appWindow = GetAppWindowForCurrentWindow();

            // For starting the window in a specified size
            IntPtr hWnd = WinRT.Interop.WindowNative.GetWindowHandle(this);
            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hWnd);
            var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
            appWindow.Resize(new Windows.Graphics.SizeInt32 { Width = 500, Height = 500 });
        }

        private void StartBreakTimer()
        {
            if (_appWindow.Presenter.Kind == AppWindowPresenterKind.FullScreen)
            {
                _appWindow.SetPresenter(AppWindowPresenterKind.Default);
            }
            else
            {
                _appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);
            }
        }
        private AppWindow GetAppWindowForCurrentWindow()
        {
            // For fullscreen
            IntPtr hWnd = WindowNative.GetWindowHandle(this);
            WindowId myWndId = Win32Interop.GetWindowIdFromWindow(hWnd);
            return AppWindow.GetFromWindowId(myWndId);
        }
        private void DispatcherTimer_Tick(object sender, object e)
        {
            displayTime.Text = $"{focusTime.Value} minutes";
            focusTime.Value--;
            if (displayTime.Text.Equals("0 minutes"))
            {
                dispatcherTimer.Stop();
                focusTime.Value = 30;
                StopVisibility();
                StartBreakTimer();
            }
        }
        private void StartTimer()
        {
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            dispatcherTimer.Start();
        }

        private void StartVisibility()
        {
            // Show Stop button and hide Start button when timer starts
            startBtn.Visibility = Visibility.Collapsed;
            stopBtn.Visibility = Visibility.Visible;
            // Hide NumberBox and show display time
            focusTime.Visibility = Visibility.Collapsed;
            displayTime.Visibility = Visibility.Visible;
        }
        private void StopVisibility()
        {
            startBtn.Visibility = Visibility.Visible;
            stopBtn.Visibility = Visibility.Collapsed;
            focusTime.Visibility = Visibility.Visible;
            displayTime.Visibility = Visibility.Collapsed;
        }
        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            StartTimer();
            StartVisibility();
        }
        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            dispatcherTimer.Stop();
            StopVisibility();
            focusTime.Value = 30;
            displayTime.Text = $"{focusTime.Value} minutes";
        }

    }
}
