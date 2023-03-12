using System;

namespace PowerUtils.Results.Tests.Fakes
{
    public record FakeRecord
    {
        public Guid Id { get; set; }

        public string Foo { get; set; }
    }
}
