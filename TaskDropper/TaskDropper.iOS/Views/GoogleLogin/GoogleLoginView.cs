using Foundation;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Ios.Views;
using System;
using TaskDropper.Core.ViewModels;
using UIKit;
using TaskDropper.Core.Authentication;
using TaskDropper.Core.Services;


namespace TaskDropper.iOS.Views
{
    public partial class GoogleLoginView : MvxViewController<GoogleLoginViewModel>, IGoogleAuthenticationDelegate
    {
        public static GoogleAuthenticator Auth;

        public GoogleLoginView() : base(nameof(GoogleLoginView), null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            Auth = new GoogleAuthenticator(Configuration.ClientId, Configuration.Scope, Configuration.RedirectUrl, this);

            GoogleLoginButton.TouchUpInside += OnGoogleLoginButtonClicked;

            var set = this.CreateBindingSet<GoogleLoginView, GoogleLoginViewModel>();
            set.Apply();
        }

        private void OnGoogleLoginButtonClicked(object sender, EventArgs e)
        {
            var authentificator = Auth.GetAuthenticator();
            var viewController = authentificator.GetUI();
            PresentViewController(viewController, true, null);
        }


        public async void OnAuthenticationCompleted(GoogleOAuthToken token)
        {
            DismissViewController(true, null);

            var googleService = new GoogleService();
            var email = await googleService.GetEmailAsync(token.TokenType, token.AccessToken);

            GoogleLoginButton.SetTitle($"Connected with {email}", UIControlState.Normal);

        }

        public void OnAuthenticationCanceled()
        {
            DismissViewController(true, null);
            var alertComtroller = new UIAlertController
            {
                Title = "Authentication Canceled",
                Message = "You didn`t completed the authentication process"
            };
            //PresentViewController(alertComtroller, true, null);

            DismissViewController(true, null);
        }

        public void OnAuthenticationFailed(string message, Exception exception)
        {
            // SFSafariViewController doesn't dismiss itself
            DismissViewController(true, null);

            var alertController = new UIAlertController
            {
                Title = message,
                Message = exception?.ToString()
            };
            //PresentViewController(alertController, true, null);

            DismissViewController(true, null);
        }
    }
}