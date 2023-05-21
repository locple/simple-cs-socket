// C# TCP Server
// Mô tả: Chương trình client-server yêu cầu quy đổi USD -> VND
// Lưu ý: Vì mục đích minh họa mô hình TCP client-server thật đơn giản nên không xử lý những trường hợp lỗi.
// by LPLoc @ Nam Can Tho Univ. 2022

using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class CSTcpServer {
	// Khai báo port mặc định của server
	static string port = "9000";
	// Khai báo dữ liệu của server
	static double rate = 24640;		// Tỉ giá VND/USD

	static void Serve(Object obj) {
		// Khai báo bộ nhớ đệm để nhận data (32 bytes)
		byte[] buffer = new byte[32];
		Socket shipper = (Socket)obj;

		// Nhận data từ client gửi đến
		int len = shipper.Receive(buffer);
		string data = Encoding.ASCII.GetString(buffer, 0, len);
		Console.WriteLine("Data received: {0}", data);

		// Xử lý: đổi con số ghi trong data từ mệnh giá USD sang VND
		data = Math.Round(Convert.ToDouble(data) * rate).ToString();

		// Gửi data đã xử lý về lại client
		shipper.Send(Encoding.ASCII.GetBytes(data));
		Console.WriteLine("Data sent back: {0}", data);

		// Đóng socket nối với client
		shipper.Shutdown(SocketShutdown.Both);
	}

	public static void Main(string[] args) {
		// Lấy tham số port từ dòng lệnh
		if (args.Length > 0) port = args[0];

		// Tạo server socket
		///Socket listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
		///listener.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.IPv6Only, false);
		Socket listener = new Socket(SocketType.Stream, ProtocolType.Tcp);
		// Gán server tại mọi IPv4 & IPv6 : port
		IPEndPoint address = new IPEndPoint(IPAddress.IPv6Any, int.Parse(port));
		listener.Bind(address);
		// Mở server để chờ kết nối đến
		listener.Listen(0);			// Max số client tối đa cho phép cùng lúc
		Console.WriteLine("C# Server listening at port {0}...", port);

		// Xử lý cho từng client kết nối
		while (!Console.KeyAvailable) {
			// Tạo socket khi có client kết nối đến
			Socket shipper = listener.Accept();
			Console.WriteLine("Client connected.");

			Thread t = new Thread(new ParameterizedThreadStart(Serve));
			t.Start(shipper);
		};

		// Đóng socket lắng nghe
		listener.Close();
	}
}
