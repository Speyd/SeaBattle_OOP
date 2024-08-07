using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuilderUse
{
    public class BuilderUsage<T>
    {
        public T Builder { get; set; }
        public int UsageCount { get; set; }

        public BuilderUsage(T builder, int usageCount)
        {
            Builder = builder;
            UsageCount = usageCount;
        }
    }
}
