using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Emit;
using WebAppLogin.Models;

namespace WebAppLogin.Services
{
    public class LoginService
    {
        string urlApi = WebAppContantes.URL_API;

        public string GetToken(string login, string senha)
        {
            string parametros = "/token?username=" + login + "&password=" + senha;
            var client = new RestClient(urlApi + parametros);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);

            IRestResponse response = client.Execute(request);

            var loginResponseRequest = new LoginResponseRequest();

            if (response.Content != null)
                loginResponseRequest = JsonConvert.DeserializeObject<LoginResponseRequest>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
                return loginResponseRequest.access_token;
            if (response.StatusCode == HttpStatusCode.Unauthorized)
                throw new Exception("Login inválido");
            else if (response.StatusCode == HttpStatusCode.NotFound)
                throw new Exception("Usuário não encontrado");
            else if(loginResponseRequest!= null && !loginResponseRequest.error_description.IsNullOrWhiteSpace())
                throw new Exception(loginResponseRequest.error_description);
            else
                throw new Exception("Não foi possível realizar o login");
        }

        public bool EnviarNovaSenha(string senha, string token)
        {
            var client = new RestClient(urlApi + "/usuario/atualizarsenha?senha=" + senha);
            client.Timeout = -1;
            var request = new RestRequest(Method.PUT);
            request.AddHeader("Authorization", $"Bearer {token}");

            IRestResponse response = client.Execute(request);

            var loginResponseRequest = new LoginResponseRequest();

            if (response.Content != null)
                loginResponseRequest = JsonConvert.DeserializeObject<LoginResponseRequest>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Usuário não encontrado");
            }
            else if (!loginResponseRequest.error_description.IsNullOrWhiteSpace())
            {
                throw new Exception(loginResponseRequest.error_description);
            }
            else
            {
                throw new Exception("Não foi possível realizar o login");
            }
        }

        public bool EnviarEmailRecuperacao(string email)
        {
            var client = new RestClient(urlApi + "/usuario/recuperar?email=" + email);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            IRestResponse response = client.Execute(request);

            var loginResponseRequest = new LoginResponseRequest();

            if (response.Content != null)
                loginResponseRequest = JsonConvert.DeserializeObject<LoginResponseRequest>(response.Content);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return true;
            }
            else if (response.StatusCode == HttpStatusCode.NotFound)
            {
                throw new Exception("Usuário não encontrado");
            }
            else if (!loginResponseRequest.error_description.IsNullOrWhiteSpace())
            {
                throw new Exception(loginResponseRequest.error_description);
            }
            else
            {
                throw new Exception("Não foi possível realizar o login");
            }
        }
    }

    #region [ Classes Auxiliares]
    public class RequestTokenModel
    {
        public string username;
        public string password;
        public string grant_type = "password";
    }

    public class LoginResponseRequest
    {
        public string access_token { get; set; }
        public string error { get; set; }
        public string error_description { get; set; }
    }
    #endregion [ Classes Auxiliares]
}