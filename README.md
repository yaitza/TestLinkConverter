# TestLinkTransfer  

TestLink XML Transfer Excel or Excel Transfer XML.

## XML Transfer to Excel

支持解析的XML内部结构如下：

![pic1](/Resource/Image/pic1.png)

1. 代码支持多层testsuite嵌套，均能解析出对应的testcase，图示例较为简单，只作为说明。


# 临时记录
## 1.
http://wangsx.cn/?p=648

## 2.
无法嵌入互操作类型“Microsoft.Office.Interop.Word.ApplicationClass”。请改用适用的接口。


错误 4317 无法嵌入互操作类型“Microsoft.Office.Interop.Word.ApplicationClass”。请改用适用的接口。

类型“Microsoft.Office.Interop.Word.ApplicationClass”未定义构造函数

解决办法：

在Visual Studio 中点击菜单项“视图->解决方案资源管理器”，在其中点开“引用”文件夹，在"Microsoft.Office.Interop.Word" 上点击鼠标右键，选择“属性”，将属性中的“嵌入互操作类型”的值改为“false”即可。

## 3.  
http://blog.csdn.net/zzukun/article/details/50830439
