//***CODE BEHIND - BY RODOLFO.FONSECA***//

using MassTransit;
using MongoDB.Bson.Serialization.Attributes;
using System.Runtime.Serialization;

namespace CodeBehind.PadraoProjeto.MaquinaSaga
{
    public class OrderState : SagaStateMachineInstance, ISagaVersion
    {
        public string CurrentState { get; set; }

        public Guid? RequestId { get; set; }

        public DateTime? SubmitDate { get; set; }

        public DateTime? Updated { get; set; }

        public int Version { get; set; }

        [BsonId]
        public Guid CorrelationId { get; set; }

        public Uri? ResponseAddress { get; internal set; }

        public string? InfoComplementar { get; set; }
    }
}
