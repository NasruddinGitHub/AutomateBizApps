using AutomateCe.Modules;
using Microsoft.Playwright;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AutomateCe.ObjectRepository.ObjectRepository;

namespace AutomateCe.Modules
{
    public class LoginModule : SharedPage
    {
        private IPage _page;

        public LoginModule(IPage page) : base(page)
        {
            this._page = page;
        }

        public async Task EnterEmailAsync(string email)
        {
            var usernameElement = Locator(LoginModuleLocators.Username);
            await FillAsync(usernameElement, email);
        }

        public async Task ClickNextAsync()
        {
            var usernameElement = Locator(LoginModuleLocators.Next);
            await ClickAsync(usernameElement);
            await WaitUntilAppIsIdleAsync();
        }

        public async Task EnterPasswordAsync(string password)
        {
            var passwordElement = Locator(LoginModuleLocators.Password);
            await FillAsync(passwordElement, password);
        }

        public async Task ClickSignInAsync()
        {
            var signInElement = Locator(LoginModuleLocators.SignIn);
            await ClickAsync(signInElement);
        }

        public async Task AcceptStaySignedInAsync()
        {
            var staySignedInYesElement = Locator(LoginModuleLocators.StaySignedInYes);
            await ClickAsync(staySignedInYesElement);
            await WaitUntilAppIsIdleAsync();
            // await WaitForNoActiveRequests();
        }

        public async Task RejectStaySignedInAsync()
        {
            var staySignedInNoElement = Locator(LoginModuleLocators.StaySignedInNo);
            await ClickAsync(staySignedInNoElement);
        }

        public async Task EnterCodeAsync(string mfaKey)
        {
            var code = GetTotp(mfaKey);
            var codeElement = Locator(LoginModuleLocators.Code);
            await FillAsync(codeElement, code);
        }

        public async Task ClickCodeVerifyAsync()
        {
            var verifyCodeElement = Locator(LoginModuleLocators.VerifyCode);
            await ClickAsync(verifyCodeElement);
        }

        public async Task LoginAsync(string? email, string? password)
        {
            await EnterEmailAsync(email);
            await ClickNextAsync();
            await EnterPasswordAsync(password);
            await ClickSignInAsync();
            await AcceptStaySignedInAsync();
        }

        public async Task LoginAsync(string? email, string? password, string? mfaKey)
        {
            await EnterEmailAsync(email);
            await ClickNextAsync();
            await EnterPasswordAsync(password);
            await ClickSignInAsync();
            await EnterCodeAsync(mfaKey);
            await ClickCodeVerifyAsync();
            await AcceptStaySignedInAsync();
        }

    }
}
