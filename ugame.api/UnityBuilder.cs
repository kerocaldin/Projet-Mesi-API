using Michelin.ePC.Library.Logging;
using Michelin.ePC.Library.Unity.Enums;
using Michelin.ePC.Library.Unity.Fluent;

namespace ugame.api
{
    public class UnityBuilder : BuilderBase
    {
        public override void Configure()
        {
            Map<ILogger, ePCTraceLogger>(CommonLifetime.Singleton);
        }
    }
}
