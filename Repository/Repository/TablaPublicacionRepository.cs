using AutoMapper;
using Database.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace Repository.Repository
{
    public class TablaPublicacionRepository : BaseRepository<TablaPublicaciones, SocialNetworkContext>
    {
        private readonly SocialNetworkContext _context;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly TablaUsuarioRepository _tablaUsuarioRepository;
        private readonly TablaComentarioRepository _tablaComentarioRepository;
        private readonly SubTablaComentarioRepository _subTablaComentarioRepository;

        public TablaPublicacionRepository(SocialNetworkContext context, IMapper mapper,
              IHostingEnvironment hostingEnvironment, TablaUsuarioRepository tablaUsuarioRepository, 
              TablaComentarioRepository tablaComentarioRepository, SubTablaComentarioRepository subTablaComentarioRepository) : base(context)
        {
            _context = context;
            _mapper = mapper;
            this.hostingEnvironment = hostingEnvironment;
            _tablaUsuarioRepository = tablaUsuarioRepository;
            _tablaComentarioRepository = tablaComentarioRepository;
            _subTablaComentarioRepository = subTablaComentarioRepository;
        }



        public async Task AgregarPublicacion(PublicacionUsuarioViewModels pb) {

            string uniqueName = null;

            if (pb.FotoIformfilePublicacion != null)
            {

                var FolderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/fotoPublicacion");

                uniqueName = Guid.NewGuid().ToString() + "name" + pb.FotoIformfilePublicacion.FileName;

                var FilePath = Path.Combine(FolderPath, uniqueName);

                if (FilePath != null)
                {

                    var stream = new FileStream(FilePath, FileMode.Create);
                    pb.FotoIformfilePublicacion.CopyTo(stream);
                    stream.Flush();
                    stream.Close();
                  
                  
                }


            }


            pb.FotoPublicacion = uniqueName;
            var publicacion = _mapper.Map<TablaPublicaciones>(pb);

            await AddAsync(publicacion);

        }


        public async Task<List<PublicacionPlantilla>> TraarPublicacionesMyUsuario(int id){

            var usuario = await _tablaUsuarioRepository.GetByIdAsync(id);
            var publicaciones = await _context.TablaPublicaciones.Where(op => op.IdUsuario == id).OrderByDescending(op => op.Fecha).ToListAsync();

            var listado = new List<PublicacionPlantilla>();

            publicaciones.ForEach(op => {

                var final = _mapper.Map<PublicacionPlantilla>(op);
                final.Nombre = usuario.Nombre;
                final.Apellido = usuario.Apellido;
                final.FotoPerfil = usuario.FotoPerfil;

                listado.Add(final);
                
            
            });

            return listado;
        }


        public async Task<List<ComentarioPlantilla>> TraerComentariosMyUsuario()
        {

            var comentarios = await _tablaComentarioRepository.GetAllAsync();

            var listado = new List<ComentarioPlantilla>();

            foreach (var op in comentarios)
            {
                var usuario = await _tablaUsuarioRepository.GetByIdAsync(op.IdUsuario);
                var final = _mapper.Map<ComentarioPlantilla>(op);
                final.Nombre = usuario.Nombre;
                final.Apellido = usuario.Apellido;
                final.FotoPerfil = usuario.FotoPerfil;

                listado.Add(final);

            }

            return listado;
        }

        public async Task<List<PlantillaSubComentarios>> TraerSubComentariosMyUsuario()
        {

            var subcomentarios = await _subTablaComentarioRepository.GetAllAsync();

            var listado = new List<PlantillaSubComentarios>();

            foreach (var op in subcomentarios)
            {
                var usuario = await _tablaUsuarioRepository.GetByIdAsync(op.IdUsuario);
                var final = _mapper.Map<PlantillaSubComentarios>(op);
                final.Nombre = usuario.Nombre;
                final.Apellido = usuario.Apellido;
                final.FotoPerfil = usuario.FotoPerfil;

                listado.Add(final);

            }

            return listado;
        }


        public async Task<PublicacionEditViewModels> GetPublicacionEdit(int id) {

            var publicacion = await GetByIdAsync(id);

            var pvm = new PublicacionEditViewModels();

            var usuario = await _tablaUsuarioRepository.GetByIdAsync(publicacion.IdUsuario);
            pvm= _mapper.Map<PublicacionEditViewModels>(publicacion);
            pvm.FotoPerfil = usuario.FotoPerfil;
            pvm.Nombre = usuario.Nombre;
            pvm.Apellido = usuario.Apellido;


            return pvm;
        }

        public async Task<bool> PostPublicacionEdit(PublicacionEditViewModels pb, int id)
        {
            if (pb == null) {

                return false;
            }

            var publicacionVieja = await GetByIdAsync(id);
            try
            {
                string uniqueName = null;

            if (pb.FotoIFormFilePublicacion != null)
            {

                var FolderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/fotoPublicacion");

                uniqueName = Guid.NewGuid().ToString() + "name" + pb.FotoIFormFilePublicacion.FileName;

                var FilePath = Path.Combine(FolderPath, uniqueName);



                if (!string.IsNullOrEmpty(publicacionVieja.FotoPublicacion))
                {

                    var FilePathDelete = Path.Combine(FolderPath, publicacionVieja.FotoPublicacion);

                    if (System.IO.File.Exists(FilePathDelete))
                    {

                        var FileInfo = new System.IO.FileInfo(FilePathDelete);
                        FileInfo.Delete();

                    }

                }

                if (FilePath != null)
                {

                    var stream = new FileStream(FilePath, FileMode.Create);
                    pb.FotoIFormFilePublicacion.CopyTo(stream);
                    stream.Flush();
                    stream.Close();

                }


                publicacionVieja.Cuerpo = pb.Cuerpo;
                publicacionVieja.Titulo = pb.Titulo;
                publicacionVieja.Fecha = DateTime.Now;
               publicacionVieja.FotoPublicacion = uniqueName;
                await Update(publicacionVieja);


                return true;

            }
            else {


                publicacionVieja.Cuerpo = pb.Cuerpo;
                publicacionVieja.Titulo = pb.Titulo;
                publicacionVieja.Fecha = DateTime.Now;
                await Update(publicacionVieja);


                return true;



            }


        }
            catch
            {

                return false;
            }


            }


        public async Task<bool> EliminarPublicacionAll(int IdPublicacion) {

            try
            {
                var ListadocomentarioPublicacion = await _context.TablaComentarios.Where(op => op.IdPublicacion == IdPublicacion).ToListAsync();


                foreach (var coment in ListadocomentarioPublicacion)
                {

                    var ListadoSubcomentario = await _context.SubTablaComentarios.Where(x => x.IdComentario == coment.IdComentario).ToListAsync();

                    foreach (var Subcoment in ListadoSubcomentario)
                    {
                        await _subTablaComentarioRepository.DeleteEntity(Subcoment);
                    }

                    await _tablaComentarioRepository.DeleteEntity(coment);

                }

                var PublicacionABorrar = await GetByIdAsync(IdPublicacion);

                //  Eliminando Imagen  
                var FolderPath = Path.Combine(hostingEnvironment.WebRootPath, "images/fotoPublicacion");
                if (!string.IsNullOrEmpty(PublicacionABorrar.FotoPublicacion))
                {

                    var FilePathDelete = Path.Combine(FolderPath, PublicacionABorrar.FotoPublicacion);

                    if (System.IO.File.Exists(FilePathDelete))
                    {

                        var FileInfo = new System.IO.FileInfo(FilePathDelete);
                        FileInfo.Delete();

                    }

                }


              


                await DeleteEntity(PublicacionABorrar);


                return true;

            }catch{

                return false;
            }


        }

        //public void Borrar(string path)
        //{
        //    File.SetAttributes(path, FileAttributes.Normal);
        //    System.GC.Collect();
        //    System.GC.WaitForPendingFinalizers();

        //    File.Delete(path);
        //}



    }
}
