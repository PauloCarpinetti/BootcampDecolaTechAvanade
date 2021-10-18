using Projeto2ApiCatalogoJogos.InputModel;
using Projeto2ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto2ApiCatalogoJogos.Services
{
    public interface IJogoService : IDisposable
    {
        Task<List<JogoViewModel>> Obter(int pagina, int quantidade);
        Task<JogoViewModel> Obter(Guid Id);
        Task<JogoViewModel> Inserir(JogoInputModel jogo);
        Task Atualizar(Guid Id, JogoInputModel jogo);
        Task Atualizar(Guid Id, double preco);
        Task AtualizarNome(Guid Id, string nome);
        Task Remover(Guid Id);

    }
}
