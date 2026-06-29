using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Dialogs;
using LSS.Core.Logging;
using LSS.Core.Settings;

namespace LSS.WordAddIn.Commands;

/// <summary>
/// Toggles developer mode for diagnostics and migration tools.
/// </summary>
[Command(CommandIds.ToggleDeveloperMode, "Toggle Developer Mode", "Settings", Description = "Turns developer diagnostics on or off.", DeveloperOnly = false)]
public sealed class ToggleDeveloperModeCommand : CommandBase
{
    private readonly ISettingsService _settingsService;
    private readonly IMessageDialogService _dialogService;

    public ToggleDeveloperModeCommand(ISettingsService settingsService, IMessageDialogService dialogService, IAppLogger logger)
        : base("Toggle Developer Mode", logger)
    {
        _settingsService = settingsService;
        _dialogService = dialogService;
    }

    protected override async Task ExecuteCoreAsync(CancellationToken cancellationToken)
    {
        var settings = _settingsService.Current;
        settings.DeveloperMode = !settings.DeveloperMode;
        await _settingsService.SaveAsync(settings, cancellationToken).ConfigureAwait(false);
        _dialogService.ShowInformation($"Developer Mode is now {(settings.DeveloperMode ? "ON" : "OFF")}.");
    }
}
