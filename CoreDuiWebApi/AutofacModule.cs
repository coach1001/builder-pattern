using Autofac;
using CoreDui.TaskHandling;
using CoreDuiWebApi.Authentication;

namespace CoreDuiWebApi
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var currentAsm = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(currentAsm)
                .AsClosedTypesOf(typeof(IFlowTask<,>)).SingleInstance();
        }
    }
}
