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
	static string ip = "::1";						// Default Server IP
	static string port = "9000";						// Default Server Port
	// Khai báo bộ nhớ đệm để nhận data (32 bytea)
	static byte[] buffer = new byte[32];
	// Khai báo data
	static string data;									// USD

	public static void Main(string[] args) {
		// Tham số dòng lệnh có dạng IPv4 hay IPv6 như sau:
		// 127.0.0.1:8000
		// ::1:8000
		// [fe80::40ee:6de3:7926:d4bb]:8000
		// fe80::34e3:97ee:f79a:c001:8000
		if (args.Length > 0) {
			int idx = args[0].LastIndexOf(':');
			ip = args[0].Substring(0, idx);
			port = args[0].Substring(idx + 1);
		}

		// Tạo client socket
		Socket shipper = new Socket(SocketType.Stream, ProtocolType.Tcp);
		// Kết nối đến server tại ip:port
		IPEndPoint address = new IPEndPoint(IPAddress.Parse(ip), int.Parse(port));
		shipper.Connect(address);
		Console.WriteLine("Connected to server.");

		// Nhập số USD cần quy đổi
		Console.Write("Nhap so USD: ");
		data = Console.ReadLine();

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
