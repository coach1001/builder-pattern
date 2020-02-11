using Autofac;
using CoreDuiWebApi.Authentication;

namespace CoreDuiWebApi
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {            
            builder.RegisterAssemblyTypes();
        }
    }
}
