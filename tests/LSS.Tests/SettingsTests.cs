using System.IO;
using System.Threading.Tasks;
using LSS.Core.Settings;
using LSS.Infrastructure.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LSS.Tests;

[TestClass]
public sealed class SettingsTests
{
    [TestMethod]
    public async Task Settings_RoundTrip_Works()
    {
        var path = Path.Combine(Path.GetTempPath(), "lss-settings-test.json");
        if (File.Exists(path)) File.Delete(path);
        var service = new JsonSettingsService(path);
        await service.SaveAsync(new AppSettings { EnvironmentName = "Test" });
        var loaded = await service.LoadAsync();
        Assert.AreEqual("Test", loaded.EnvironmentName);
    }
}
