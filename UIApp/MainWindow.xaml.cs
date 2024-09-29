using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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
using UIApp.Entities;

namespace UIApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        HttpClient httpClient = new HttpClient();
        HttpResponseMessage response=new HttpResponseMessage();
        public MainWindow()
        {
            InitializeComponent();
            Contact = new Contact();
            this.DataContext= this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyCanged([CallerMemberName]string name = null)
        {
            PropertyChangedEventHandler? handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private ObservableCollection<Contact> allContacts;

        public ObservableCollection<Contact> AllContacts
        {
            get { return allContacts; }
            set { allContacts = value; OnPropertyCanged(); }
        }

        private Contact contact;

        public Contact Contact
        {
            get { return contact; }
            set { contact = value; OnPropertyCanged(); }
        }

        private void GetAll_Click(object sender, RoutedEventArgs e)
        {
            GetAllContacts();
        }

        private async void GetAllContacts()
        {
            response = await httpClient.GetAsync($"https://localhost:22950/c");
            var str=await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Contact>>(str);
            AllContacts = new ObservableCollection<Contact>(items);
        }

        private async void DeleteContact()
        {
            if(Contact!=null && Contact.Id != 0)
            {
                response = await httpClient.DeleteAsync($"https://localhost:22950/c/{Contact.Id}");
                if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                {
                    GetAllContacts();
                    MessageBox.Show("Contact deleted successfully");
                }
            }
            else
            {
                MessageBox.Show("Please select any contact");
            }
            Contact = new Contact();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            DeleteContact();
        }

        private async void addContact_Click(object sender, RoutedEventArgs e)
        {
            var myContent = JsonConvert.SerializeObject(Contact);
            var buffer=Encoding.UTF8.GetBytes(myContent);   
            var byteContent=new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

            response = await httpClient.PostAsync("https://localhost:22950/c", byteContent);

            var str = await response.Content.ReadAsStringAsync();
            var item=JsonConvert.DeserializeObject<Contact>(str);
            if (item.Id != 0)
            {
                GetAllContacts();
            }
        }

        private void updateContact_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}