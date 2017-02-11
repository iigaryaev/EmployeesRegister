using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EmployeesRegister.Startup))]
namespace EmployeesRegister
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
