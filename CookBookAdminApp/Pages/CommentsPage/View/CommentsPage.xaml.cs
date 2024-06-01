namespace CookBookAdminApp;

[QueryProperty(nameof(ItemId), "ItemId")]
public partial class CommentsPage : ContentPage
{
    private CommentsViewModel _viewModel;

    public CommentsPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_id != null)
        {
            _ = Task.Run(_viewModel.InitAsync);
        }
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
                BindingContext = _viewModel = new CommentsViewModel(ItemId, this);
            }
        }
    }
}