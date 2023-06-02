//***CODE BEHIND - BY RODOLFO.FONSECA***//

namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IPedidoCancelado
    {
        public Guid? RequestId { get; set; }
        Guid CurrelationId { get; }
        DateTime Timestamp { get; }
    }
}
