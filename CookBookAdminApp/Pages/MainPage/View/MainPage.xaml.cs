
namespace CookBookAdminApp
{
    public partial class MainPage : ContentPage
    {
        MainViewModel viewModel;

        public MainPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new MainViewModel(this);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            _ = Task.Run(viewModel.InitAsync);
        }

        protected override void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
        }

        private void RecipeList_RemainingItemsThresholdReached(object sender, EventArgs e)
        {
            viewModel.RemainingItemseachedCommand.Execute(null);
        }
    }

}
