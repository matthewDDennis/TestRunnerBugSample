using System;

namespace CodeProject.Framework.ReferenceData
{
    public interface ITag : IHasId
    {
        string Notes { get; }
        int Count { get; }
        string Name { get; }
        string ShortName { get; }
        byte Position { get; }
        bool Assignable { get; }
        bool Selectable { get; }
        bool SiteIndependent { get; }
        bool AllVersions { get; }
        int AttributeTypeId { get; }
        int ParentId { get; }
        bool SomeAttribute { get; }
        short SelectionMode { get; }
        int PublishingStatusId { get; }
    }

    public class Tag : ITag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public byte Position { get; set; }
        public bool Assignable { get; set; }
        public bool Selectable { get; set; }
        public bool SiteIndependent { get; set; }
        public bool AllVersions { get; set; }
        public int AttributeTypeId { get; set; }
        public int ParentId { get; set; }
        public bool SomeAttribute { get; set; }
        public short SelectionMode { get; set; }
        public int PublishingStatusId { get; set; }
        public string Notes { get; set; }
        public int Count { get; set; }
    }

    public class TagSummary : NameIdPair
    {
        public TagSummary(int id, string name, int count) : base(id, name)
        {
            Count = count;
        }

        public int Count { get; }
    }

    public class TagSummaryFactory : ISummaryFactory<ITag, TagSummary>
    {
        public TagSummary CreateSummary(ITag tag)
        {
            if (tag == null)
                return null;

            return new TagSummary(tag.Id, tag.Name, tag.Count);
        }
    }
}