using System.Collections.Generic;

namespace Models.Tags
{
    public class TagCloud
    {
        private readonly Dictionary<Tag, int> _tagCount = new Dictionary<Tag, int>();


        public void AddTagToCloud(Tag tag)
        {
            SetTagCountValue(tag, _tagCount.ContainsKey(tag) ? _tagCount[tag] : 1);
        }

        public void SetTagCountValue(Tag tag, int count)
        {
            if (_tagCount.ContainsKey(tag))
            {
                _tagCount.Remove(tag);
            }

            _tagCount[tag] = count;
        }

        public int this[Tag tag]
        {
            get
            {
                if (tag == null) return 0;
                return _tagCount.ContainsKey(tag) ? _tagCount[tag] : 0;
            }
        }
        
    }
}
