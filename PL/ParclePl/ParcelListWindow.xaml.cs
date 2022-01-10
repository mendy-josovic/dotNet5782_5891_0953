using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BlApi;
using BO;

namespace PL
{
    /// <summary>
    /// Interaction logic for ParcelListWindow.xaml
    /// </summary>
    public partial class ParcelListWindow : Window
    {
        IBl BlObject;
        IEnumerable<IGrouping<String, ParcelToList>> parcelToLists { set; get; }
        public ParcelListWindow(IBl blObject)
        {
            InitializeComponent();
            this.BlObject = blObject;
            parcelToLists = from l in BlObject.DisplayParcelList()
                            group l by l.Sender;
            ParcelLiastView.ItemsSource = parcelToLists;
        }

        private void ParcelLiastView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

    }
}
