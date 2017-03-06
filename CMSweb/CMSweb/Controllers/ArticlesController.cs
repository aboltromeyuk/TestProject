using CMSdal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CMSweb.Controllers
{
    public class ArticlesController : ApiController
    {
        
        public Article Get(int id)
        {
            var result = new Article();

            using (var db=new CMSContext())
            {
                result = db.Articles.Single(a=>a.Id==id);
            }

            return result;
        }

        [Route("GetLast")]
        public List<Article> GetLast(int n)
        {
            var result = new List<Article>();

            using (var db = new CMSContext())
            {

                List<Article> allArticles = db.Articles.ToList();
                result = allArticles.Skip(Math.Max(0, allArticles.Count() - n)).ToList();
            
            }

            return result;
        }
    }
}