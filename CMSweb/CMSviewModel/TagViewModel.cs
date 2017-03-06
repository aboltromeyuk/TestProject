using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSviewModel
{
    public class TagViewModel
    {
        public int Id { get; set; }
        [DisplayName("Тег")]
        public string Name { get; set; }
    }
}
