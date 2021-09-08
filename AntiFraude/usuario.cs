using System;

namespace AntiFraude
{
    public class usuario
    {
        private string nome;
        public string Nome
        {
            get { return nome; }
            set { nome = value; }
        } 

         private string cpf;
         public string Cpf
         {
             get { return cpf; }
             set { cpf = value; }
         }
        
        private string patrimonio;
        public string Patrimonio
        {
            get { return patrimonio; }
            set { patrimonio = value; }
        }
        
        private string resultrelac;
        public string ResultRelac
        {
            get { return resultrelac; }
            set { resultrelac = value; }
        }
    }
}