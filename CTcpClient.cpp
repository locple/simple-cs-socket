// C/C++ TCP Client for Windows & Linux
// Mo ta: Chuong trinh client-server yeu cau quy doi USD -> VND
// Luu y: Vi muc dich minh hoa mo hinh TCP client-server that don gian nen khong xu ly nhung truong hop loi
// by LPLoc @ Nam Can Tho Univ. 2022

#define _WIN32_WINNT 0x0600			// for Windows platform, Vista/2008 or higher required
#include <string>					// for std::stod()
#include <math.h>					// for llround()
#include <stdio.h>

#if defined WIN32
#include <ws2tcpip.h>				// for Windows socket functions
#pragma comment(lib, "ws2_32.lib")	// for linking to Winsock2 library in VS2019 projects
#else	// Linux
#include <cstring>					// for strlen()
#include <unistd.h>					// for read()
#include <arpa/inet.h>				// for inet_pton()
#include <sys/socket.h>				// for Linux/Unix socket functions
#endif


// Khai bao dia chi va port cua server
const char* ip = "127.0.0.1";
int port = 8000;
// Khai bao bo nho dem de nhan data (32 bytes)
char buffer[32] = {0};
// Khai bao data
const char* data = "20.5";			// USD


int main() {
	// Tao client socket
#if defined WIN32
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) exit(0);
#endif
	int shipper = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	// Ket noi den server tai ip:port
	struct sockaddr_in address;
	address.sin_family = AF_INET;
	address.sin_port = htons(port);
	inet_pton(AF_INET, ip, &(address.sin_addr));
	int fd = connect(shipper, (struct sockaddr*) &address, sizeof(address));
	printf("Connected to server.\n");


	// Gui data cho server
	send(shipper, data, strlen(data), 0);
	printf("USD: %s\n", data);


	// Nhan data server gui lai
#if defined WIN32
	int len = recv(shipper, buffer, 32, 0);
#else	// Linux
	int len = read(shipper, buffer, 32);
#endif
	printf("VND: %s\n", buffer);


	// Dong cac socket
#if defined WIN32
	closesocket(fd);
	WSACleanup();
#else	// Linux
	close(fd);
#endif
	return 0;
}
