using Microsoft.Ajax.Utilities;
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
    public class UsuarioService
    {
        string urlApi = WebAppContantes.URL_API + "/usuario";
        private string USER_TOKEN = "";
        RestClient _RestClient;
        public UsuarioService(string token)
        {
            USER_TOKEN = token;

            _RestClient = new RestClient(urlApi)
            {
                Timeout = -1
            };
        }

        public Usuario Inserir(Usuario usuario)
        {
            var request = new RestRequest(Method.POST);
            var json = JsonConvert.SerializeObject(usuario);

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = _RestClient.Execute(request);

             
             ApiResponse apiResponse = JsonConvert.DeserializeObject<ApiResponse>(response.Content);


            if (response.StatusCode == HttpStatusCode.OK)
            {
                var novoUsuario = JsonConvert.DeserializeObject<Usuario>(response.Content);
                return novoUsuario;
            }
            else if(!apiResponse.Message.IsNullOrWhiteSpace())
                throw new Exception(apiResponse.Message);
            else
            {
                throw new Exception("Não foi possível inserir");
            }
        }

        public Pagination<Usuario> Paginacao(int page = 1, int qtdRegistros = 10)
        {
            var client = new RestClient(urlApi + $"search=&limite={qtdRegistros}&page={page}&sort=desc&tiposort=0");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {USER_TOKEN}");

            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<Pagination<Usuario>>(response.Content);
                return responseData;
            }
            else
            {
                throw new Exception("Não foi possível Listar");
            }
        }

        public List<Usuario> Listar()
        {
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {USER_TOKEN}");

            IRestResponse response = _RestClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<List<Usuario>>(response.Content);
                return responseData;
            }
            else
            {
                throw new Exception("Não foi possível Listar");
            }
        }

        public Usuario BuscarPorId(int? id)
        {
            int idUsuario = (int)id;

            if (id == null)
                return null;

            var client = new RestClient(urlApi + $"/{idUsuario}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {USER_TOKEN}");
            
            IRestResponse response = client.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseData = JsonConvert.DeserializeObject<Usuario>(response.Content);
                return responseData;
            }
            else
            {
                throw new Exception("Não foi possível encontrar");
            }
        }

        public Usuario Atualizar(UsuarioEdit usuario)
        {
            var request = new RestRequest(Method.PUT);
            var json = JsonConvert.SerializeObject(usuario);
            
            request.AddHeader("Authorization", $"Bearer {USER_TOKEN}");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = _RestClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                var novoUsuario = JsonConvert.DeserializeObject<Usuario>(response.Content);
                return novoUsuario;
            }
            else
            {
                throw new Exception("Não foi possível inserir");
            }
        }
        public bool Deletar(int idUsuario)
        {
            var client = new RestClient(urlApi + $"/{idUsuario}");
            client.Timeout = -1;
            var request = new RestRequest(Method.DELETE);
            request.AddHeader("Authorization", $"Bearer {USER_TOKEN}");

            IRestResponse response = client.Execute(request);

            return response.StatusCode == HttpStatusCode.OK;
        }
    }


}