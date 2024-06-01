using CommunityToolkit.Maui.Core.Platform;

namespace CookBookAdminApp;

public partial class RegistrationPage : ContentPage
{
    private RegistrationViewModel viewModel;

    public RegistrationPage()
    {
        InitializeComponent();

        BindingContext = viewModel = new RegistrationViewModel(this);
    }
}