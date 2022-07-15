using Farsica.Framework.Test.Common;
using OpenQA.Selenium;

namespace Farsica.Framework.Test.Core;

public interface INamedBrowserFactory : IFactory<WebDriver>
{
    BrowserType BrowserType { get; }
}