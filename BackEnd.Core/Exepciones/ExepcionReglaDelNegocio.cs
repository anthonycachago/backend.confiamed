

namespace BackEnd.Core.Exepciones;

public class ExepcionReglaDelNegocio:Exception
{
    public ExepcionReglaDelNegocio(string mensaje)
        :base(mensaje)
    {
        
    }
}
