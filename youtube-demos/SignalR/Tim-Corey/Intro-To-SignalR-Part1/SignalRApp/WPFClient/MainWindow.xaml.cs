using System;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.AspNetCore.SignalR.Client;

namespace WPFClient;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    HubConnection connection;

    public MainWindow()
    {
        InitializeComponent();
        connection = new HubConnectionBuilder()
            .WithUrl("https://localhost:7181/chat")
            .WithAutomaticReconnect()
            .Build();

        connection.Reconnecting += (sender) =>
        {
            //wait on this...
            this.Dispatcher.Invoke(() =>
            {
                // add this to our listbox 'messages'
                var newMessage = "Attempting to reconnect...";
                messages.Items.Add(newMessage);
            });

            //...return a complete task
            return Task.CompletedTask;
        };

        connection.Reconnected += (sender) =>
        {
            //wait on this...
            this.Dispatcher.Invoke(() =>
            {
                // add this to our listbox 'messages'
                messages.Items.Clear();
                var newMessage = "Reconnected to the server";
                messages.Items.Add(newMessage);
            });

            //...return a complete task
            return Task.CompletedTask;
        };

        connection.Closed += (sender) =>
        {
            //wait on this...
            this.Dispatcher.Invoke(() =>
            {
                // add this to our listbox 'messages'
                var newMessage = "Connection Closed";
                messages.Items.Add(newMessage);
                openConnection.IsEnabled = true;
                sendMessage.IsEnabled = false;
            });

            //...return a complete task
            return Task.CompletedTask;
        };
    }

    private async void openConnection_Click(object sender, RoutedEventArgs e)
    {
        // any time we receive a message, trigger this...
        connection.On<string, string>("ReceiveMessage", (user, message) =>
        {
            this.Dispatcher.Invoke(() =>
            {
                var newMessage = $"{user}: {message}";
                messages.Items.Add(newMessage);
            });
        });

        try
        {
            await connection.StartAsync();
            messages.Items.Add("Connection Started");
            openConnection.IsEnabled = false;
            sendMessage.IsEnabled = true;

        }
        catch (Exception exception)
        {
            // doesn't need the dispatcher.invoke since it's on the UI thread
            messages.Items.Add(exception.Message);
        }
    }

    private async void sendMessage_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            await connection.InvokeAsync("SendMessage", "WPF Client", messageInput.Text);
        }
        catch (Exception exception)
        {
            messages.Items.Add(exception.Message);
        }
    }
}