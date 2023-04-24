// C/C++ TCP Server for Windows & Linux
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
#include <netinet/in.h>				// for sockaddr_in
#include <sys/socket.h>				// for Linux/Unix socket functions
#endif


// Khai bao port cua server
int port = 8000;
// Khai bao bo nho dem de nhan data (32 bytes)
char buffer[32] = {0};
// Khai bao du lieu cua server
double rate = 24640;				// Ti gia VND/USD


int main() {
	// Tao server socket
#if defined WIN32
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) exit(0);
#endif
	int listener = socket(AF_INET, SOCK_STREAM, IPPROTO_TCP);

	// Gan server tai port
	struct sockaddr_in address;
	address.sin_family = AF_INET;
	address.sin_addr.s_addr = INADDR_ANY;
	address.sin_port = htons(port);
	bind(listener, (struct sockaddr*) &address, sizeof(address));

	// Mo server de cho ket noi den
	listen(listener, 3);			// So client toi da cho phep cung luc = 3
	printf("C++ Server listening...\n");

	// Tao socket khi co client ket noi den
	int addrlen = sizeof(address);
	int shipper = accept(listener, (struct sockaddr*) &address, (socklen_t*) &addrlen);
	printf("Client connected.\n");


	// Nhan data tu client gui den
#if defined WIN32
	int len = recv(shipper, buffer, 32, 0);
#else	// Linux
	int len = read(shipper, buffer, 32);
#endif
	printf("Data received: %s\n", buffer);


	// Xy ly: doi con so ghi trong data tu menh gia USD sang VND
	char data[32];
	sprintf(data, "%llu", llround(std::stod(buffer) * rate));


	// Gui data da xu ly ve lai client
	send(shipper, data, strlen(data), 0);
	printf("Data sent back: %s\n", data);


	// Dong cac socket
#if defined WIN32
	closesocket(shipper);
	shutdown(listener, SD_BOTH);
	WSACleanup();
#else	// Linux
	close(shipper);
	shutdown(listener, SHUT_RDWR);
#endif
	return 0;
}
