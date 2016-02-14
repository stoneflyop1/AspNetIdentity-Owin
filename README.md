# 结合Owin使用ASPNet Identity
## 项目构成
* Identity.Core                —— 类库，包括实体对象以及数据访问
* Identity.Auth                —— 类库，认证信息
* Identity.Services            —— 类库，业务逻辑，包括用户注册、登录以及注销等
* Identity.Web                 —— 空的WebApp，带有Cookie的网站及Token认证的WebAPI
* Identity.ConsoleClient       —— 控制台程序，测试网站和WebAPI的客户端
## 引用的Nuget包
* Install-Package Microsoft.AspNet.Identity.EntityFramework -projectName Identity.Core
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Services (会自动引入：Microsoft.AspNet.Identity.Core，Microsoft.Owin，Microsoft.Owin.Security，Microsoft.Owin.Security.Cookies，Microsoft.Owin.Security.OAuth，Newtonsoft.Json)
* Install-Package Microsoft.AspNet.Mvc -projectName Identity.Web  (会自动引入：Microsoft.AspNet.Razor，Microsoft.AspNet.WebPages，Microsoft.Web.Infrastructure)
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Web 
* Install-Package Microsoft.Owin.Host.SystemWeb -projectName Identity.Web
注：若创建视图时选择了使用布局(_Layout)，则会自动引入bootstrap，jQuery，Modernizr的Nuget包。
## AspNet Identity的搭建
使用了EF的Identity实现，主键类型使用了整型，而不是字符串。主要涉及的类有：
* User      <= IdentityUser<TKey, TLogin, TRole, TClaim>
* Role      <= IdentityRole<TKey, TUserRole>
* UserClaim <= IdentityUserClaim<TKey>
* UserLogin <= IdentityUserLogin<TKey>
* UserRole  <= IdentityUserRole<TKey>


