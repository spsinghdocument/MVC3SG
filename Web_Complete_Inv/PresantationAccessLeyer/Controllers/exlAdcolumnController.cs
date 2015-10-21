using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using ReportManagement;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using DataAccessLeyer.DAL;
using PresantationAccessLeyer.Models;
using System.Collections;

using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using Microsoft.VisualBasic;
using Zayko.Finance;
using System.Xml;
using System.Data;

namespace PresantationAccessLeyer.Controllers
{
    public class exlAdcolumnController : Controller
    {
        //
        // GET: /exlAdcolumn/

        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult SAve_Xml(string ColumnName, string Type)
        {
            XmlDocument MyXmlDocument = new XmlDocument();

     var path = Path.Combine(Server.MapPath("~/App_Data/Excelf/a.xml"));
     MyXmlDocument.Load(path);
     XmlElement ParentElement = MyXmlDocument.CreateElement("spreadtracker");
     XmlElement ColumnNamee = MyXmlDocument.CreateElement("ColumnName");
     ColumnNamee.InnerText = ColumnName;
     XmlElement Typee = MyXmlDocument.CreateElement("Type");
     Typee.InnerText = Type;
     ParentElement.AppendChild(ColumnNamee);
     ParentElement.AppendChild(Typee);
     MyXmlDocument.DocumentElement.AppendChild(ParentElement);
     MyXmlDocument.Save(path);


            return Json("SAve", JsonRequestBehavior.AllowGet);
        }
        [OutputCache(Duration=0)]
        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult Show(string ColumnName, string Type)
        {
            XmlDocument MyXmlDocument = new XmlDocument();

            var path = Path.Combine(Server.MapPath("~/App_Data/Excelf/a.xml"));
           // MyXmlDocument.Load(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);


            return Json(ds, JsonRequestBehavior.AllowGet);
        }

        
    }
}
