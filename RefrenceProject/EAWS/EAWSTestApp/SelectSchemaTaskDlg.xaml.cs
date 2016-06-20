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
    /// Interaction logic for SelectSchemaTaskDlg.xaml
    /// </summary>
    public partial class SelectSchemaTaskDlg : Window
    {
        public SelectSchemaTaskDlg(string[] tasknames)
        {
            InitializeComponent();

            foreach (string taskname in tasknames)
            {
                this.IDC_TASK_NAMES.Items.Add(taskname);
            }

        }

        private void IDC_SELECT_NAME_BUT_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
