using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public class ColoredProgressBar : ProgressBar
{
    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    private const int PBM_SETSTATE = 0x0410;
    private const int PBST_NORMAL = 1;   // Green
    private const int PBST_ERROR = 2;    // Red

    public void SetState(int state)
    {
        SendMessage(Handle, PBM_SETSTATE, (IntPtr)state, IntPtr.Zero);
    }

    public void SetGreen() => SetState(PBST_NORMAL);
    public void SetRed() => SetState(PBST_ERROR);
}
