using System;

namespace LSS.Core.Dialogs;

public interface IMessageDialogService
{
    void ShowInformation(string message, string title = "LSS Office Suite");
    void ShowWarning(string message, string title = "LSS Office Suite");
    void ShowError(Exception exception, string title = "LSS Office Suite");
}
