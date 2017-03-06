using CMSdal;
using CMSviewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSbl
{
    public class CMSService
    {
        public CMSContext Context { get; set; }

        //---------------------Article-------------------//
        public void AddArt(ArticleViewModel article)                        //Adding new article
        {
            Context.AddArt(new Article
            {
                 Header = article.Header,
                 Text = article.Text,
                 Annotation = article.Annotation                
            });
        }

        public List<ArticleViewModel> GetArts()                            //Get all article
        {
            var result = new List<ArticleViewModel>();
            var tempTagList = new List<TagViewModel>();
            foreach(var art in Context.GetArts())
            {
                foreach(var tag in art.Tags)
                {
                    tempTagList.Add(new TagViewModel
                    {
                        Id = tag.Id,
                        Name = tag.Name
                    });
                }
                result.Add(new ArticleViewModel
                {
                     Id = art.Id,
                     Header = art.Header,
                     Text = art.Text,
                     Annotation = art.Annotation,
                     CreateTime = art.CreateTime,
                     ChangeTime = art.ChangeTime,
                     Tags=tempTagList
                });
            }
            return result;
        }

        public ArticleViewModel GetArt(int id)                                  //Get article by id
        {
            var tempArt = Context.GetArt(id);
            var tempTagList = new List<TagViewModel>();

            foreach(var tag in tempArt.Tags)
            {
                tempTagList.Add(new TagViewModel
                {
                     Id=tag.Id,
                     Name=tag.Name
                });
            }

            var result = new ArticleViewModel
            {
                 Id=tempArt.Id,
                 Header=tempArt.Header,
                 Text=tempArt.Text,
                 Annotation=tempArt.Annotation,
                 CreateTime=tempArt.CreateTime,
                 ChangeTime=tempArt.ChangeTime,
                 Tags=tempTagList
            };

            return result;
        }

        public void UpdateArt(ArticleViewModel article)                         //Updat article
        {
            var tempArt = new Article

            {
                Id = article.Id,
                Header = article.Header,
                Text = article.Text,
                Annotation = article.Annotation
             };

            Context.UpdateArt(tempArt);
        }

        public void RemoveArt(int id)                                           //Remove article
        {
            Context.RemoveArt(id);
        }

        public void AddTagToArt(int idArt, string nameTag)                      //Add tag to article
        {
            Context.AddTagToArt(idArt, nameTag);
        }

        public void DelTagArt(int idArt, int idTag)
        {
            Context.DelTagArt(idArt, idTag);
        }

        //-----------------------Tags---------------------------//
        public void AddTag(TagViewModel tag)                                    //Adding tag
        {
            var tempTag = new Tag { Id=tag.Id, Name=tag.Name };

            Context.AddTag(tempTag);
        }

        public List<TagViewModel> GetTags()                                     //Get all tags
        {
            var result = new List<TagViewModel>();

            foreach(var tag in Context.GetTags())
            {
                result.Add(new TagViewModel
                {
                    Id=tag.Id,
                    Name=tag.Name
                });
            }

            return result;
        }

        public List<TagViewModel> GetTags(int idArt)                            //Get tags by id of article
        {
            var result = new List<TagViewModel>();

            foreach(var tag in Context.GetTags(idArt))
            {
                result.Add(new TagViewModel
                {
                     Id=tag.Id,
                     Name=tag.Name
                });
            }

            return result;
        }

        public TagViewModel GetTag(int id)                                       //Get tag by id
        {
            var tempTag = Context.GetTag(id);
            var result = new TagViewModel
            {
                Id=tempTag.Id,
                Name=tempTag.Name
            };

            return result;
        }

        public void UpdateTag(TagViewModel tag)                                 //Update tag
        {
            var tempTag = new Tag { Id = tag.Id, Name = tag.Name };

            Context.UpdateTag(tempTag);            
        }

        public void RemoveTag(int id)                                           //Remove tag by id
        {
            Context.RemoveTag(id);
        }
    }
}
