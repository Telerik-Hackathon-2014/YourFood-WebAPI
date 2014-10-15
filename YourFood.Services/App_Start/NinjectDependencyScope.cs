namespace YourFood.Services.App_Start
{
    using System;
    using System.Linq;
    using System.Web.Http.Dependencies;
    using Ninject;
    using Ninject.Syntax;

    public class NinjectDependencyScope : IDependencyScope
    {
        IResolutionRoot resolver;

        public NinjectDependencyScope(IResolutionRoot resolver)
        {
            this.resolver = resolver;
        }

        public object GetService(Type serviceType)
        {
            if (this.resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return this.resolver.TryGet(serviceType);
        }

        public System.Collections.Generic.IEnumerable<object> GetServices(Type serviceType)
        {
            if (this.resolver == null)
                throw new ObjectDisposedException("this", "This scope has been disposed");

            return this.resolver.GetAll(serviceType);
        }

        public void Dispose()
        {
            IDisposable disposable = this.resolver as IDisposable;
            if (disposable != null)
                disposable.Dispose();

            this.resolver = null;
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, IDependencyResolver
    {
        IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this.kernel.BeginBlock());
        }
    }
}