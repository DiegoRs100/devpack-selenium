using OpenQA.Selenium;

namespace Devpack.Selenium.Handlers.Interfaces
{
    public interface ISeleniumHandler : IDisposable
    {
        string GetAtualUrl();
        void Navigate(string url);
        void GoBackNavigate(int times = 1);

        bool ExistsByAnchor(string anchor);

        void ClickLinkByText(string text);
        void ClickByAnchor(string anchor);

        IWebElement GetByAnchor(string anchor);

        void SetTextBoxById(string id, string text);
        void SetDropDownById(string id, string optionValue);

        string GetTextByAnchor(string anchor);
        string GetInputValueById(string id);
        IEnumerable<IWebElement> GetElementsByAnchor(string anchor);
    }
}