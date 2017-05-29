# rongim.server.netcore
融云ServerSDK .Net Core版
[如鹏网](http://www.rupeng.com)杨中科从融云的.net版Server SDK移植为.Net Core版本。
# NuGet安装指令：
 >Install-Package rongim.server.netcore
# 例子代码：
 ```C#
 RongCloud cloud = RongCloud.getInstance("your apikey", "your AppSecret");  
var result = await cloud.user.getTokenAsync('userId', "username", "http://uc.discuz.net/images/noavatar_middle.gif");
```
在服务器退出的时候最好执行：
```C#
RongCloud.ReleaseAll();
```