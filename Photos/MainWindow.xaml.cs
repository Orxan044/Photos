using Photos.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
namespace Photos;


public partial class MainWindow : Window
{

    public ObservableCollection<Models.Image> Images { get; set; } = new();
    public int DateImagePath { get; set; }

    private string? selectPath;
    public MainWindow()
    {
        InitializeComponent();
    }

    private void AddFolderMenuItem_Click(object sender, RoutedEventArgs e)
    {
        if (sender is MenuItem menuItem)
            if (menuItem.Name == "Open") Photos.Items.Clear(); 
        
        using (FolderBrowserDialog fbd = new())
        {
            DialogResult result = fbd.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                selectPath = fbd.SelectedPath;
                DirectoryInfo folder = new DirectoryInfo(selectPath);
                FileInfo[] images = folder.GetFiles("*.*");
                foreach (FileInfo img in images)
                {
                    if (img.Extension.Equals(".png", StringComparison.OrdinalIgnoreCase) || img.Extension.Equals(".jpg", StringComparison.OrdinalIgnoreCase))
                    {
                        Models.Image image = new(img.FullName);
                        Images.Add(image);
                        Photos.Items.Add(new BitmapImage(new Uri(image.ImagePath)));
                    }
                }
            }
        }
    }

    private void SaveMenyuItem_Click(object sender, RoutedEventArgs e)
    {
        SaveFile(selectPath);
    }

    private void SaveFile(string FilePath)
    {
        try
        {
            StreamWriter writer = new StreamWriter(FilePath);

            foreach (var image in Images) writer.WriteLine(image.ImagePath);

            System.Windows.MessageBox.Show("File saved successfully!");
        }
        catch (Exception ex)
        {
            System.Windows.MessageBox.Show("An error occurred while saving the file: " + ex.Message);
        }
    }

    private void SaveAsMenyuItem_Click(object sender, RoutedEventArgs e)
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "Text files (*.png)|*.jpg"; 

        if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        {
            string filePath = saveFileDialog.FileName;

            SaveFile(filePath);
        }
    }

    private void AddFileMenyuItem_Click(object sender, RoutedEventArgs e)
    {
        Microsoft.Win32.OpenFileDialog openFileDialog = new();
        openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg";
        openFileDialog.FilterIndex = 2;
        if (openFileDialog.ShowDialog() == true)
        {
            Models.Image image = new(openFileDialog.FileName);
            Images.Add(image);
            Photos.Items.Add(new BitmapImage(new Uri(image.ImagePath)));
        }
    }

    private void ViewMenuItem_Checked(object sender, RoutedEventArgs e)
    {
        if(sender is System.Windows.Controls.RadioButton radioButton)
        {
            if(radioButton.Name == "Tiles") Resources["imageSize"] = 80.0;
            else if (radioButton.Name == "Small") Resources["imageSize"] = 20.0;
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

    private void ExitMenyuItem_Click(object sender, EventArgs e) { Close();}
}





