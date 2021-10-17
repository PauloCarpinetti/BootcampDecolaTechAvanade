using System;



namespace DIO_Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();          
        static void Main(string[] args)
        {
           string opcaoUsuario = OpcaoUsaurio();
           
           while (opcaoUsuario.ToUpper() != "X")
           {
               switch (opcaoUsuario)
               {
                   case "1":
                        ListarSeries();
                        break;
                   case "2":
                        InserirSeries();
                        break;
                   case "3":
                        AtualizarSeries();
                        break;
                   case "4":
                        ExcluirSeries();
                        break;
                   case "5":
                        VisualizarSeries();
                        break;
                   case "C":
                        Console.Clear();
                        break;
                   
                   default:
                   throw new ArgumentOutOfRangeException(); 
               }

                opcaoUsuario = OpcaoUsaurio();
           }

           Console.WriteLine("Obrigado por usar nossos serviços.");
           Console.ReadLine();
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar series");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma serie encontrada.");
                return;
            }
            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), 
                serie.retornaTitulo(), (excluido ? "*Excluido*" : ""));
            }
        }

        private static void InserirSeries()
        {
            Console.WriteLine("Inserir nova serie");

            var indiceSerie = repositorio.ProximoId();  

            var serie = PreecheSerie(indiceSerie);

            repositorio.Insere(serie);

            var retorno = repositorio.ProximoId() - 1;
            
            Console.WriteLine();
            Console.WriteLine("Serie incluida com sucesso, Id numero {0}.", retorno);            

            }

        private static void AtualizarSeries()
        {
            Console.WriteLine("Atualizar serie existente.");
           
            var indiceSerie = VerificaId();           

            var serie = PreecheSerie(indiceSerie);

            repositorio.Atualiza(indiceSerie, serie);                

        }

        private static void ExcluirSeries()
        {
            Console.Write("Exclusao de series:");
            Console.WriteLine();
            var indiceSerie = VerificaId();

            repositorio.Exclui(indiceSerie);

            Console.WriteLine("Serie excluida com sucesso!");
            Console.WriteLine();
        }

        private static void VisualizarSeries()
        {
            Console.WriteLine("Visualizar descrição de uma serie");
            var indiceSerie = VerificaId();

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string OpcaoUsaurio()
        {

            string [] valores = {"1", "2", "3", "4", "5", "C", "X"};

            Console.WriteLine();
            Console.WriteLine("DIO Series ao seu dispor!");
            Console.WriteLine("Informe a opcao desejada:");

            Console.WriteLine("1- Listar series");
            Console.WriteLine("2- Inserir nova serie");
            Console.WriteLine("3- Atualizar serie");
            Console.WriteLine("4- Excluir serie");
            Console.WriteLine("5- Visualizar serie");   
            Console.WriteLine("C- limpar tela");
            Console.WriteLine("X- Sair");
            Console.WriteLine(); 

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();

            if (Array.Exists(valores, valor => valor == opcaoUsuario))
            {
                return opcaoUsuario;
            }
            else 
            {
                Console.WriteLine("Insira um valor valido.");
                return OpcaoUsaurio();
            }
        }

        private static int VerificaId()
        {
            int tentativas = 0;

            while(tentativas < 3)
            {            
            Console.WriteLine("Digite o Id da serie: ");
            string indiceSerie = Console.ReadLine();
            int i = 0;
            bool resultado = int.TryParse(indiceSerie, out i);

            if (resultado)
            {
            try 
            {
                var serie = repositorio.RetornaPorId(i);
                return i;
            }
            catch 
            {
                Console.WriteLine("Id não encontrado.");
                Console.WriteLine();
                tentativas++;                
            }
            }
            else
            {
               Console.WriteLine("Id deve ser numerico.");
               Console.WriteLine();
               tentativas++;              
            } 
            }
            Console.WriteLine("Numero de tenteivas excedido o aplicativo sera encerrado.");
            Console.WriteLine();
            return 500;                    
        }  

        private static Serie PreecheSerie(int id)
        {
            int entradaGenero = EntraGenero();    

            int indiceSerie = id;

            Console.WriteLine("Digite o Titulo da serie: ");
            string entradaTitulo = Console.ReadLine();
            
            Console.WriteLine("Digite o Ano de criação da serie: ");
            int entradaAno = EntraAno();

            Console.WriteLine("Digite a desrcição da serie: ");
            string entradaDescricao =  Console.ReadLine();

            Serie serie = new Serie(id: indiceSerie,
                                  genero: (Genero)entradaGenero,
                                  titulo: entradaTitulo,
                                  ano: entradaAno,
                                  descricao: entradaDescricao);
           return serie;
        }

        private static int EntraAno()
        {            
            string entradaAno = Console.ReadLine();
            int i = 0;
            bool resultado = int.TryParse(entradaAno, out i);

            if(resultado)
            {
               return i;
            }
            else
            {
                Console.WriteLine("Digite um valor numerico.");
                return EntraAno();
            }
        }

        private static int EntraGenero()
        {
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Genero), i));                                 
            }

            Console.WriteLine("Digite entre as opcoes acima: ");
            var entradaGenero = (Console.ReadLine());

            Genero genero;    
            if(Enum.TryParse<Genero>(entradaGenero, out genero))
            {
                int entradaGeneroFinal = int.Parse(entradaGenero);
                return entradaGeneroFinal;
            }
            else
            {
                return EntraGenero();
            }
        }
       
    }
}




        