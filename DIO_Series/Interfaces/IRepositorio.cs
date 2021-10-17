using System.Collections.Generic;

namespace DIO_Series.Interfaces
{
    public interface IRepositorio<T>
    {
        List<T> Lista();
        T RetornaPorId(int Id);
        void Insere(T entidade);
        void Exclui(int Id);
        void Atualiza(int id, T entidade);
        int ProximoId();

    }    
}