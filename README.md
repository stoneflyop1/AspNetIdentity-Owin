# ���Owinʹ��ASPNet Identity
## ��Ŀ����
* Identity.Core                ���� ��⣬����ʵ������Լ����ݷ���
* Identity.Auth                ���� ��⣬��֤��Ϣ
* Identity.Services            ���� ��⣬ҵ���߼��������û�ע�ᡢ��¼�Լ�ע����
* Identity.Web                 ���� �յ�WebApp������Cookie����վ��Token��֤��WebAPI
* Identity.ConsoleClient       ���� ����̨���򣬲�����վ��WebAPI�Ŀͻ���
## ���õ�Nuget��
* Install-Package Microsoft.AspNet.Identity.EntityFramework -projectName Identity.Core
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Services (���Զ����룺Microsoft.AspNet.Identity.Core��Microsoft.Owin��Microsoft.Owin.Security��Microsoft.Owin.Security.Cookies��Microsoft.Owin.Security.OAuth��Newtonsoft.Json)
* Install-Package Microsoft.AspNet.Mvc -projectName Identity.Web  (���Զ����룺Microsoft.AspNet.Razor��Microsoft.AspNet.WebPages��Microsoft.Web.Infrastructure)
* Install-Package Microsoft.AspNet.Identity.Owin -projectName Identity.Web 
* Install-Package Microsoft.Owin.Host.SystemWeb -projectName Identity.Web
ע����������ͼʱѡ����ʹ�ò���(_Layout)������Զ�����bootstrap��jQuery��Modernizr��Nuget����
## AspNet Identity�Ĵ
ʹ����EF��Identityʵ�֣���������ʹ�������ͣ��������ַ�������Ҫ�漰�����У�
* User      <= IdentityUser<TKey, TLogin, TRole, TClaim>
* Role      <= IdentityRole<TKey, TUserRole>
* UserClaim <= IdentityUserClaim<TKey>
* UserLogin <= IdentityUserLogin<TKey>
* UserRole  <= IdentityUserRole<TKey>


