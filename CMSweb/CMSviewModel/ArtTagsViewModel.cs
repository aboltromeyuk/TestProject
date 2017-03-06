using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSviewModel
{
    public class ArtTagsViewModel
    {
        public int Id { get; set; }
        public List<TagViewModel> Tags { get; set;}
        public ArtTagsViewModel()
        {
            Tags = new List<TagViewModel>();
        }
    }
}
