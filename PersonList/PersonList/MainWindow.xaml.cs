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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Xml.Serialization;


namespace PersonList
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        //Global var
        ObservableCollection<Person> persons = new ObservableCollection<Person>();


        public MainWindow()
        {

            InitializeComponent();
            PersonsList.ItemsSource = persons;
        }



        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //create new Person
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            string strfilename;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == true)
            {
                strfilename = openFileDialog.InitialDirectory + openFileDialog.FileName;
                ImageWay.Text = strfilename;
                var uri = new Uri(strfilename);
                var bitmap = new BitmapImage(uri);
                PersonImage.Source = bitmap;
            }
        }

        private void clearBox()
        {
            name.Clear();
            Surname.Clear();
            Age.Clear();
            ImageWay.Clear();
            Number.Clear();
            PersonImage.Source = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (name.Text == "" || Surname.Text == "" || Age.Text == "" || ImageWay.Text == "" || Number.Text == "")
            {
                MessageBox.Show("Enter data please", "Error", MessageBoxButton.OK);
            }
            else
            {
                Person person = new Person(name.Text, Surname.Text, Age.Text, ImageWay.Text, Number.Text, persons);
                persons.Add(person);
                MessageBox.Show("Person added", "SUccess", MessageBoxButton.OK);
                clearBox();
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            if (persons.Count == 0)
            {
                MessageBox.Show("List is empty", "Error", MessageBoxButton.OK);
            }
            else
            {
                File.Delete("persons.xml");
                XmlSerializer formatter_persons = new XmlSerializer(typeof(ObservableCollection<Person>));
                using (FileStream fs = new FileStream("persons.xml", FileMode.OpenOrCreate))
                {
                    formatter_persons.Serialize(fs, persons);
                }
                MessageBox.Show("List saved", "Success", MessageBoxButton.OK);
            }

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            string strfilename = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == true)
            {
                strfilename = openFileDialog.InitialDirectory + openFileDialog.FileName;
            }

            XmlSerializer formatter_persons = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (FileStream fs = File.OpenRead(strfilename))
            {
                persons = (ObservableCollection<Person>)formatter_persons.Deserialize(fs);
                MessageBox.Show("List opened", "Success", MessageBoxButton.OK);
            }
            PersonsList.ItemsSource = persons;
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            if (persons.Count != 0)
            {
                persons.Clear();
                PersonsList.ItemsSource = persons;
            }
            else MessageBox.Show("List is empty", "Error", MessageBoxButton.OK);
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            if (persons.Count != 0 && PersonsList.SelectedIndex != -1)
            {
                int index = PersonsList.SelectedIndex;
                persons.RemoveAt(index);
                PersonsList.ItemsSource = persons;
            }
            else
            {
                if (persons.Count == 0) MessageBox.Show("List is empty", "Error", MessageBoxButton.OK);
                else MessageBox.Show("Enter person", "Error", MessageBoxButton.OK);
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            int index = PersonsList.SelectedIndex;
            string strway = persons[index].image_way;
            var uri = new Uri(strway);
            var bitmap = new BitmapImage(uri);
            PersonImage.Source = bitmap;
        }



        bool view_check=true;
        private void ViewMenu_Click(object sender, RoutedEventArgs e)
        {
            if (view_check)
            {
                if (persons.Count != 0 && PersonsList.SelectedIndex != -1)
                {
                    int index = PersonsList.SelectedIndex;

                    //view
                    PersonsList.Visibility = System.Windows.Visibility.Hidden;
                    ViewCurrentPerson.Visibility = System.Windows.Visibility.Visible;
                    EditMenu.Visibility = System.Windows.Visibility.Visible;
                    ViewMenu.Header = "View all list";
                    view_check = false;
                    addPersonMenu.Visibility = System.Windows.Visibility.Hidden;
                    
                    //image
                    string image_way = persons[index].image_way;
                    var uri = new Uri(image_way);
                    var bitmap = new BitmapImage(uri);
                    ImageCurrentPerson.Source = bitmap;

                    //person data
                    currentImage.Text = persons[index].image_way;
                    currentName.Text = persons[index].name;
                    currentSurname.Text = persons[index].surname;
                    currentAge.Text = persons[index].age;
                    currentPhone.Text = persons[index].number;

                }
                else
                {
                    if (persons.Count == 0) MessageBox.Show("List is empty", "Error", MessageBoxButton.OK);
                    else MessageBox.Show("Enter person", "Error", MessageBoxButton.OK);
                }
            }
            else
            {
                ViewCurrentPerson.Visibility = System.Windows.Visibility.Hidden;
                EditMenu.Visibility = System.Windows.Visibility.Hidden;
                PersonsList.Visibility = System.Windows.Visibility.Visible;
                view_check = true;
                ViewMenu.Header = "View current Person";
                addPersonMenu.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            
            ViewMenu_Click(sender, e);

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //image save
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog(this) == true)
            {
                string strfilename = openFileDialog.InitialDirectory + openFileDialog.FileName;
                currentImage.Text = strfilename;
                var uri = new Uri(strfilename);
                var bitmap = new BitmapImage(uri);
                ImageCurrentPerson.Source = bitmap;
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            int index = PersonsList.SelectedIndex;
            persons[index].name = currentName.Text;
            persons[index].surname = currentSurname.Text;
            persons[index].age = currentAge.Text;
            persons[index].number = currentPhone.Text;
            persons[index].image_way = currentImage.Text;
            MenuItem_Click_1(sender, e);

            XmlSerializer formatter_persons = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (FileStream fs = File.OpenRead("persons.xml"))
            {
                persons = (ObservableCollection<Person>)formatter_persons.Deserialize(fs);
            }
            PersonsList.ItemsSource = persons;
        }
    }
}
