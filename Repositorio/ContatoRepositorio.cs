using ControleDeContatos.Data;
using ControleDeContatos.Models;
using System.Collections.Generic;
using System.Linq;

namespace ControleDeContatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;
        public ContatoRepositorio(BancoContext bancoContext)
        {
            _bancoContext = bancoContext;
        }


        public ContatoModel ListarPorId(int id)
        {
            return _bancoContext.Contatos.FirstOrDefault(x => x.Id == id);
        }
        public List<ContatoModel> BuscarTodos()
        {
            return _bancoContext.Contatos.ToList();
        }
        public ContatoModel Adicionar(ContatoModel contato)
        {
            _bancoContext.Contatos.Add(contato);
            _bancoContext.SaveChanges();
            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel conatatoDB = ListarPorId(contato.Id);

            if (conatatoDB == null) throw new System.Exception("Houve um erro na atualização do contato");

            conatatoDB.Nome = contato.Nome;
            conatatoDB.Email = contato.Email;
            conatatoDB.Celular = contato.Celular;


            _bancoContext.Contatos.Update(conatatoDB);
            _bancoContext.SaveChanges();

            return conatatoDB;
        }

        public bool Apagar(int id)
        {
            ContatoModel conatatoDB = ListarPorId(id);

            if (conatatoDB == null) throw new System.Exception("Houve um erro na deleção do contato");

            _bancoContext.Contatos.Remove(conatatoDB);
            _bancoContext.SaveChanges();

            return true;

        }
    }
}
