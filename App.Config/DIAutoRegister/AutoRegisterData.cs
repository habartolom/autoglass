using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Config.DIAutoRegister
{
    public class AutoRegisterData
    {
        public AutoRegisterData(IServiceCollection services, IEnumerable<Type> typesToConsider)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TypesToConsider = typesToConsider ?? throw new ArgumentNullException(nameof(typesToConsider));
        }

        public IServiceCollection Services { get; }

        public IEnumerable<Type> TypesToConsider { get; }

        public Func<Type, bool> TypeFilter { get; set; }
    }
}
