using System.Threading;
using System.Threading.Tasks;

namespace LSS.Core.Settings;

/// <summary>
/// Loads and saves user application settings.
/// </summary>
public interface ISettingsService
{
    AppSettings Current { get; }
    Task<AppSettings> LoadAsync(CancellationToken cancellationToken = default);
    Task SaveAsync(AppSettings settings, CancellationToken cancellationToken = default);
}
