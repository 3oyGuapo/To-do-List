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
using System.ComponentModel;

namespace To_do_List_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Readonly declaration to avoid change of the _controller object
        private readonly TodoListController _controller;//Outside Constructor so is a member of class
        public MainWindow()
        {
            InitializeComponent();
            //Define and initialization _controller object
            _controller = new TodoListController();

            this.DataContext = _controller;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Store the content of textbox to a string variable
            string description = inputTextBox.Text;

            //Check the description is not null or whitespace
            if (!string.IsNullOrWhiteSpace(description))
            {
                _controller.AddList(new ListContent(description));//Perform add by using object's method
                inputTextBox.Text = "";//Reset the textbox to empty
            }
            
        }

        //Button event that delete a selected list from the todo list
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //Check if the selected item is a ListContent type
            if (todoListBox.SelectedItem is ListContent selectedList)
            {
                _controller.DeleteList(selectedList);//Using object to call delete method
            }
            else
            {
                MessageBox.Show("Delete fail, selete a valid list to delete,");
            }
        }

        //When window closing event
        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            _controller.SaveListsToFile();//Perform a save to the file when close to avoid unsaved changes
        }
    }
}