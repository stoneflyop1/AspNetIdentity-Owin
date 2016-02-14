# 结合Owin使用AspNet Identity

## 项目构成
* Identity.Core         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       —— 类库，包括实体对象以及数据访问
* Identity.Auth         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       —— 类库，用户认证的配置等
* Identity.Services     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       —— 类库，业务逻辑，包括用户注册、登录以及注销等
* Identity.Web          &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;       —— 空的WebApp，带有Cookie的网站及Token认证的WebAPI
* Identity.ConsoleClient   &nbsp;&nbsp;&nbsp;     —— 控制台程序，测试网站和WebAPI的客户端

## 引用的Nuget包
* Install-Package Microsoft.AspNet.Identity.EntityFramework -projectName Identity.Core
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Services (会自动引入：Microsoft.AspNet.Identity.Core，Microsoft.Owin，Microsoft.Owin.Security，Microsoft.Owin.Security.Cookies，Microsoft.Owin.Security.OAuth，Newtonsoft.Json)
* Install-Package Microsoft.AspNet.Mvc -projectName Identity.Web  (会自动引入：Microsoft.AspNet.Razor，Microsoft.AspNet.WebPages，Microsoft.Web.Infrastructure)
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Web 
* Install-Package Microsoft.Owin.Host.SystemWeb -projectName Identity.Web

注：若创建视图时选择了使用布局(_Layout)，则会自动引入bootstrap，jQuery，Modernizr的Nuget包。

## AspNet Identity
使用了EF的Identity实现，主键类型使用了`int`，而不是*string*。主要涉及的实体类有：
* [User](Identity.Core/User.cs)      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<= IdentityUser&lt;TKey, TLogin, TRole, TClaim&gt;
* [Role](Identity.Core/Role.cs)      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<= IdentityRole&lt;TKey, TUserRole&gt;
* [UserClaim](Identity.Core/UserClaim.cs)                          &nbsp;&nbsp;&nbsp;&nbsp;<= IdentityUserClaim&lt;TKey&gt;
* [UserLogin](Identity.Core/UserLogin.cs)                          &nbsp;&nbsp;&nbsp;&nbsp;<= IdentityUserLogin&lt;TKey&gt;
* [UserRole](Identity.Core/UserRole.cs)                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<= IdentityUserRole&lt;TKey&gt;

关键的一些其他类有：
* [CustomDbContext](Identity.Core/CustomDbContext.cs)  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<= IdentityDbContext&lt;TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim&gt;
* [CustomUserStore](Identity.Core/CustomUserStore.cs)  &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<= UserStore&lt;TUser, TRole, TKey, TUserLogin, TUserRole, TUserClaim&gt;
* [CustomUserManager](Identity.Core/CustomUserManager.cs) &nbsp;&nbsp;&nbsp;&nbsp;<= UserManager&lt;TUser, TKey&gt;
* [CustomSigninManager](Identity.Services/CustomSigninManager.cs) &nbsp;&nbsp;<= SignInManager&lt;TUser, TKey&gt;

各个类之间的关系： User和Role等实体类 => IdentityDbContext => UserStore => UserManager => SignInManager。
IdentityDbContext数据库中的表跟用户相关的实体类一一对应；
UserStore封装了对用户的操作，不再需要直接面对数据访问层；
UserManager则对用户属性做了一些限定(比如：用户名、邮箱和密码的规则)，通过UserStore操作用户及用户的信息；
SignInManager处理用户的登录，跟Owin的认证中间件有关，通过UserManager验证用户登录是否合法。
