using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ServicoRest_AplicandoSerti_BT.Models;
using ServicoRest_AplicandoSerti_BT.Models.WS;

namespace ServicoRest_AplicandoSerti_BT.Controllers
{
    public class VolumenController : ApiController
    {
        [HttpGet]
        public Response Get()
        {
            Response r = new Response();
            r.resultado = 0;

            try
            {
                using (Biblioteca_BabelEntities1 db = new Biblioteca_BabelEntities1())
                {
                    List<VolumenesModel> volumenes = List(db);
                    r.data = volumenes;
                    r.resultado = 1;
                    r.mensaje = volumenes.Count == 0 ? "No existen volumenes registrados" : "OK";
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        [HttpPost]
        public Response Add([FromBody] VolumenesModel model)
        {
            Response r = new Response();
            r.resultado = 0;

            if (!Validate(model))
            {
                r.mensaje = "Todos los valores son requeridos";
                return r;
            }

            try
            {
                using (Biblioteca_BabelEntities1 db = new Biblioteca_BabelEntities1())
                {
                    Volumenes v = new Volumenes();
                    v.No_Volumen = model.No_Volumen;
                    v.Titulo = model.Titulo;
                    v.IsActive = 1;
                    v.Id_Localizacion = model.Id_Localizacion;

                    db.Volumenes.Add(v);
                    db.SaveChanges();

                    List<VolumenesModel> lst = List(db);
                    r.data = lst;
                    r.resultado = 1;
                    r.mensaje = "Agregado exitosamente";
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        [HttpPut]
        public Response Edit([FromBody] VolumenesModel model)
        {
            Response r = new Response();
            r.resultado = 0;

            if (!Validate(model))
            {
                r.mensaje = "Todos los valores son requeridos";
                return r;
            }

            try
            {
                using (Biblioteca_BabelEntities1 db = new Biblioteca_BabelEntities1())
                {
                    Volumenes v = db.Volumenes.Find(model.Id);
                    v.No_Volumen = model.No_Volumen;
                    v.Titulo = model.Titulo;
                    v.Id_Localizacion = model.Id_Localizacion;

                    db.Entry(v).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<VolumenesModel> lst = List(db);
                    r.data = lst;
                    r.resultado = 1;
                    r.mensaje = "Volúmen con el ID: " + model.Id + " modificado con éxito";
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        [HttpDelete]
        public Response Delete(int? Id)
        {
            Response r = new Response();
            r.resultado = 0;

            if (Id == null)
            {
                r.mensaje = "El ID es requerido en la URL";
                return r;
            }

            try
            {
                using (Biblioteca_BabelEntities1 db = new Biblioteca_BabelEntities1())
                {
                    Volumenes v = db.Volumenes.Find(Id);
                    if (v != null)
                    {
                        if (v.IsActive == 1)
                        {
                            v.IsActive = 0;

                            db.Entry(v).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            List<VolumenesModel> lst = List(db);
                            r.data = lst;
                            r.resultado = 1;
                            r.mensaje = "Volúmen eliminado con éxito";
                        }
                        else
                        {
                            r.mensaje = "El Volúmen con el ID: " + Id + " no existe";
                        }
                    }
                    else
                    {
                        r.mensaje = "El Volúmen con el ID: " + Id + " no existe";
                    }
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        #region HELPERS

        private bool Validate(VolumenesModel model)
        {
            if(model != null)
            {
                return model.Titulo == "" ? false : true;
            }
            return false;
        }

        private List<VolumenesModel> List(Biblioteca_BabelEntities1 db)
        {
            List<VolumenesModel> volumenes = (from v in db.Volumenes
                                              join l in db.Localizacion on v.Id_Localizacion equals l.Id
                                              where v.IsActive == 1
                                                        select new VolumenesModel
                                                        {
                                                            Id = v.Id,
                                                            No_Volumen = v.No_Volumen,
                                                            Titulo = v.Titulo,
                                                            IsActive = v.IsActive,
                                                            Id_Localizacion = v.Id_Localizacion
                                                        }).ToList();

            return volumenes;
        }

        #endregion
    }
}
