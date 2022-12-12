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
            .WithUrl("https://localhost:7260/chat")
            .WithAutomaticReconnect()
            .Build();

    }
}