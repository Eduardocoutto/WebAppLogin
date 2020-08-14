using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using WebAppLogin.Models;

namespace WebAppLogin.Services
{
    public class ApiResponse
    {
        public string access_token { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
        public string Message { get; set; }
    }
}