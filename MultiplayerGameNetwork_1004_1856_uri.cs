// 代码生成时间: 2025-10-04 18:56:58
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Represents a multiplayer game network service.
/// </summary>
public class MultiplayerGameNetwork
{
    private readonly TcpListener _tcpListener;
    private readonly int _port;
    private readonly bool _isRunning;
    private readonly byte[] _buffer = new byte[1024];

    /// <summary>
    /// Initializes a new instance of the MultiplayerGameNetwork class.
    /// </summary>
    /// <param name="port">The port number to listen on.</param>
    public MultiplayerGameNetwork(int port)
    {
        _port = port;
        _tcpListener = new TcpListener(System.Net.IPAddress.Any, _port);
        _isRunning = false;
    }

    /// <summary>
    /// Starts the multiplayer game network service.
    /// </summary>
    public void Start()
    {
        _isRunning = true;
        _tcpListener.Start();
        ListenForClients();
    }

    /// <summary>
    /// Stops the multiplayer game network service.
    /// </summary>
    public void Stop()
    {
        _isRunning = false;
        _tcpListener.Stop();
    }

    /// <summary>
    /// Listens for incoming client connections and handles them.
    /// </summary>
    private async void ListenForClients()
    {
        while (_isRunning)
        {
            try
            {
                TcpClient client = await _tcpListener.AcceptTcpClientAsync();
                await HandleClientAsync(client);
            }
            catch (Exception ex)
            {
                // Handle exceptions, e.g., log the error and continue.
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Handles a client connection.
    /// </summary>
    /// <param name="client">The TcpClient to handle.</param>
    private async Task HandleClientAsync(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        try
        {
            while (_isRunning)
            {
                int bytesRead = await stream.ReadAsync(_buffer, 0, _buffer.Length);
                if (bytesRead > 0)
                {
                    // Process the received data.
                    string receivedData = Encoding.UTF8.GetString(_buffer, 0, bytesRead);
                    Console.WriteLine($"Received: {receivedData}");

                    // Echo the received data back to the client.
                    byte[] response = Encoding.UTF8.GetBytes(receivedData);
                    await stream.WriteAsync(response, 0, bytesRead);
                }
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions, e.g., log the error and continue.
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
        finally
        {
            client.Close();
        }
    }
}
