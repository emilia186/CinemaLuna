using CinemaLuna.Windows;
using System.Linq;
using System.Windows;

public static class WindowManager
{
    public static void OpenNewWindowAndCloseOthers(Window newWindow)
    {
        var currentWindows = Application.Current.Windows.Cast<Window>().ToList();

        newWindow.Show();

        foreach (var win in currentWindows)
        {
            
            if (win != newWindow)
            {
                if (!(newWindow is LoginWindow && win is LoginWindow))
                {
                    win.Close();
                }
            }
        }
    }
}
