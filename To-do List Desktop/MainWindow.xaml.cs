using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using To_do_List;

namespace To_do_List_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<ListContent> TodoList { get; set; }
        public ListContent SelectedList { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            TodoList = new ObservableCollection<ListContent>();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string description = inputTextBox.Text;

            if (!string.IsNullOrWhiteSpace(description))
            {
                TodoList.Add(new ListContent(description));
                inputTextBox.Text = "";
            }
            
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedList != null)
            {
                TodoList.Remove(SelectedList);
            }
            else
            {
                MessageBox.Show("Delete fail, selete a valid list to delete,");
            }
        }
    }
}