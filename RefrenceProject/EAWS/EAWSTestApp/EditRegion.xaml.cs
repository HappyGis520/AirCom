using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EAWSTestApp
{
    /// <summary>
    /// Interaction logic for EditRegion.xaml
    /// </summary>
    public partial class EditRegion : Window
    {
        public EditRegion()
        {
            InitializeComponent();
        }

        private void IDC_SaveRegionButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRegion = true;
            this.Close();
        }

        private void IDC_CancelRegionButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateRegion = false;
            this.Close();
        }

        public bool UpdateRegion
        {
            get
            {
                return m_UpdateRegion;
            }
            set
            {
                m_UpdateRegion = value;
            }
        }

        private bool m_UpdateRegion;


    }
}
