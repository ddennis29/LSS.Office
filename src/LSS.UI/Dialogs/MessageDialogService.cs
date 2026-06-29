using System.Windows.Forms;

namespace LSS.UI.Dialogs;

public interface IMessageDialogService
{
    void ShowInformation(string title, string message);
    void ShowError(string title, string message);
}

public sealed class MessageDialogService : IMessageDialogService
{
    public void ShowInformation(string title, string message) => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
    public void ShowError(string title, string message) => MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
}
