using Photos.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace Photos;


public partial class MainWindow : Window
{

    public ObservableCollection<Image> Images { get; set; } = new();

    public int DateImagePath { get; set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {

        using (FolderBrowserDialog fbd = new())
        {
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                DirectoryInfo folder = new DirectoryInfo(fbd.SelectedPath);
                FileInfo[] images = folder.GetFiles("*.*");

                foreach (FileInfo img in images)
                {
                    if (img.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) || img.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        Image image = new(img.FullName);
                        Images.Add(image);
                        Photos.Items.Add(new BitmapImage(new Uri(image.ImagePath)));
                    }
                }
            }
        }
    }

    private void Photos_MouseDoubleClick(object sender, MouseButtonEventArgs e)
    {
        DateImagePath = Photos.SelectedIndex;
        Hide();
        Gallery gallery = new(Images,DateImagePath,this);
        gallery.ShowDialog();
        Show();
    }
}





