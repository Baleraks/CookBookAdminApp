namespace CookBookAdminApp;

public partial class LoginPage : ContentPage
{
    private LoginViewModel _viewModel;

	public LoginPage()
	{
		InitializeComponent();

        BindingContext = _viewModel = new LoginViewModel(this);
    }
}