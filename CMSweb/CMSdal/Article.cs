using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSdal
{
    public class Article
    {
        public int Id { get; set; }
        public string Header { get; set; }
        public string Text { get; set; }
        public string Annotation { get; set; }
        public string CreateTime { get; set; }
        public string ChangeTime { get; set; }

        public ICollection<Tag> Tags { get; set; }
        public Article()
        {
            Tags = new List<Tag>();
        }
    }
}
