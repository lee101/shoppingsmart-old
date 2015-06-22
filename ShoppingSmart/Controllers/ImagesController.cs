using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShoppingSmart.Models;

namespace ShoppingSmart.Controllers
{
    public class ImagesController : Controller
    {
        //
        // GET: /Images/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// todo add some headers eg expires header for cacheing
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ActionResult Small(String name)
        {
            ShoppingSmartDAO ssdao  =new ShoppingSmartDAO();
            smallimage img;
            try
            {
                img = ssdao.getSmallImage(name);
            }
            catch (Exception)
            {
                Response.Redirect("/content/images/no_image.png");
                return null;
            }
            Response.ContentType = "image/jpg";
            Response.BinaryWrite(img.image.ToArray());
            return null;
        }


        public ActionResult Large(String name)
        {
            ShoppingSmartDAO ssdao = new ShoppingSmartDAO();
            largeimage img;

            try{
                img = ssdao.getLargeImage(name);
            }
            catch (Exception)
            {
                Response.Redirect("/content/images/no_image.png");
                return null;
            }
            Response.ContentType = "image/jpeg";
            Response.BinaryWrite(img.image.ToArray());
            return null;
        }


    }
}
