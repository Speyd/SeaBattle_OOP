using Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLib
{
    public class BuilderUsage
    {
        public IBuilder Builder { get; set; }
        public int UsageCount { get; set; }

        public BuilderUsage(IBuilder builder, int usageCount)
        {
            Builder = builder;
            UsageCount = usageCount;
        }
    }
}
