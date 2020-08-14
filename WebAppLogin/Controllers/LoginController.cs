using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebAppLogin.Models;
using WebAppLogin.Services;

namespace WebAppLogin
{
    public class LoginController : Controller
    {
        LoginService loginService = new LoginService();
        UsuarioService UsuarioService = new UsuarioService("");

        // GET: Login/Entrar
        public ActionResult Entrar()
        {
            Session["USER_TOKEN"] = "";
            return View();
        }

        public ActionResult Sair()
        {
            Session["USER_TOKEN"] = "";
            return View();
        }

        // GET: Login/Entrar
        public ActionResult Registrar()
        {
            Session["USER_TOKEN"] = "";
            return View();
        }

        public ActionResult Recuperar()
        {
            Session["USER_TOKEN"] = "";
            return View();
        }

        public ActionResult DefinirSenha(string t)
        {
            Session["USER_TOKEN"] = t;
            return View();
        }

        

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Entrar([Bind(Include = "Login,Senha")] UsuarioLogin model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string token = loginService.GetToken(model.Login, model.Senha);

                    Session["USER_TOKEN"] = token;

                    return RedirectToAction("Index", "usuarios");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
            }
            return View(model);
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registrar([Bind(Include = "IdUsuario,Email,Login,Senha,Status,Admin")] Usuario model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuarioCriado = UsuarioService.Inserir(model);

                    string token = loginService.GetToken(usuarioCriado.Login, model.Senha);

                    Session["USER_TOKEN"] = token;

                    return RedirectToAction("Index", "usuarios");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
            }
            return View(model);
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Recuperar([Bind(Include = "Email")] UsuarioRecuperar model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool emailEnviado = loginService.EnviarEmailRecuperacao(model.Email);

                    return RedirectToAction("entrar", "login");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
            }
            return View(model);
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DefinirSenha([Bind(Include = "Senha")] UsuarioDefinirSenha model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string token = Request.QueryString.Get("t");

                    bool emailEnviado = loginService.EnviarNovaSenha(model.Senha, token);

                    if (!token.IsNullOrWhiteSpace())
                        Session["USER_TOKEN"] = token;

                    JavaScript("<script>alert(\"Email enviado com sucesso!\")</script>");
                    return RedirectToAction("Index", "usuarios");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
            }
            return View(model);
        }
    }
}
