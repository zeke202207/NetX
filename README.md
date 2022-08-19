<!-- vscode-markdown-toc -->
* 1. [前言 :book:](#:book:)
* 2. [软件架构 :rose:](#:rose:)
	* 2.1. [AssemblyLoadContext](#AssemblyLoadContext)
	* 2.2. [Application Part](#ApplicationPart)
		* 2.2.1. [**版本调整**](#)
	* 2.3. [Multi Tenant](#MultiTenant)
	* 2.4. [框架对多租户的支持](#-1)
	* 2.5. [数据库对多租户的支持](#-1)
* 3. [开发计划 :fire:](#:fire:)
* 4. [欢迎提交MR和Issues :pray:](#MRIssues:pray:)
* 5. [感谢 :pray:](#:pray:)

<!-- vscode-markdown-toc-config
	numbering=true
	autoSave=true
	/vscode-markdown-toc-config -->
<!-- /vscode-markdown-toc -->


##  1. <a name=':book:'></a>前言 :book:

<!-- https://www.webfx.com/tools/emoji-cheat-sheet/ -->

NetX 前身是 ``` sagittarius ``` ,一个我业余时间开发的模块化组件框架，在小范围使用后，效果还不错，能提高很大一部分开发效率。随着，Net6的问世，遂将其升级为Net6版本。

> 本框架仅支持webapi开发，暂不考虑带view试图模式开发
> 前台将开发配套模块化框架，敬请期待

##  2. <a name=':rose:'></a>软件架构 :rose:

###  2.1. <a name='AssemblyLoadContext'></a>AssemblyLoadContext

![assemblyloadcontext](./doc/images/netx-arch.png#pic_center)

> netcore程序启动的时候，会创建一个默认```ALC```，在加载每一个用户模块的时候，会创建建立一个新的```ALC```，也就是这种机制，使得我们的程序可以在不同的模块加载不同版本的依赖库

###  2.2. <a name='ApplicationPart'></a>Application Part

![applicationpart](./doc/images/netx-apppart-arch.png#pic_center)

> 正是基于 ```AssemblyLoadContext``` ```Application Part``` 才能成就我们的```NetX```

####  2.2.1. <a name=''></a>**版本调整**


|序号|修正内容                                          | 修正日期  |
|--- | ---                                             | ---      |
|1   |支持配置独立```Context```和```SharedContext```    |2022/08/19|


###  2.3. <a name='MultiTenant'></a>Multi Tenant

###  2.4. <a name='-1'></a>框架对多租户的支持

多租户实现逻辑图

![mutil tenant](./doc/images/mutil-tenant.png#pic_center)

> 借助 Tenant中间件，在每次http请求中，根据Tenant选择策略和存储方式（均可自定义）获取Tenant信息，并将其封装到HttpContext请求上下文中。

###  2.5. <a name='-1'></a>数据库对多租户的支持



##  3. <a name=':fire:'></a>开发计划 :fire:

     [√] web主机构建完成
     [√] 模块化功能集成 
     [√] 消息总线
     [-] 基础设施组件完善
     [开发中] 数据库访问层构建
     [开发中] 多租户支持

##  4. <a name='MRIssues:pray:'></a>欢迎提交MR和Issues :pray:


##  5. <a name=':pray:'></a>感谢 :pray:

本框架设计参考了一些优秀的设计思路，非常感谢这些开源作者的付出（排名不分先后）

* 框架
     - NetModular
     - CoolCat
     - Furion
* 博文
     - [Creating a multi-tenant .NET Core Application](https://michael-mckenna.com/multi-tenant-asp-dot-net-core-application-tenant-resolution)
