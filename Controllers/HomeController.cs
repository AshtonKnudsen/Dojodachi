using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;


namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        Random r = new Random();
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            int? full = HttpContext.Session.GetInt32("full");
            if(full == null){
                HttpContext.Session.SetInt32("full", 20);
            }
            int? happy = HttpContext.Session.GetInt32("happy");
            if(happy == null){
                HttpContext.Session.SetInt32("happy", 20);
            }
            int? energy = HttpContext.Session.GetInt32("energy");
            if(energy == null){
                HttpContext.Session.SetInt32("energy", 50);
            }
            int? meals = HttpContext.Session.GetInt32("meals");
            if(meals == null){
                HttpContext.Session.SetInt32("meals", 3);
            }
            string reaction = HttpContext.Session.GetString("reaction");
            if(reaction == null){
                HttpContext.Session.SetString("reaction", "See how the pet reacts here! :)");
            }
            if(full>=100&&happy>=100&&energy>=100){
                int game = 1;
                ViewBag.game = game;
                return View("verdict");
            }
            if(full<=0 || happy<=0){
                int game = 0;
                ViewBag.game = game;
                return View("verdict");
            }
            ViewBag.reaction = HttpContext.Session.GetString("reaction");
            ViewBag.fullness = HttpContext.Session.GetInt32("full");
            ViewBag.happy = HttpContext.Session.GetInt32("happy");
            ViewBag.energy = HttpContext.Session.GetInt32("energy");
            ViewBag.meals = HttpContext.Session.GetInt32("meals");
            return View();
        }
        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            int? meals = HttpContext.Session.GetInt32("meals");
            int? full = HttpContext.Session.GetInt32("full");
            if(meals != 0){
                meals -= 1;
                int like = r.Next(0,10);
                if(like>1){
                    full += r.Next(5,10);
                    HttpContext.Session.SetString("reaction", "Your pet gained some fullness and you used 1 meal.");
                }
                else{
                    full -= r.Next(5,10);
                    HttpContext.Session.SetString("reaction", "That food wasnt rotten enough for this pet.");
                }
                HttpContext.Session.SetInt32("full",(int)full);
                HttpContext.Session.SetInt32("meals",(int)meals);
            }
            else{
                HttpContext.Session.SetString("reaction", "You Have no Food Left!");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            int? energy = HttpContext.Session.GetInt32("energy");
            int? happy = HttpContext.Session.GetInt32("happy");
            if(energy >= 5){
                energy -= 5;
                int like = r.Next(0,10);
                if(like>1){
                    happy += r.Next(5,10);
                    HttpContext.Session.SetString("reaction", "You spent 5 energy and you pet got a little happier");
                }
                else{
                    happy -= r.Next(5,10);
                    HttpContext.Session.SetString("reaction", "Your pet didnt like that buddy");
                }
                HttpContext.Session.SetInt32("happy",(int)happy);
                HttpContext.Session.SetInt32("energy",(int)energy);
            }
            else{
                HttpContext.Session.SetString("reaction", "You Have no energy Left to Play with your pet!");
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            int? full= HttpContext.Session.GetInt32("full");
            int? happy = HttpContext.Session.GetInt32("happy");
            int? energy = HttpContext.Session.GetInt32("energy");
            full -= 5;
            happy -= 5;
            energy += 15;
            HttpContext.Session.SetString("reaction", "You had  great sleep!");
            HttpContext.Session.SetInt32("full",(int)full);
            HttpContext.Session.SetInt32("happy",(int)happy);
            HttpContext.Session.SetInt32("energy",(int)energy);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            int? meals = HttpContext.Session.GetInt32("meals");
            int? energy = HttpContext.Session.GetInt32("energy");
            energy -= 5;
            meals += r.Next(1,5);
            HttpContext.Session.SetInt32("meals",(int)meals);
            HttpContext.Session.SetInt32("energy",(int)energy);
            HttpContext.Session.SetString("reaction", "You went to work and brought home some food!");
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
