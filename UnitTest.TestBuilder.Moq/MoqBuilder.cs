using UnitTest.TestBuilder.Core;
using UnitTest.TestBuilder.Core.Abstracts;

namespace UnitTest.TestBuilder.Moq
{
    public abstract class MoqBuilder<T> : BaseBuilder<T>
    {
        public MoqBuilder(IContainer container) : base(container, new ObjectBuilder())
        {

        }
    }
}
