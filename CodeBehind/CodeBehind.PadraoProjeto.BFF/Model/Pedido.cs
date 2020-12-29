//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Newtonsoft.Json;
using System;

namespace CodeBehind.BFF
{
    public class Pedido
    {
        [JsonProperty("idpedido")]
        public int IdPedido { get; set; }
        [JsonProperty("idcliente")]
        public int IdCliente { get; set; }
        [JsonProperty("valor")]
        public decimal Valor { get; set; }
        [JsonProperty("datacompra")]
        public DateTime DataCompra { get; set; }
        [JsonProperty("cep")]
        public string Cep { get; set; }
        [JsonProperty("cidade")]
        public string Cidade { get; set; }
    }
}
