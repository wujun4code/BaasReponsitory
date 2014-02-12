using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using BaaSReponsitory;
using SampleDemo;

namespace BRDemo.WindowsPhone
{
    public partial class Home : PhoneApplicationPage
    {
        SimpleService dbRepon = new SimpleService();
        public Home()
        {
            InitializeComponent();

            RefreshData();
        }

        private const string ADD_popup_message_pattern = "new todo item added!Id is{0}";
        private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        {
            if (!my_popup_xaml.IsOpen)
            {
                my_popup_xaml.IsOpen = true;
            }
            else
            {
                my_popup_xaml.IsOpen = true;
            }


        }
        private void refresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }
        protected void RefreshData()
        {
            dbRepon.GetAll<string, Todo>(SetBinding);

        }

        protected void SetBinding(IQueryable<Todo> dataSource)
        {
            var s = dataSource.OrderByDescending(item => item.StartTime);
            lb_demo.DataContext = s;
        }

        private Todo CreateNow(string title, string content)
        {
            Todo rtn = new Todo()
            {
                Content = content,
                StartTime = DateTime.Now.AddHours(1),
                Title = title,
                Status = 0,
                From = "BR demo WP Client"
            };

            return rtn;
        }

        private void btn_continue_Click(object sender, RoutedEventArgs e)
        {
            var newItem = CreateNow(txb_title.Text.Trim(), txb_content.Text.Trim());
            dbRepon.Add<string, Todo>(newItem, new Action<Todo>
                (
                (item) =>
                {
                    RefreshData();
                    string mesaage = string.Format(ADD_popup_message_pattern, newItem.ItemId);
                    MessageBox.Show(mesaage);
                }
                ));

            if (my_popup_xaml.IsOpen)
            {
                my_popup_xaml.IsOpen = false;
            }
        }

        private void btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            if (my_popup_xaml.IsOpen)
            {
                my_popup_xaml.IsOpen = false;
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            CustomUser Diana = new CustomUser() 
            {
                UserName = "Diana",
                Password = "abcd1234!",
            };

            dbRepon.LoginAsync<CustomUser>(Diana, new Action<CustomUser>(
               (item) =>
               {
                   string mesaage = "Logined successful!, and the sessionToken is " + item.sessionToken;
                   MessageBox.Show(mesaage);
               }
               ));
        }

        private void Register_Click(object sender, EventArgs e)
        {
            CustomUser Diana = new CustomUser()
            {
                UserName = "Diana",
                Password = "abcd1234!",
                Email = "Diana@FBI.com",
                birthday = DateTime.Now.AddYears(-22),
                mobilePhone = "138888888"
            };

            dbRepon.RegisterAsync<CustomUser>(Diana, new Action<CustomUser>(
                (item) =>
                {
                    string mesaage = "Registerd successful!, and the sessionToken is " + item.sessionToken;
                    MessageBox.Show(mesaage);
                }
                ));
        }
        private void test_Click(object sender, EventArgs e)
        {

 
        }
    }
}