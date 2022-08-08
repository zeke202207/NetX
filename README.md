<!-- https://www.webfx.com/tools/emoji-cheat-sheet/ -->
# 前言 :book:

NetX 前身是 ``` sagittarius ``` ,一个我业余时间开发的模块化组件框架，在小范围使用后，效果还不错，能提高很大一部分开发效率。随着，Net6的问世，遂将其升级为Net6版本。

> 本框架仅支持webapi开发，暂不考虑带view试图模式开发
> 前台将开发配套模块化框架，敬请期待

# 软件架构 :rose:

## AssemblyLoadContext

![assemblyloadcontext](./doc/images/netx-arch.png#pic_center)

> netcore程序启动的时候，会创建一个默认```ALC```，在加载每一个用户模块的时候，会创建建立一个新的```ALC```，也就是这种机制，使得我们的程序可以在不同的模块加载不同版本的依赖库

## Application Part

![applicationpart](./doc/images/netx-apppart-arch.png#pic_center)

> 正是基于 ```AssemblyLoadContext``` ```Application Part``` 才能成就我们的```NetX```

# 开发计划 :fire:

     [√] web主机构建完成
     [√] 模块化功能集成 
     [√] 消息总线
     [-] 基础设施组件完善
     [开发中] 数据库访问层构建
     [-] 多租户支持

# 欢迎提交MR和Issues :pray: