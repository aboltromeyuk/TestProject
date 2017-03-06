using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CMSviewModel
{
    public class ArticleViewModel
    {
        [DisplayName("ID")]
        public int Id { get; set; }
        [DisplayName("Заголовок")]
        public string Header { get; set; }
        [DisplayName("Текст")]
        [UIHint("tinymce_jquery_full"), AllowHtml]
        public string Text { get; set; }
        [DisplayName("Аннотация")]
        public string Annotation { get; set; }
        [DisplayName("Время создания")]
        public string CreateTime { get; set; }
        [DisplayName("Время изменения")]
        public string ChangeTime { get; set; }
        public List<TagViewModel> Tags { get; set; }

    }
}
