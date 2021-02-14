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
using CavemanTcp;

namespace CavemanServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CavemanTcpClient client;
        CavemanTcpServer server;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        

        // Client
        private void Client_Connect(object sender, RoutedEventArgs e)
        {
            client = new CavemanTcpClient(ClientConnectIP.Text + ":11500");
            client.Events.ClientConnected += Events_ClientConnected;
            client.Events.ClientDisconnected += Events_ClientDisconnected;
            client.Connect(5000);
        }
        private void Client_Disconnect(object sender, RoutedEventArgs e)
        {
            ReadResult res = null;

            //res = client.ReadWithTimeout(1, 1); // This line causes the issue

            if(res != null)
                Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += $"Read resulted in ({res.Status})\n");



            if (client != null)
            {
                client.Disconnect();
                Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += "Client disconnected by button.\n");
            }
            else
                Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += "Client is already null.\n");
        }
        private void Events_ClientConnected(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += "Client connected event.\n");
        }
        private void Events_ClientDisconnected(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += "Client disconnected event.\n");
            
        }



        // Server
        private void Server_Start(object sender, RoutedEventArgs e)
        {
            server = new CavemanTcpServer(ServerIP.Text + ":11500");
            server.Events.ClientConnected += Events_ServerClientConnected;
            server.Events.ClientDisconnected += Events_ServerClientDisconnected;
            server.Start();
            Application.Current.Dispatcher.Invoke(() => ClientTextBlock.Text += "Server started.\n");
        }
        private void Server_Disconnect(object sender, RoutedEventArgs e)
        {
            if(server.GetClients().Count() > 0)
            {
                server.DisconnectClient(server.GetClients().First());
                Application.Current.Dispatcher.Invoke(() => ServerTextBlock.Text += "Disconnected client by button.\n");
            }
            else
            {
                Application.Current.Dispatcher.Invoke(() => ServerTextBlock.Text += "No client to disconnect.\n");
            }
            
        }
        private void Events_ServerClientConnected(object sender, ClientConnectedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => ServerTextBlock.Text += "Client connected to server event.\n");
        }
        private void Events_ServerClientDisconnected(object sender, ClientDisconnectedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() => ServerTextBlock.Text += "Client has disconnected event.\n");
        }
    }
}
