//***CODE BEHIND - BY RODOLFO.FONSECA***//
using Newtonsoft.Json;
using System;

namespace CodeBehind.BFF
{
    public class Cliente
    {
        [JsonProperty("idcliente")]
        public int IdCliente { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }
}
