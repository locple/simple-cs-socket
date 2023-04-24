// C# TCP Server
// Mô tả: Chương trình client-server yêu cầu quy đổi USD -> VND
// Lưu ý: Vì mục đích minh họa mô hình TCP client-server thật đơn giản nên không xử lý những trường hợp lỗi.
// by LPLoc @ Nam Can Tho Univ. 2022

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class CSTcpServer {
	// Khai báo port của server
	static int port = 8000;
	// Khai báo bộ nhớ đệm để nhận data (32 bytes)
	static byte[] buffer = new byte[32];
	// Khai báo dữ liệu của server
	static double rate = 24640;		// Tỉ giá VND/USD

	public static void Main() {
		// Tạo server socket
		Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		// Gán server tại port
		IPEndPoint address = new IPEndPoint(IPAddress.Any, port);
		listener.Bind(address);
		// Mở server để chờ kết nối đến
		listener.Listen(3);			// Số client tối đa cho phép cùng lúc = 3
		Console.WriteLine("C# Server listening...");
		// Tạo socket khi có client kết nối đến
		Socket shipper = listener.Accept();
		Console.WriteLine("Client connected.");

		// Nhận data từ client gửi đến
		int len = shipper.Receive(buffer);
		string data = Encoding.ASCII.GetString(buffer, 0, len);
		Console.WriteLine("Data received: {0}", data);

		// Xử lý: đổi con số ghi trong data từ mệnh giá USD sang VND
		data = Math.Round(Convert.ToDouble(data) * rate).ToString();

		// Gửi data đã xử lý về lại client
		shipper.Send(Encoding.ASCII.GetBytes(data));
		Console.WriteLine("Data sent back: {0}", data);

		// Đóng các socket
		shipper.Shutdown(SocketShutdown.Both);
		listener.Close();
	}
}
