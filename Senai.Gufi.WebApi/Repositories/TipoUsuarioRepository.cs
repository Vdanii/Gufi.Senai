using Senai.Gufi.WebApi.Domains;
using Senai.Gufi.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Gufi.WebApi.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuarioRepository
    {
        GufiContext ctx = new GufiContext();

        public void Atualizar(int id, TipoUsuario tipoUsuarioAtualizado)
        {
            TipoUsuario TipoUsuarioBuscado = ctx.TipoUsuario.Find(id);

            TipoUsuarioBuscado.TituloTipoUsuario = tipoUsuarioAtualizado.TituloTipoUsuario;

            ctx.TipoUsuario.Update(TipoUsuarioBuscado);

            ctx.SaveChanges();
        }

        public TipoUsuario BuscarPorId(int id)
        {
            return ctx.TipoUsuario.FirstOrDefault(tu => tu.IdTipoUsuario == id);
        }

        public void Cadastrar(TipoUsuario novoTipoUsuario)
        {
            ctx.TipoUsuario.Add(novoTipoUsuario);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            ctx.TipoUsuario.Remove(BuscarPorId(id));

            ctx.SaveChanges();
        }

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuario.ToList();
        }
    }
}
