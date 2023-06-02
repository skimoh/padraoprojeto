namespace CodeBehind.PadraoProjeto.MaquinaSaga.Event
{
    public interface IEventoBase
    {
        public Guid? RequestId { get; set; }
        Guid CurrelationId { get; }

        //personalizado
        DateTime DataOcorrencia { get; }
        public int CodigoPedido { get; set; }
    }
}
