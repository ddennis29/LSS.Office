using LSS.Core.Settings;
using Newtonsoft.Json;

namespace LSS.Infrastructure.Settings;

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

        var json = await File.ReadAllTextAsync(_settingsPath, cancellationToken).ConfigureAwait(false);
        Current = JsonConvert.DeserializeObject<AppSettings>(json) ?? new AppSettings();
        return Current;
    }

    public async Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default)
    {
        Current = settings;
        var json = JsonConvert.SerializeObject(settings, Formatting.Indented);
        await File.WriteAllTextAsync(_settingsPath, json, cancellationToken).ConfigureAwait(false);
    }
}
