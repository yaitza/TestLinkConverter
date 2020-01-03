# TestLinkConverter  # 

[![Gitter](https://badges.gitter.im/yaitza/TestLinkConverter.svg)](https://gitter.im/yaitza/TestLinkConverter?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)[![Travis](https://travis-ci.org/yaitza/TestLinkConverter.svg?branch=master)](https://travis-ci.org/yaitza/TestLinkConverter)[![GitHub release](https://img.shields.io/github/release/yaitza/TestLinkConverter.svg)](https://github.com/yaitza/TestLinkConverter/releases)[![GitHub tag](https://img.shields.io/github/tag/yaitza/TestLinkConverter.svg)](https://github.com/yaitza/TestLinkConverter/tags)[![Docs](https://img.shields.io/badge/Docs-Chinese-blue.svg)](https://www.yaitza.cn/2017/05/21/CSharp-TestLink/)

TestLink XML Convert to Excel or Excel Convert to XML.  

![pic4](/Resource/Image/pic4.png)

**注意：** 支持XML转换为Excel后，对应Excel转换为XML；XML和Excel相互转换。

## XML Convert to Excel ##

支持解析的XML(默认TestLink导出的XMlL)内部结构如下：

![pic1](/Resource/Image/pic11.png)

## Excel Convert To XML ##

支持解析Excel的格式如下：
![pic3](/Resource/Image/pic31.png)

**注意:**   

1. 支持Excel多Sheet页转换，每个Sheet页默认为一个测试套。
2. 本次针对XML解析至解析了常规使用字段，未解析全部导出XML字段。

## 用户手册 ## 

1. **测试用例步骤以及测试结果带序号可配置**

   文件目录下：.\TestLinkConverterSetup\TestLinkConverter.exe.config

   ![user-guide-001](/Resource/Image/user-guide-001.png)

   **true:**  为打开测试用例以及测试结果添加步骤序号。

   ![user-guide-002](/Resource/Image/user-guide-002.png)

   **false:**  为关闭测试用例以及测试结果添加步骤序号，默认为false。

   ![user-guide-003](/Resource/Image/user-guide-003.png)



## 引用三方类库 ##  

### 1.log4net ###
​	**下载地址：**  <https://logging.apache.org/log4net/download_log4net.cgi>

​	**参考博客：**  <http://www.cnblogs.com/kissazi2/p/3389551.html>

### 2.EPPlus ###
​	**下载地址：**  <https://github.com/JanKallman/EPPlus>  

​	**参考博客：**  <http://blog.csdn.net/ejinxian/article/details/52315950>

​	**资源地址：**  <https://archive.codeplex.com/?p=epplus>

### 3..Net FrameWork 4.5.2  ###
​	**下载地址：**	<https://www.microsoft.com/net/download/thank-you/net452-developer-pack>  

## 参考引用 ## 

#### 1. TestLink字段解析 ####  
​	http://wangsx.cn/?p=648

#### 2. Excel读写示例 ####  
​	http://blog.csdn.net/zzukun/article/details/50830439

#### 3. EXE打包教程  ####  
​	http://www.cnblogs.com/yinsq/p/5254893.html

​	https://blog.csdn.net/L120305q/article/details/98210048

​	https://blog.csdn.net/DonetRen/article/details/88766135

#### 4. C#中跨线程访问控件问题解决方案  ####  
​	http://blog.csdn.net/henreash/article/details/7789566

#### 5. Excel解析采用开源库EPPlus  #### 
​	https://github.com/JanKallman/EPPlus

#### 日志记录  ####  



