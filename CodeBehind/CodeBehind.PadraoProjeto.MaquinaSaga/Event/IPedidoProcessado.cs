namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IPedidoProcessado
    {
        public Guid? RequestId { get; set; }
        Guid CurrelationId { get; }
        DateTime Timestamp { get; }
    }
}
