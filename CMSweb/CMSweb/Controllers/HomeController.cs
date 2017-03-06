using CMSbl;
using CMSdal;
using CMSviewModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMSweb.Controllers
{
    public class HomeController : Controller
    {
        CMSContext context = new CMSContext();
        CMSService service = new CMSService();
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Articles()
        {
            var result = new List<ArticleViewModel>();

            using (service.Context = context)
            {
                result = service.GetArts();
            }
            return View(result);
        }

        public ActionResult CreateArt()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateArt(ArticleViewModel article)
        {
            using (service.Context=context)
            {
                service.AddArt(article);
            }
            return RedirectToAction("Index");
        }

        public ActionResult UpdateArt(int id)
        {
            var result = new ArticleViewModel();

            using (service.Context = context)
            {
                result = service.GetArt(id);
            }

            return View(result);
        }
        [HttpPost]
        public ActionResult UpdateArt(ArticleViewModel article)
        {
            using (service.Context = context)
            {
                service.UpdateArt(article);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteArt(int id)
        {
            using (service.Context = context)
            {
                service.RemoveArt(id);
            }
            return RedirectToAction("Index");
        }
        
        public ActionResult AddedTags(int idArt)
        {
            var result = new ArtTagsViewModel();
            result.Id = idArt;
            using (service.Context = context)
            {
                result.Tags = service.GetTags(idArt);
            }

            return PartialView(result);
        }

        public void AddTagToArt(int idArt, string nameTag)
        {
            using (service.Context = context)
            {
                if(service.GetTags(idArt).Count < 20)
                service.AddTagToArt(idArt, nameTag);
            }
        }

        //-------------------------Tags--------------------------//

        public ActionResult Tags()
        {
            var result = new List<TagViewModel>();

            using (service.Context = context)
            {
                result = service.GetTags();
            }
            return View(result);
        }

        public ActionResult CreateTag()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult CreateTag(TagViewModel tag)
        {
            using (service.Context = context)
            {
                service.AddTag(tag);

            }
            return View();
        }

        public ActionResult UpdateTag(int id)
        {
            var result = new TagViewModel();

            using (service.Context = context)
            {
                result = service.GetTag(id);
            }

            return View(result);
        }

        [HttpPost]
        public ActionResult UpdateTag(TagViewModel tag)
        {
            using (service.Context = context)
            {
                service.UpdateTag(tag);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTag(int id)
        {
            using (service.Context = context)
            {
                service.RemoveTag(id);
            }

            return RedirectToAction("Index");
        }

        public ActionResult DelTagArt(int idArt, int idTag)
        {
            using (service.Context = context)
            {
                service.DelTagArt(idArt, idTag);
            }
            return RedirectToAction("Index");
        }
        public ActionResult Autocomplete(string term)
        {
            IEnumerable result;

            using (service.Context = context)
            {
                result = service.GetTags().Where(item => item.Name
                        .IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0)
                        .Select(a => new { value = a.Name })
                        .Distinct();
            }
            
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}