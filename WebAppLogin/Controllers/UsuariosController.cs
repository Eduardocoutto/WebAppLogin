using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Web.Mvc;
using WebAppLogin.Models;
using WebAppLogin.Services;
using X.PagedList;

namespace WebAppLogin
{
    public class UsuariosController : Controller
    {
        private UsuarioService usuarioService;

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
            string token = Session["USER_TOKEN"] == null ? "" : Session["USER_TOKEN"].ToString();

            if(token == "")
                RedirectToAction("entrar","login");

            this.usuarioService = new UsuarioService(token);
        }

        // GET: Usuarios
        public ViewResult Index(int? pagina)
        {
            try
            {
                int numPag = pagina ?? 1;

                return View(usuarioService.Listar().ToPagedList(numPag, 10));
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View();
            }
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = usuarioService.BuscarPorId(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);

            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View();
            }
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.Message = "";
            return View();
        }        

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdUsuario,Email,Login,Senha,Status,Admin")] Usuario model)
        {
            try
            {
                ViewBag.Message = "";

                if (ModelState.IsValid)
                {
                    Usuario usuarioCriado = usuarioService.Inserir(model);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
            }
            return View(model);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Usuario usuario = usuarioService.BuscarPorId(id);
                if (usuario == null)
                {
                    return HttpNotFound();
                }
                
                return View(usuario.GetUserEdit());
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View(id);
            }
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdUsuario,Email,Login,Status,Admin")] UsuarioEdit model)
        {
            try
            {
                ViewBag.Message = "";

                if (ModelState.IsValid)
                {
                    usuarioService.Atualizar(model);
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View(model);
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                Usuario usuario = usuarioService.BuscarPorId((int)id);

                if (usuario == null)
                {
                    return HttpNotFound();
                }
                return View(usuario);
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View(id);
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                usuarioService.Deletar(id);
                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                ViewBag.Message = erro.Message;
                return View(id);
            }
        }
    }
}
