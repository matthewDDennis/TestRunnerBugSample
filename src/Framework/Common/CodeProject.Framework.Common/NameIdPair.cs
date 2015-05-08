namespace CodeProject.Framework
{
    /// <summary>
    /// Encapsulates a Name and its corresponding integer Id.
    /// </summary>
    public class NameIdPair : IHasId
    {
        public NameIdPair(int id, string name)
        {
            Id   = id;
            Name = name;
        }

        /// <summary>
        /// Gets or sets the Id of the pair.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets or sets the Name for the pair.
        /// </summary>
        public string Name { get; }
    }

    public class NameIdDescription : NameIdPair
    {
        public NameIdDescription(int id, string name, string description) :base(id, name)
        {
            Description = description;
        }

        public string Description { get; }
    }
}
