using System;
using System.Collections.Generic;

namespace Tech.Models
{
    public class ResultadoViewModel
    {
        public string QuestionarioNome { get; set; }
        public int TotalQuestoes { get; set; }
        public int Acertos { get; set; }
        public double Percentual { get; set; }
        
        // Propriedade calculada para determinar se o usuário foi aprovado
        public bool Aprovado => Percentual >= 70; // Considerando 70% como aprovação
        
        // Opcional: Lista de respostas para mostrar detalhes
        public List<RespostaDetalhe> DetalhesRespostas { get; set; }
        
        public ResultadoViewModel()
        {
            DetalhesRespostas = new List<RespostaDetalhe>();
        }
    }
    
    // Classe auxiliar para armazenar detalhes de cada resposta (opcional)
    public class RespostaDetalhe
    {
        public int QuestaoId { get; set; }
        public string QuestaoTexto { get; set; }
        public string RespostaUsuario { get; set; }
        public string RespostaCorreta { get; set; }
        public bool Acertou { get; set; }
    }
}
