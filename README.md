# TestLinkConverter  

TestLink XML Convert to Excel or Excel Convert to XML.

## XML Convert to Excel

支持解析的XML内部结构如下：

![pic1](/Resource/Image/pic1.png)

1. 代码支持多层testsuite嵌套，均能解析出对应的testcase，图示例较为简单，只作为说明。
2. 本次针对XML解析至解析了常规使用字段，未解析全部导出XML字段。

## Excel Convert To XML

支持解析Excel的格式如下：
![pic3](/Resource/Image/pic3.png)

**注意:** 暂不解析关键字，规约编号字段。

---
## 引用三方类库
### 1.log4net
**下载地址：**  <https://logging.apache.org/log4net/download_log4net.cgi>

**参考博客：**  <http://www.cnblogs.com/kissazi2/p/3389551.html>

---
## 问题  
代码过程中以及环境设置中出现的相关问题。
### 1. 调用Excel相关类库时环境配置问题
**问题描述：**  
无法嵌入互操作类型“Microsoft.Office.Interop.Word.ApplicationClass”。请改用适用的接口。
错误 4317 无法嵌入互操作类型“Microsoft.Office.Interop.Word.ApplicationClass”。请改用适用的接口。
类型“Microsoft.Office.Interop.Word.ApplicationClass”未定义构造函数。  
**解决办法：**  
在Visual Studio 中点击菜单项“视图->解决方案资源管理器”，在其中点开“引用”文件夹，在"Microsoft.Office.Interop.Excel;" 上点击鼠标右键，选择“属性”，将属性中的“嵌入互操作类型”的值改为“false”即可。  
**说明:**  
引用Excel类库：Microsoft Excel 14.0 Object Library，需要安装Office 2010.    

![pic2](/Resource/Image/pic2.png)

## 临时记录
### 1. TestLink字段解析
http://wangsx.cn/?p=648

### 2. Excel读写示例 
http://blog.csdn.net/zzukun/article/details/50830439

### 3. EXE打包教程  
http://www.cnblogs.com/yinsq/p/5254893.html

### 4. C#中跨线程访问控件问题解决方案  
http://blog.csdn.net/henreash/article/details/7789566
