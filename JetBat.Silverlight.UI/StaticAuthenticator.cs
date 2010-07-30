namespace JetBat.Silverlight.UI
{
	public static class StaticAuthenticator
	{
		public static bool IsAuthenticating { get; private set; }

		public delegate void OnAuthentificationRequredDelegate();
		public delegate void OnAuthentificationFinishedDelegate(bool succeeded);

		public static event OnAuthentificationRequredDelegate OnAuthenticationRequred;
		public static event OnAuthentificationFinishedDelegate OnAuthenticationFinished;

		public static void RequireAuthentication()
		{
			if(OnAuthenticationRequred == null)
			{
				if(OnAuthenticationFinished != null)
					OnAuthenticationFinished(false);
				return;
			}
			if(!IsAuthenticating)
			{
				IsAuthenticating = true;
				OnAuthenticationRequred();
			}
		}

		public static void SetAuthenticationResult(bool succeeded)
		{
			IsAuthenticating = false;
			if(OnAuthenticationFinished != null)
				OnAuthenticationFinished(succeeded);
		}
	}
}