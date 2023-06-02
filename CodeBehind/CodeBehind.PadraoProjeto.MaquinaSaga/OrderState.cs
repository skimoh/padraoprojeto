//***CODE BEHIND - BY RODOLFO.FONSECA***//

using MassTransit;
using MongoDB.Bson.Serialization.Attributes;

namespace CodeBehind.PadraoProjeto.MaquinaSaga
{
    public class OrderState : SagaStateMachineInstance, ISagaVersion
    {
        [BsonId]
        public Guid CorrelationId { get; set; }
        public string? CurrentState { get; set; }
        public Guid? RequestId { get; set; }
        public DateTime? SubmitDate { get; set; }
        public DateTime? Updated { get; set; }
        public int Version { get; set; }                
        public Uri? ResponseAddress { get; internal set; }
        public string? InfoComplementar { get; set; }
        public int CodigoPedido { get; set; }
    }
}
