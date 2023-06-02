//***CODE BEHIND - BY RODOLFO.FONSECA***//

namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IPedidoEnviado:IEventoBase
    {       
        public string Info { get; set; }
    }
}
