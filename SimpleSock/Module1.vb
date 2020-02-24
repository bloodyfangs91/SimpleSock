Imports System.Net
Imports System.Net.Sockets
Imports System.Text

Module Module1

    Sub Main()
        Dim socket As Socket
        Dim port As Int32 = 80
        Dim host As String = "google.com"
        Dim request As String = "GET \  "
        Dim bytesReceived() As Byte
        Dim bytesSent() As Byte = Encoding.ASCII.GetBytes(request)

        socket = createSocket(host, port)

        If socket IsNot Nothing Then
            Console.WriteLine("Socket connected!")
            sendRequest(socket, bytesSent, bytesReceived)

            '---- Receive Buffer Code ------
            'If bytesReceived.Length > 0 Then
            '    Console.Write("Bytes received: ")
            '    For Each x In bytesReceived
            '        Console.Write(Hex(x) & " ")
            '    Next
            'Else
            '    Console.Write("No bytes receieved!")
            'End If
        Else
            Console.WriteLine("Socket not connected.")
        End If


    End Sub

    Public Function createSocket(ByVal server As String, ByVal port As Int32) As Socket
        Dim hostEntry As IPHostEntry
        Try
            hostEntry = Dns.GetHostEntry(server)
        Catch e As SocketException
            Console.WriteLine("host not found")
            Return Nothing
        End Try

        For Each ipaddress In hostEntry.AddressList
            Dim ipe As IPEndPoint = New IPEndPoint(ipaddress, port)
            Dim sock As Socket = New Socket(ipe.AddressFamily, SocketType.Stream, ProtocolType.Tcp)
            sock.Connect(ipe)

            If sock.Connected Then
                Return sock
            Else
                Return Nothing
            End If
        Next
    End Function

    Public Sub sendRequest(ByRef sock As Socket, ByRef bytesSent() As Byte, ByRef bytesReceived() As Byte)

        sock.Send(bytesSent, bytesSent.Length, 0)
        Dim bytes As Int32 = 0

        ' ----- Receive Buffer Code -----
        'Dim utcnow = DateTime.UtcNow
        'While sock.Available = 0
        '    If ((DateTime.UtcNow - utcnow) > TimeSpan.FromSeconds(10)) Then
        '        Console.WriteLine("Socket did not receieve any data for 10 seconds.")
        '        bytesReceived = Nothing
        '        Exit While
        '    End If
        'End While

        'sock.Receive(bytesReceived, SocketFlags.None)
    End Sub

End Module
