# CavemanServer
Showcase of the client.Disconnect issue in CavemanTcp

How to use:
For the server, input the computer's IP (without port) on which you are running the app into the TextBox next to Server and then press Start.
For the client, input the remote computer's IP (without port) to which you are connecting and press Connect.

Both client and server should print that a client has connected.
You can then press the Disconnect button on the client, which calls the `client.Disconnect()` method and it should return saying the client is disconnected now.
If you then uncomment the line with `// This line causes the issue` and try the experiment again, you will find it will not disconnect anymore and will freeze.
