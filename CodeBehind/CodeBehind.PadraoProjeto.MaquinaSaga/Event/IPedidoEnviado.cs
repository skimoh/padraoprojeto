namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IPedidoEnviado
    {
        public Guid? RequestId { get; set; }
        Guid CurrelationId { get; }
        DateTime Timestamp { get; }
    }
}
