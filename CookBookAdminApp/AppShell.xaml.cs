namespace CookBookAdminApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute("MainRout",typeof(MainPage));
            Routing.RegisterRoute(nameof(RecipePage),typeof(RecipePage));
            Routing.RegisterRoute(nameof(CommentsPage),typeof(CommentsPage));
        }
    }
}
