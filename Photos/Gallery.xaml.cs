using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using Photos.Models;
    
namespace Photos;


public partial class Gallery : Window
{
    public ObservableCollection<Image> Images { get; set; }

    public int DateImagePath { get; set; }

    private int situationImage {  get; set; }

    private bool isCheck = true;

    private MainWindow mainWindow;

    public Gallery(ObservableCollection<Image> images , int dateImagePath , MainWindow mainWindow)
    {
        InitializeComponent();
        this.mainWindow = mainWindow;
        Images = images;
        DateImagePath = dateImagePath;
        Photo.Items.Add(new BitmapImage(new Uri(Images[DateImagePath].ToString())));
        situationImage = DateImagePath;
    }

    private void BackBtn_Click(object sender, RoutedEventArgs e)
    {
        Close();
        //MainWindow mainWindow = new MainWindow();   
        //mainWindow.ShowDialog();
        mainWindow.Show();
    }

    private void LeftBtn_Click(object sender, RoutedEventArgs e)
    {
        if (situationImage != 0)
        {
            RightBtn.IsEnabled = true;
            situationImage--;
            Photo.Items.Clear();
            Photo.Items.Add(new BitmapImage(new Uri(Images[situationImage].ToString())));
        }
        else
        {
            MessageBox.Show("Finished Picture");
            LeftBtn.IsEnabled = false;
        }
    }

    private void PauseBtn_Click(object sender, RoutedEventArgs e) { isCheck = false; }

    private async void AutoPlayBtn_Click(object sender, RoutedEventArgs e)
    {
        isCheck = true;
        while (isCheck)
        {
            situationImage++;
            if (situationImage < Images.Count)
            {
                await Task.Delay(1200);
                Photo.Items.Clear();
                Photo.Items.Add(new BitmapImage(new Uri(Images[situationImage].ToString())));
            }
            else  situationImage = -1;
        }
        

        
    }

    private void RightBtn_Click(object sender, RoutedEventArgs e)
    {
        if (situationImage != Images.Count - 1)
        {
            RightBtn.IsEnabled = true;
            situationImage++;
            Photo.Items.Clear();
            Photo.Items.Add(new BitmapImage(new Uri(Images[situationImage].ToString())));
        }
        else
        {
            MessageBox.Show("Finished Picture");
            RightBtn.IsEnabled = false;
        }
    }
}



