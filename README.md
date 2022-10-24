##  1. <a name=':snowflake:'></a>注意 :snowflake:
1. ~~一定要阅读wiki文档~~ [参考文档](http://doc.netx.net.cn) 🤣
2. ~~一定要阅读wiki文档~~ [预览地址](http://www.netx.net.cn) :fire:
3. ~~一定要阅读wiki文档~~ :fire: 

##  2. <a name=':book:'></a>前言 :book:

<!-- https://www.webfx.com/tools/emoji-cheat-sheet/ -->

NetX 前身是 ``` sagittarius ``` ,一个我业余时间开发的模块化组件框架，在小范围使用后，效果还不错，能提高很大一部分开发效率。随着，Net6的问世，遂将其升级为Net6版本。

> 本框架仅支持webapi开发，暂不考虑带view试图模式开发
> 前台将开发配套模块化框架，请参考：[netx app](https://github.com/zeke202207/netx-app)

##  3. <a name=':rose:'></a>软件架构 :rose:

###  3.1. <a name='AssemblyLoadContext'></a>AssemblyLoadContext

![assemblyloadcontext](./doc/images/netx-arch.png#pic_center)

> netcore程序启动的时候，会创建一个默认```ALC```，在加载每一个用户模块的时候，会创建建立一个新的```ALC```，也就是这种机制，使得我们的程序可以在不同的模块加载不同版本的依赖库

###  3.2. <a name='ApplicationPart'></a>Application Part

![applicationpart](./doc/images/netx-apppart-arch.png#pic_center)

> 正是基于 ```AssemblyLoadContext``` ```Application Part``` 才能成就我们的```NetX```

####  3.2.1. <a name=''></a>**版本调整**


|序号|修正内容                                          | 修正日期  |
|--- | ---                                             | ---      |
|1   |支持配置独立```Context```和```SharedContext```    |2022/08/19|


##  4. <a name=':fire:'></a>开发计划 :fire:

     [√] web主机构建完成
     [√] 模块化功能集成 
     [√] 消息总线
     [√] 多租户支持
     [√] swagger文档组件扩展
     [doing] 可扩展的日志组件
     [doing] 基于内存的缓存模块
     [-] 基础设施组件完善
     

##  5. <a name='MRIssues:pray:'></a>欢迎提交MR和Issues :pray:


##  6. <a name=':pray:'></a>感谢 :pray:

1. 本框架设计参考了一些优秀的设计思路，非常感谢这些开源作者的付出（排名不分先后）

     * 框架
          - NetModular
          - CoolCat
          - Furion
     * 博文
          - [Creating a multi-tenant .NET Core Application](https://michael-mckenna.com/multi-tenant-asp-dot-net-core-application-tenant-resolution)

2. 本框架使用了一些开源项目

     <!-- 图标生成工具 :https://shields.io/category/version -->
     
     | 依赖项  |  版本|
     |  ----  | ---- |     
     | swagger | <img src="https://img.shields.io/badge/swagger-6.4.0-blue"/>  |
     | freesql cloud | <img src="https://img.shields.io/badge/freesql cloud-1.5.2-blue"/>  |

             
