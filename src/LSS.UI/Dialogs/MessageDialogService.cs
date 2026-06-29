using System;
using System.Windows.Forms;
using LSS.Core.Dialogs;

namespace LSS.UI.Dialogs;

public sealed class MessageDialogService : IMessageDialogService
{
    public void ShowInformation(string message, string title = "LSS Office Suite")
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    public void ShowWarning(string message, string title = "LSS Office Suite")
    {
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public void ShowError(Exception exception, string title = "LSS Office Suite")
    {
        MessageBox.Show(exception.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
