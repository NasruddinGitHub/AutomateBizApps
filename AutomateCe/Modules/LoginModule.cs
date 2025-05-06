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

        public async Task EnterEmail(string email)
        {
            var usernameElement = Locator(LoginModuleLocators.Username);
            await FillAsync(usernameElement, email);
        }

        public async Task ClickNext()
        {
            var usernameElement = Locator(LoginModuleLocators.Next);
            await ClickAsync(usernameElement);
        }

        public async Task EnterPassword(string password)
        {
            var passwordElement = Locator(LoginModuleLocators.Password);
            await FillAsync(passwordElement, password);
        }

        public async Task ClickSignIn()
        {
            var signInElement = Locator(LoginModuleLocators.SignIn);
            await ClickAsync(signInElement);
        }

        public async Task AcceptStaySignedIn()
        {
            var staySignedInYesElement = Locator(LoginModuleLocators.StaySignedInYes);
            await ClickAsync(staySignedInYesElement);
            await WaitUntilAppReadyStateIsComplete();
            // await WaitForNoActiveRequests();
        }

        public async Task RejectStaySignedIn()
        {
            var staySignedInNoElement = Locator(LoginModuleLocators.StaySignedInNo);
            await ClickAsync(staySignedInNoElement);
        }

        public async Task EnterCode(string mfaKey)
        {
            var code = GetTotp(mfaKey);
            var codeElement = Locator(LoginModuleLocators.Code);
            await FillAsync(codeElement, code);
        }

        public async Task ClickCodeVerify()
        {
            var verifyCodeElement = Locator(LoginModuleLocators.VerifyCode);
            await ClickAsync(verifyCodeElement);
        }

        public async Task Login(string? email, string? password)
        {
            await EnterEmail(email);
            await ClickNext();
            await EnterPassword(password);
            await ClickSignIn();
            await AcceptStaySignedIn();
        }

        public async Task Login(string? email, string? password, string? mfaKey)
        {
            await EnterEmail(email);
            await ClickNext();
            await EnterPassword(password);
            await ClickSignIn();
            await EnterCode(mfaKey);
            await ClickCodeVerify();
            await AcceptStaySignedIn();
        }

    }
}
