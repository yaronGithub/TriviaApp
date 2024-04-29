using TriviaAppClean.ViewModels;

namespace TriviaAppClean.Views;

public partial class TriviaGameView : ContentPage
{
	public TriviaGameView(TriviaGameViewModel vm)
	{
		InitializeComponent();
		this.BindingContext = vm;
        RandomizeChildren();
    }
    private void RandomizeChildren()
    {
        var children = stackLayout.Children.ToList();
        stackLayout.Children.Clear();

        var rnd = new Random();
        while (children.Count > 0)
        {
            var index = rnd.Next(0, children.Count);
            var child = children[index];
            children.RemoveAt(index);
            stackLayout.Children.Add(child);
        }
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        if (next.IsEnabled)
        {
            var children = stackLayout.Children.ToList();
            stackLayout.Children.Clear();

            var rnd = new Random();
            while (children.Count > 0)
            {
                var index = rnd.Next(0, children.Count);
                var child = children[index];
                children.RemoveAt(index);
                stackLayout.Children.Add(child);
            }
        }
    }
}