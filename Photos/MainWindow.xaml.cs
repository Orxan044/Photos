using DevExpress.Utils.CommonDialogs.Internal;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Photos;


public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }


    private void MenuItem_Click(object sender, RoutedEventArgs e)
    {
        DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory());
        var files = di.GetFiles().ToList();
        di.GetFiles();
    }
}
