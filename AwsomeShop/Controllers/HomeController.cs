using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AwsomeShop.Models;
using AwesomeShop.AzureQueueLibrary.QueueConnection;
using AwesomeShop.AzureQueueLibrary.Messages;

namespace AwsomeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IQueueCommunicator _queueCommunicator;

        public HomeController(ILogger<HomeController> logger, IQueueCommunicator queueCommunicator)
        {
            _logger = logger;
            _queueCommunicator = queueCommunicator;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            ViewBag.Message = "";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactUs(string contactName, string emailAddress)
        {
            var thankYou = new SendEmailComand()
            {
                To = emailAddress,
                Subject = "Thank you",
                Body = "We will contact with you"
            };

            await _queueCommunicator.SendAsync(thankYou);
            var adminEmail = new SendEmailComand()
            {
                To = "kazarovroman@gmail.com ",
                Subject = "New Contact",
                Body = $"{contactName} {emailAddress}"
            };

            await _queueCommunicator.SendAsync(adminEmail);
            ViewBag.Message = "Thank you for message";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
