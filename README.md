# Simple socket demo in C/C++ & C#
Simple C# program for TCP socket client-server demo

Simple TCP socket programming demo written by *C#* & *C/C++* languages.
C/C++ programs written to be compiled on both *Windows* & *Linux*.  
Demo description: *money exchange from VND to USD*.  
Note: it's a one-time-only server for minimizing code.

Minh họa cách lập trình TCP socket bằng cả hai ngôn ngôn *C#* và *C/C++*.  
Riêng chương trình C/C++ có thể biên dịch trên cả *Windows* và *Linux*.  
Nội dung: client kết nối đến server nhờ quy đổi khoản tiền VND sang USD.  
Lưu ý: để tối giản chương trình server chỉ phục vụ yêu cầu 1 lần duy nhất rồi thoát.  
<br>

#### Guide to compile and run C# programs on Windows:
Set PATH to your ***csc.exe*** inside .NET platform folder, for example *C:\WINDOWS\Microsoft.NET\Framework\v4.0.30319*  
Open CMD window, change to *simple-socket* folder and run following command lines:  
```
csc /target:exe CSTcpServer.cs
csc /target:exe CSTcpClient.cs
```
Then run the CSTcpServer and open another CMD window to run CSTcpClient.  
Note: click **Allow access** when *Windows Defender Firewall* asks.  
<br>

#### Guide to compile and run C/C++ programs on Windows:
Make sure you have: ***gcc/g++*** and ***ws2_32.lib*** installed on Windows (I use **[TDM64-GCC](https://jmeubank.github.io/tdm-gcc/))**) with the PATH pointed to it.  
Open CMD window, change to *simple-socket* folder and run following command lines:  
```
g++ -o CTcpClient CTcpClient.cpp -lws2_32
g++ -o CTcpServer CTcpServer.cpp -lws2_32
```
Then run the CTcpServer and open another CMD window to run CTcpClient.  
Note: click **Allow access** when *Windows Defender Firewall* asks.  
<br>

#### Guide to compile and run C/C++ programs on Linux:
Change to *simple-socket* directory and run following command lines:  
```
g++ -o CTcpClient CTcpClient.cpp  
g++ -o CTcpServer CTcpServer.cpp
```
Run CTcpServer and CTcpClient in seperate shells.  
<br>
Having fun programming!! 💙
