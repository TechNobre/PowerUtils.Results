namespace PowerUtils.Results.Benchmarks.Fakes
{
    public class FakeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FakeModel()
        {
            Id = 1;
            Name = "Fake name";
        }

        public FakeModel(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
