using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LSS.Core.Settings;
using Newtonsoft.Json;

namespace LSS.Infrastructure.Settings;

/// <summary>
/// JSON settings store backed by the user's LocalAppData folder.
/// </summary>
public sealed class JsonSettingsService : ISettingsService
{
    private readonly string _settingsPath;

    public JsonSettingsService(string settingsPath)
    {
        _settingsPath = settingsPath;
        Directory.CreateDirectory(Path.GetDirectoryName(_settingsPath)!);
        Current = new AppSettings();
    }

    public AppSettings Current { get; private set; }

    public async Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default)
    {
        if (!File.Exists(_settingsPath))
        {
            await SaveAsync(Current, cancellationToken).ConfigureAwait(false);
            return Current;
        }

        using (var reader = new StreamReader(_settingsPath))
        {
            var json = await reader.ReadToEndAsync().ConfigureAwait(false);
            Current = JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
        }

        return Current;
    }

    public async Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default)
    {
        Current = settings;
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        using (var writer = new StreamWriter(_settingsPath, false))
        {
            await writer.WriteAsync(json).ConfigureAwait(false);
        }
    }
}
