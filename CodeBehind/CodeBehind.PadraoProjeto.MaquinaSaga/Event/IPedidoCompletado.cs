namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IPedidoCompletado
    {
        public Guid? RequestId { get; set; }
        Guid CurrelationId { get; }
        DateTime Timestamp { get; }
    }
}
