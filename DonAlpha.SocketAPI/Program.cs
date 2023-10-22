using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DonAlpha.SocketAPI
{
    /// <summary>
    /// The main class that contains the socket server application.
    /// </summary>
    public static class Program
    {
        private static readonly Dictionary<string, Func<string[], IPAddress?, Socket, Task<string>>>
            GameActions = new()
            {
                { "createGame", GameMethods.CreateGame },
                { "createPlayer", GameMethods.CreatePlayer },
                { "getPlayers", GameMethods.GetPlayers },
                { "startGame", GameMethods.StartGame },
                { "move", GameMethods.Move },
                { "broadcast", GameMethods.Broadcast }
            };

        private static readonly List<Socket> ConnectedClients = new();
        private static readonly string Ack = "";
        private static readonly string Eom = "";

        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments (not used).</param>
        public static async Task Main(string[] args)
        {
            await StartSocketServer("localhost");
        }

        /// <summary>
        /// Starts the socket server and listens for incoming connections.
        /// </summary>
        /// <param name="server">The server's IP address.</param>
        private static async Task StartSocketServer(string server)
        {
            if (!IPAddress.TryParse(server, out var ipAddress))
            {
                Console.WriteLine("Invalid server IP address.");
                return;
            }

            var ipEndPoint = new IPEndPoint(ipAddress, 11_000);
            Console.WriteLine("Server is listening for incoming connections...");

            using var listener = CreateSocket(ipEndPoint);
            while (true)
            {
                var handler = await listener.AcceptAsync();
                ConnectedClients.Add(handler);
                _ = HandleClientAsync(handler);
            }
        }

        /// <summary>
        /// Handles communication with a connected client.
        /// </summary>
        /// <param name="handler">The client socket.</param>
        private static async Task HandleClientAsync(Socket handler)
        {
            try
            {
                var clientEndPoint = (IPEndPoint)handler.RemoteEndPoint!;
                var clientIpAddress = clientEndPoint.Address;

                Console.WriteLine($"Client connected from {clientIpAddress}");

                while (true)
                {
                    var buffer = new byte[1_024];
                    var received = await handler.ReceiveAsync(buffer, SocketFlags.None);
                    if (received == 0)
                    {
                        // The client has disconnected.
                        ConnectedClients.Remove(handler);
                        break;
                    }

                    var response = Encoding.UTF8.GetString(buffer, 0, received);

                    if (response.Contains(Eom, StringComparison.Ordinal))
                    {
                        response = response.Replace(Eom, "");
                        var output = await DoGameActions(response, clientIpAddress, handler);

                        var echoBytes = Encoding.UTF8.GetBytes(output + Ack);
                        await handler.SendAsync(echoBytes);
                        Console.WriteLine($"Sent: \"{output}\"");
                    }
                }
            }
            catch (Exception e)
            {
                ConnectedClients.Remove(handler);
                Console.WriteLine($"Error occurred: {e}");
            }
        }

        /// <summary>
        /// Creates a socket and binds it to the specified IP endpoint.
        /// </summary>
        /// <param name="ipEndPoint">The IP endpoint to bind the socket to.</param>
        /// <returns>The created socket.</returns>
        private static Socket CreateSocket(IPEndPoint ipEndPoint)
        {
            var listener = new Socket(ipEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(ipEndPoint);
            listener.Listen(100);
            return listener;
        }

        /// <summary>
        /// Executes the appropriate game action based on the input.
        /// </summary>
        /// <param name="input">The input string from the client.</param>
        /// <param name="clientIpAddress">The client's IP address.</param>
        /// <param name="handler">The client socket.</param>
        /// <returns>The response to send back to the client.</returns>
        private static async Task<string> DoGameActions(string input, IPAddress? clientIpAddress, Socket handler)
        {
            var inputArgs = input.Split(',');

            if (GameActions.TryGetValue(inputArgs[0], out var action))
            {
                return await action(inputArgs, clientIpAddress, handler);
            }

            return "Error, invalid command";
        }

        /// <summary>
        /// Stops the socket server and closes all connected clients.
        /// </summary>
        public static void StopSocketServer()
        {
            foreach (var client in ConnectedClients)
            {
                client.Close();
            }

            ConnectedClients.Clear();
            // Optionally, close the listener here as well.
        }

        /// <summary>
        /// Sends a message to a specific client.
        /// </summary>
        /// <param name="message">The message to send.</param>
        /// <param name="client">The client socket to send the message to.</param>
        private static async Task SendToClient(string message, Socket client)
        {
            var echoBytes = Encoding.UTF8.GetBytes(message + Ack);
            await client.SendAsync(echoBytes);
        }

        /// <summary>
        /// Broadcasts a message to all connected clients.
        /// </summary>
        /// <param name="message">The message to broadcast.</param>
        /// <param name="socket">The socket to exclude from the broadcast.</param>
        internal static async Task BroadcastToAllExceptOne(string message, Socket socket)
        {
            foreach (var client in ConnectedClients.Where(x => x != socket))
            {
                await SendToClient(message, client);
            }
        }
    }
}