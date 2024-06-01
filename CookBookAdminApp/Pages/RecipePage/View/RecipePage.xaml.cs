namespace CookBookAdminApp;

[QueryProperty(nameof(ItemId), "ItemId")]
public partial class RecipePage : ContentPage
{
    RecipeViewModel viewModel;


    public RecipePage()
    {
        InitializeComponent();
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

    private string _id;
    public string ItemId
    {
        get => _id;
        set
        {
            _id = value;

            if (BindingContext == default)
            {
                BindingContext = viewModel = new RecipeViewModel(ItemId,this);
            }
        }
    }
}