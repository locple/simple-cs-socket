// C# TCP Client
// Mô tả: Chương trình client-server yêu cầu quy đổi USD -> VND
// Lưu ý: Vì mục đích minh họa mô hình TCP client-server thật đơn giản nên không xử lý những trường hợp lỗi.
// by LPLoc @ Nam Can Tho Univ. 2022

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class CSTcpClient {
	// Khai báo địa chỉ và port của server
	static IPAddress ip = IPAddress.Parse("127.0.0.1");	// Server IP
	static int port = 8000;								// Server Port
	// Khai báo bộ nhớ đệm để nhận data (32 bytea)
	static byte[] buffer = new byte[32];
	// Khai báo data
	static string data = "20.5";	// USD

	public static void Main() {
		// Tạo client socket
		Socket shipper = new Socket(SocketType.Stream, ProtocolType.Tcp);
		// Kết nối đến server tại ip:port
		IPEndPoint address = new IPEndPoint(ip, port);
		shipper.Connect(address);
		Console.WriteLine("Connected to server.");

		// Gửi data cho server
		shipper.Send(Encoding.ASCII.GetBytes(data));
		Console.WriteLine("USD: {0}", data);

		// Nhận data server gửi lại
		int len = shipper.Receive(buffer);
		data = Encoding.ASCII.GetString(buffer, 0, len);
		Console.WriteLine("VND: {0:n0}", Convert.ToDouble(data));

		// Đóng client socket
		shipper.Shutdown(SocketShutdown.Both);
	}
}
