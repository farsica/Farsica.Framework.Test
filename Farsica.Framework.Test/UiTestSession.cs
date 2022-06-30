using Farsica.Framework.Test.Common;
using Farsica.Framework.Test.Core.DI;
using Microsoft.Extensions.DependencyInjection;

namespace Farsica.Framework.Test.Core;

public class UiTestSession
{
    private IServiceProvider services = null!;

    public SessionSettings Settings => Resolve<SessionSettings>();

    public static UiTestSession Current => InstanceFactory.Value;

    private static readonly Lazy<UiTestSession> InstanceFactory = new(() => new UiTestSession());

    private UiTestSession()
    {
    }

    public void Start()
    {
        services = ServiceRegistry.Register();
        if (!string.IsNullOrWhiteSpace(Settings.DownloadDirectory) && !Directory.Exists(Settings.DownloadDirectory))
        {
            Directory.CreateDirectory(Settings.DownloadDirectory);
        }
    }

    public void CleanUp()
    {
        if (Directory.Exists(Settings.DownloadDirectory))
        {
            Directory.Delete(Settings.DownloadDirectory, true);
        }
    }

    public T Resolve<T>() where T : notnull
    {
        if (services is null)
        {
            throw new InvalidOperationException("The session is not started");
        }

        return services.GetRequiredService<T>();
    }
}