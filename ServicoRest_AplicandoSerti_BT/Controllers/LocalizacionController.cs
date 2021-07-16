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
    public class LocalizacionController : ApiController
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
                    List<LocalizacionesModel> locations = List(db);
                    r.data = locations;
                    r.resultado = 1;
                    r.mensaje = locations.Count == 0 ? "No existen localizaciones registradas" : "OK";
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        [HttpGet]
        public Response Get(short Id)
        {
            Response r = new Response();
            r.resultado = 0;

            try
            {
                using (Biblioteca_BabelEntities1 db = new Biblioteca_BabelEntities1())
                {
                    List<LocalizacionesModel> location = (from l in db.Localizacion
                                                          where l.IsActive == 1 && l.Id == Id
                                                          select new LocalizacionesModel
                                                          {
                                                              Id = l.Id,
                                                              Estante = l.Estante,
                                                              Sala = l.Sala,
                                                              Librero = l.Librero,
                                                              Posicion = l.Posicion
                                                          }).ToList();
                    r.data = location;
                    r.resultado = 1;
                    r.mensaje = "OK";
                }
            }
            catch (Exception)
            {
                r.mensaje = "Error en el servidor, intentar más tarde";
            }
            return r;
        }

        [HttpPost]
        public Response Add([FromBody] LocalizacionesModel model)
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
                    Localizacion l = new Localizacion();
                    l.Estante = model.Estante;
                    l.Sala = model.Sala;
                    l.Librero = model.Librero;
                    l.Posicion = model.Posicion;
                    l.IsActive = 1;

                    db.Localizacion.Add(l);
                    db.SaveChanges();

                    List<LocalizacionesModel> lst = List(db);
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
        public Response Edit([FromBody] LocalizacionesModel model)
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
                    Localizacion l = db.Localizacion.Find(model.Id);
                    l.Estante = model.Estante;
                    l.Sala = model.Sala;
                    l.Librero = model.Librero;
                    l.Posicion = model.Posicion;

                    db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();

                    List<LocalizacionesModel> lst = List(db);
                    r.data = lst;
                    r.resultado = 1;
                    r.mensaje = "Localización con el ID: " + model.Id + " modificada con éxito";
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
                    Localizacion l = db.Localizacion.Find(Id);
                    if (l != null)
                    {
                        if (l.IsActive == 1)
                        {
                            l.IsActive = 0;

                            db.Entry(l).State = System.Data.Entity.EntityState.Modified;
                            db.SaveChanges();

                            List<LocalizacionesModel> lst = List(db);
                            r.data = lst;
                            r.resultado = 1;
                            r.mensaje = "Localización eliminada con éxito";
                        } else
                        {
                            r.mensaje = "La localización con el ID: " + Id + " no existe";
                        }
                    } else
                    {
                        r.mensaje = "La localización con el ID: " + Id + " no existe";
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

        private bool Validate(LocalizacionesModel model)
        {
            if(model != null)
            {
                return model.Sala == "" || model.Posicion == "" ? false : true;
            }
            return false;
        }

        private List<LocalizacionesModel> List(Biblioteca_BabelEntities1 db)
        {
            List<LocalizacionesModel> localizaciones = (from l in db.Localizacion
                                              where l.IsActive == 1
                                              select new LocalizacionesModel
                                              {
                                                  Id = l.Id,
                                                  Estante = l.Estante,
                                                  Sala = l.Sala,
                                                  Librero = l.Librero,
                                                  Posicion = l.Posicion
                                              }).ToList();

            return localizaciones;
        }

        #endregion
    }
}
