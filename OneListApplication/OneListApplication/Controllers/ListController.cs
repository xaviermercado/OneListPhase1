﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneListApplication.Controllers
{
    public class ListController : Controller
    {
        // GET: List
        public ActionResult ListManagement()
        {
            return View();
        }

        public ActionResult CreateList()
        {
            return View();
        }

        public ActionResult ShowListDetails()
        {
            return View();
        }

    }
}