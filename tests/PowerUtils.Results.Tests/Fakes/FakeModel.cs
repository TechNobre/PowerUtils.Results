using System.Diagnostics;

namespace PowerUtils.Results.Tests.Fakes
{
    [DebuggerDisplay("Name = {Name}")]
    public class FakeModel
    {
        public int Id { get; set; }

        public string Name { get; set; }



        public FakeModel() { }

        public FakeModel(string name)

            => Name = name;
        public FakeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
