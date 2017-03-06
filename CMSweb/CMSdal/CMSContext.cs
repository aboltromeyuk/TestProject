using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMSdal
{
    public class CMSContext : DbContext
    {
        public CMSContext() : base("DefaultConnection") { }


        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }

        //------------------Articles------------------//
        public void AddArt(Article article)                   //Create new article
        {
            using (var db = new CMSContext())
            {
                article.CreateTime = DateTime.Now.ToString();
                db.Articles.Add(article);
                db.SaveChanges();
            }
        }

        public List<Article> GetArts()                      //Get all articles
        {
            var listArts = new List<Article>();

            using (var db = new CMSContext())
            {
                listArts = db.Articles.Include(a=>a.Tags).ToList();
            }

            return listArts;
        }

        public Article GetArt(int id)                       //Get article by id
        {
            var tempArt = new Article();

            using (var db = new CMSContext())
            {
                tempArt = db.Articles.Include(a=>a.Tags).SingleOrDefault(a => a.Id == id);
            }

            return tempArt;
        }

        public void UpdateArt(Article article)                //Updating article  
        {
            var tempArt = new Article();
            using (var db = new CMSContext())
            {
                tempArt = db.Articles.Single(a=>a.Id == article.Id);

                tempArt.Header = article.Header;
                tempArt.Text = article.Text;
                tempArt.Annotation = article.Annotation;
                tempArt.ChangeTime = DateTime.Now.ToString();
                db.SaveChanges();
            }
        }

        public void AddTagToArt(int idArt, string nameTag)              //Adding tag to article
        {            
            using(var db = new CMSContext())
            {
                var tempArt = db.Articles.Single(a=>a.Id==idArt);           //search art by idArt
                var tempTag = db.Tags.FirstOrDefault(t => t.Name == nameTag);        //search tag by nameTag   

                if (tempTag != null && db.GetTags(idArt).FirstOrDefault(t=>t.Name==nameTag)==null)  // has the art whith id==idArt tag whith nameTag
                {
                    tempArt.Tags.Add(tempTag);
                    db.SaveChanges();
                }
                else if(tempTag == null)
                {
                    AddTag(new Tag { Name = nameTag });
                    tempArt.Tags.Add(db.Tags.FirstOrDefault(t => t.Name == nameTag));
                    db.SaveChanges();
                }
            }
        }

        public void RemoveArt(int id)                           //Remove article by id
        {
            using (var db = new CMSContext())
            {
                db.Articles.Remove(db.Articles.Single(a=>a.Id==id));
                db.SaveChanges();
            }
        }

        public void DelTagArt(int idArt, int idTag)             
        {
            using(var db=new CMSContext())
            {
                var art = db.Articles.Include(a=>a.Tags).Single(a => a.Id == idArt);
                var tag = db.Tags.Single(t => t.Id == idTag);
                art.Tags.Remove(tag);
                db.SaveChanges();
            }
        }

        //-----------------------Tags----------------------------//
        public void AddTag(Tag tag)                             //Adding new tag
        {
            using (var db = new CMSContext())
            {
                db.Tags.Add(tag);
                db.SaveChanges();
            }
        }

        public List<Tag> GetTags()                              //Get all tags
        {
            var listTags = new List<Tag>();

            using (var db = new CMSContext())
            {
                listTags = db.Tags.ToList();
            }

            return listTags;
        }

        public List<Tag> GetTags(int idArt)                     //Get tagslist by id of article
        {
            var listTags = new List<Tag>();
            var tempArt = new Article();

            using (var db=new CMSContext())
            {
                tempArt = db.Articles.Where(a => a.Id == idArt).Include(a => a.Tags).Single();
                listTags = tempArt.Tags.ToList();
            }

            return listTags;
        }

        public Tag GetTag(int id)                               //Get tag by id
        {
            var tempTag = new Tag();

            using (var db = new CMSContext())
            {
                tempTag = db.Tags.SingleOrDefault(t => t.Id == id);
            }

            return tempTag;
        }

        public void UpdateTag(Tag tag)                          //Updating tag
        {
            var tempTag = new Tag();

            using (var db=new CMSContext())
            {
                tempTag = db.Tags.Single(t => t.Id == tag.Id);

                tempTag.Name = tag.Name;
                db.SaveChanges();
            }
        }

        public void RemoveTag(int id)                           //Remove tag by id
        {
            using (var db = new CMSContext())
            {
                db.Tags.Remove(db.Tags.Single(t=>t.Id==id));
                db.SaveChanges();
            }
        }


    }
}
