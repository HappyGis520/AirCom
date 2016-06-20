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
    /// Interaction logic for EditFilters.xaml
    /// </summary>
    public partial class EditFilters : Window
    {
        public EditFilters()
        {
            InitializeComponent();
        }

        private void IDC_ADD_PLANFILTER_Click(object sender, RoutedEventArgs e)
        {
            string newfilter = this.IDC_PLANFILTER_EDITBOX.Text;

            if (newfilter.Length > 0)
            {
                this.IDC_SELECTED_PLANFILTERS.Items.Add(newfilter);
                this.IDC_PLANFILTER_EDITBOX.Text = "";
            }
        }

        private void IDC_REMOVE_PLANFILTER_Click(object sender, RoutedEventArgs e)
        {
            if (this.IDC_SELECTED_PLANFILTERS.Items.IsEmpty == false)
            {
                int removeindex = this.IDC_SELECTED_PLANFILTERS.SelectedIndex;
                this.IDC_SELECTED_PLANFILTERS.Items.RemoveAt(removeindex);
            }
        }

        public bool UpdateFilters
        {
            get
            {
                return m_UpdateFilters;
            }
            set
            {
                m_UpdateFilters = value;
            }
        }

        private bool m_UpdateFilters;

        private void IDC_UPDATE_FILTERS_BUT_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilters = true;
            this.Close();
        }

        private void IDC_CANCEL_FILTERS_BUT_Click(object sender, RoutedEventArgs e)
        {
            UpdateFilters = false;
            this.Close();
        }

        private void IDC_ADD_BESTSERVERFILTER_Click(object sender, RoutedEventArgs e)
        {
            string newfilter = this.IDC_BESTSERVER_FILTER_EDITBOX.Text;

            if (newfilter.Length > 0)
            {
                this.IDC_SELECTED_BESTSERVERFILTERS.Items.Add(newfilter);
                this.IDC_BESTSERVER_FILTER_EDITBOX.Text = "";
            }
        }

        private void IDC_REMOVE_BESTSERVERFILTER_Click(object sender, RoutedEventArgs e)
        {
            if (this.IDC_SELECTED_BESTSERVERFILTERS.Items.IsEmpty == false)
            {
                int removeindex = this.IDC_SELECTED_BESTSERVERFILTERS.SelectedIndex;
                this.IDC_SELECTED_BESTSERVERFILTERS.Items.RemoveAt(removeindex);
            }
        }
    }
}
