using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Photos.Models;

public class Image : INotifyPropertyChanged
{
	private string? imagePath;

	public string  ImagePath
	{
		get { return imagePath; }
		set { imagePath = value; OnPropertyRaised(); }
	}

    public Image(string imagePath)
    {
        ImagePath = imagePath;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyRaised([CallerMemberName] string? name = null)
    {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(name));
    }

    public override string ToString() => ImagePath;
}
