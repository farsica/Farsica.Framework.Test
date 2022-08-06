using Farsica.Framework.Test.Common;
using OpenQA.Selenium;
using static Farsica.Framework.Test.Common.Constants;

namespace Farsica.Framework.Test.Core;

public interface INamedBrowserFactory : IFactory<WebDriver>
{
    BrowserType BrowserType { get; }
}